using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            HttpContext context = HttpContext;
            var userId = context.Session.GetString("UserId");
            var userTypeId = context.Session.GetString("UserTypeId");
            var profileId = context.Session.GetString("ProfileId");

            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (userTypeId == "1")
            {
                return RedirectToAction("Details", "Profile", new { id = Int32.Parse(profileId) });
            }

            if (userTypeId != "5")
            {
                return RedirectToAction("Index", "Profile");
            }

            var spartaGlobalProfileDbContext = _context.Users.Include(u => u.UserType);
            return View(await spartaGlobalProfileDbContext.ToListAsync());
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
        public async Task<IActionResult> Create([Bind("UserId,UserEmail,UserPassword,UserTypeId")] User user, int courseId)
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
                            ProfilePicture = "",
                            Summary = "PLEASE DELETE THIS TEXT! ALL BODY TEXT SHOULD BE VERDANA SIZE 8 – PLEASE DO NOT EDIT FONT SIZES. HEADINGS ARE VERDANA 12 (I.E. SUMMARY, ACADEMY EXPERIENCE, ETC). SUBHEADINGS ARE VERDANA SIZE 9 (I.E. BUSINESS SKILLS, AUTOMATION, ETC.)" 
                            + "\nThis should be around 80 – 100 words and express your work ethics, personality, what you are like to work with in a team, what skills you are going to bring to the table and help the clients projects succeed.Example:"
                            + "Lee’s infectiously positive personality means he works very well within teams and provides motivation and direction towards the successful completion of projects. He is a person who can break down a problem into its constituent parts and provide effective solutions to tackle any issue at hand, it’s a winning formula when combining the ability to explain complex ideas concisely to audiences of varying levels in an engaging manner.",
                            CourseId =  courseId,
                            Approved = false
                        };
                        _context.Profiles.Add(profile);
                        await _context.SaveChangesAsync();
                    }

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
            ViewData["UserTypeId"] = new SelectList(_context.UserTypes, "UserTypeId", "UserTypeId", user.UserTypeId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,UserEmail,UserPassword,UserTypeId")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserTypeId"] = new SelectList(_context.UserTypes, "UserTypeId", "UserTypeId", user.UserTypeId);
            return View(user);
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
    }
}
