using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Sparta_Global_Profile.Models;


namespace Sparta_Global_Profile.Controllers
{
    public class UsersController : Controller
    {
        private readonly SpartaGlobalProfileDbContext _context;

        public UsersController(SpartaGlobalProfileDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index(string searchString)
        {
            HttpContext context = HttpContext;
            var userId = context.Session.GetInt32("UserId");
            var userTypeId = context.Session.GetInt32("UserTypeId");
            var profileId = context.Session.GetInt32("ProfileId");
            
            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (userTypeId == 1)
            {
                return RedirectToAction("Details", "Profile", new { id = profileId });
            }

            ViewData["CurrentFilter"] = searchString;

            if (userTypeId != 5)
            {
                return RedirectToAction("Index", "Profile");
            }

            var users = from user in _context.Users.Include(u => u.UserType)
                           select user;
            
            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.UserEmail.Contains(searchString));
            }

            //var spartaGlobalProfileDbContext = _context.Users.Include(u => u.UserType);
            return View(await users.ToListAsync());
        }

        // GET: Users/Details/5

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.UserType)
                .FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewData["UserTypeId"] = new SelectList(_context.UserTypes, "UserTypeId", "UserTypeName");
            ViewData["Courses"] = _context.Courses.ToList();
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,UserName,UserEmail,UserPassword,UserTypeId")] User user, int courseId, string adminEmailPassword)
        {
            if (ModelState.IsValid)
            {
                var checkUser = _context.Users.FirstOrDefault(u => u.UserEmail == user.UserEmail);
                if (checkUser == null)
                {
                    var password = Helper.EncryptPlainTextToCipherText(user.UserPassword);
                    user.UserPassword = password;
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    if (user.UserTypeId == 1)
                    {

                        var newUser = _context.Users.First(u => u.UserEmail == user.UserEmail);
                        var newUserId = newUser.UserId;
                        var profile = new Profile()
                        {
                            UserId = newUserId,
                            StatusId = 1,
                            ProfileName = "New Student",
                            ProfilePicture = @"/assets/default-profile-image.png",
                            Summary = "",
                            CourseId = courseId,
                            Approved = false
                        };
                        _context.Profiles.Add(profile);
                        await _context.SaveChangesAsync();
                    }
                    HttpContext context = HttpContext;
                    var userId = context.Session.GetInt32("UserId");
                    var admin = _context.Users.First(u => u.UserId == userId);
                    SendEmail(user.UserEmail, Helper.DecryptCipherTextToPlainText(user.UserPassword), user.UserName, admin.UserEmail, adminEmailPassword, admin.UserName);
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("UserEmail", "User Already Exists!");
                return View("Create");
            }
            ViewData["UserTypeId"] = new SelectList(_context.UserTypes, "UserTypeId", "UserTypeId", user.UserTypeId);
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["UserTypeId"] = new SelectList(_context.UserTypes, "UserTypeId", "UserTypeName", user.UserTypeId);
            ViewData["Courses"] = _context.Courses.ToList();
            var profile = _context.Profiles.FirstOrDefault(p => p.UserId == id);
            if(profile != null)
            {
                ViewData["CourseId"] = profile.CourseId;
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string userName, int userTypeId, int courseId, string newPassword, string newPasswordConfirm, string currentPassword, string currentPasswordError, string newPasswordConfirmError)
        {
            HttpContext context = HttpContext;
            var loggedInUserId = context.Session.GetInt32("UserId");
            var loggedInUserTypeId = context.Session.GetInt32("UserTypeId");

            var user = _context.Users.First(u => u.UserId == id);

            if (ModelState.IsValid)
            {
                if (loggedInUserId == id)
                {
                    if (currentPassword == Helper.DecryptCipherTextToPlainText(user.UserPassword))
                    {
                        if (newPassword == newPasswordConfirm)
                        {
                            user.UserPassword = Helper.EncryptPlainTextToCipherText(newPassword);
                            _context.Update(user);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            newPasswordConfirmError = "Passwords do not match";
                        }
                    }
                    else
                    {
                        currentPasswordError = "Password Incorrect";
                    }
                    return RedirectToAction(nameof(Index));
                }
                if(loggedInUserTypeId == 5)
                {
                    user.UserName = userName;
                    user.UserTypeId = userTypeId;
                    if(userTypeId == 1)
                    {
                        var profile = _context.Profiles.First(p => p.UserId == id);
                        profile.CourseId = courseId;
                        _context.Update(profile);
                    }
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.UserType)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            
            if(user.UserTypeId == 1)
            {
                var profile = _context.Profiles.First(p => p.UserId == id);
                _context.Profiles.Remove(profile);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }

        public async void SendEmail(string newUserEmail, string newUserPassword, string newUserName, string myEmail, string myPassword, string myName)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(myName, myEmail)); 
            message.To.Add(new MailboxAddress(newUserName, newUserEmail));
            message.Subject = "Sparta Global Profile Portal Invitation";
            message.Body = new TextPart("plain")
            {
                Text = @$"Hi {newUserName},

                You have received an invite to access Sparta Global's profile portal. Please follow the link below and enter the following account details to access the portal.
                
                Account Email: {newUserEmail}
                Account Password: {newUserPassword}

                https://spartaprofile09092020.azurewebsites.net/

                Kind regards, 
                {myName}
                "
            };

            using (var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync("smtp-mail.outlook.com", 587, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(myEmail, myPassword);
                await smtp.SendAsync(message);
                await smtp.DisconnectAsync(true);
            }
        }
    }
}
