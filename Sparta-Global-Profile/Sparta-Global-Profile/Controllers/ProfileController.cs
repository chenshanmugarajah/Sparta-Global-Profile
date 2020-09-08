using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Renci.SshNet.Messages.Authentication;
using Sparta_Global_Profile.Models;

namespace Sparta_Global_Profile.Controllers
{
    public class ProfileController : Controller
    {
        private readonly SpartaGlobalProfileDbContext _context;
        private readonly IConfiguration _config;

        public ProfileController(SpartaGlobalProfileDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }


        // GET: Profile
        public async Task<IActionResult> Index(string searchString,  int? pageNumber, string currentFilter)
        {
            HttpContext context = HttpContext;
            var userId = context.Session.GetString("UserId");
            var userTypeId = context.Session.GetString("UserTypeId");
            var profileId = context.Session.GetString("ProfileId");

            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }
            
            if(userTypeId == "1")
            {
                return RedirectToAction("Details", "Profile", new { id = profileId });
            }

            ViewData["CurrentFilter"] = searchString;

            if(searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var profiles = from profile in _context.Profiles.Include(p => p.Course)
                           select profile;

            if(userTypeId == "2")
            {
                profiles = from profile in _context.Profiles.Include(p => p.Course).Where(p => p.Approved == true)
                           select profile;
            }

            if(!String.IsNullOrEmpty(searchString))
            {
                profiles = profiles.Where(p => p.Course.CourseName.Contains(searchString));
            }
            int pageSize = 12;

            return View(await PaginatedList<Profile>.CreateAsync(profiles.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Profile/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            HttpContext context = HttpContext;
            var userId = context.Session.GetString("UserId");

            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (id == null)
            {
                return NotFound();
            }

            var profile = await _context.Profiles
                .Include(p => p.Course)
                .Include(p => p.Status)
                .Include(p => p.User)
                .Include(p => p.Assignments)
                .Include(p => p.SpartaProjects).ThenInclude(l => l.ProjectLinks)
                .Include(p => p.Employment)
                .Include(p => p.Education).ThenInclude(e => e.Modules)
                .Include(p => p.Skills)
                .Include(p => p.Hobbies)
                .Include(p => p.Comments)
                .Include(p => p.Certifications)
                .FirstOrDefaultAsync(m => m.ProfileId == id);
            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }

        // GET: Profile/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId");
            ViewData["StatusId"] = new SelectList(_context.Status, "StatusId", "StatusId");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: Profile/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProfileId,UserId,StatusId,ProfileName,ProfilePicture,Summary,CourseId,Approved")] Profile profile)
        {
            HttpContext context = HttpContext;
            var userId = context.Session.GetString("UserId");

            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (ModelState.IsValid)
            {
                _context.Add(profile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId", profile.CourseId);
            ViewData["StatusId"] = new SelectList(_context.Status, "StatusId", "StatusId", profile.StatusId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", profile.UserId);
            return View(profile);
        }

        // GET: Profile/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            HttpContext context = HttpContext;
            var userId = context.Session.GetString("UserId");
            var userTypeId = context.Session.GetString("UserTypeId");

            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if(userTypeId == "2")
            {
                return RedirectToAction("Index", "Profile");
            }

            if (id == null)
            {
                return NotFound();
            }

            var profile = await _context.Profiles
                .Include(p => p.Course)
                .Include(p => p.Status)
                .Include(p => p.User)
                .Include(p => p.Assignments)
                .Include(p => p.SpartaProjects)
                .Include(p => p.Employment)
                .Include(p => p.Education).ThenInclude(e => e.Modules)
                .Include(p => p.Skills)
                .Include(p => p.Hobbies)
                .Include(p => p.Comments)
                .Include(p => p.Certifications)
                .FirstOrDefaultAsync(m => m.ProfileId == id);
            if (profile == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", profile.CourseId);
            ViewData["StatusId"] = new SelectList(_context.Status, "StatusId", "StatusName", profile.StatusId);
            ViewData["UserId"] = new SelectList(_context.Profiles.Where(p => p.ProfileId == id), "UserId", "UserId", profile.UserId);
            return View(profile);
        }

        // POST: Profile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int UserId, int ProfileId, int StatusId, string ProfileName, string ProfilePicture, string Summary, int CourseId, bool Approved)
        {
            HttpContext context = HttpContext;
            var userTypeId = Int32.Parse(context.Session.GetString("UserTypeId"));

            var studentProfile = _context.Profiles.First(p => p.ProfileId == ProfileId);

            if (userTypeId == 1)
            {
                StatusId = studentProfile.StatusId;
                CourseId = studentProfile.CourseId;
                Approved = studentProfile.Approved;
            }
          
            studentProfile.StatusId = StatusId;
            studentProfile.ProfileName = ProfileName;
            studentProfile.ProfilePicture = ProfilePicture;
            studentProfile.Summary = Summary;
            studentProfile.CourseId = CourseId;
            studentProfile.Approved = Approved;           

            if (id != ProfileId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.SaveChanges();
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfileExists(ProfileId))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId", studentProfile.CourseId);
            ViewData["StatusId"] = new SelectList(_context.Status, "StatusId", "StatusId", studentProfile.StatusId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", studentProfile.UserId);
            return View(studentProfile);
        }

        // GET: Profile/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profile = await _context.Profiles
                .Include(p => p.Course)
                .Include(p => p.Status)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.ProfileId == id);
            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }

        // POST: Profile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profile = await _context.Profiles.FindAsync(id);
            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfileExists(int id)
        {
            return _context.Profiles.Any(e => e.ProfileId == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadImageToAWS( [FromForm] IFormFile file, int profileId)
        {
            string profilePicUrl; 

            string bucketName = _config["AWS:BucketName"];
            string accessKey = _config["AWS:ACCESS_KEY"];
            string secretKey = _config["AWS:SECRET_KEY"];

            using (var client = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.EUWest1))
            {
                using (var newMemoryStream = new MemoryStream())
                {
                    var profile = _context.Profiles.First(profile => profile.ProfileId == profileId);

                    file.CopyTo(newMemoryStream);
                    try
                    {
                        var uploadRequest = new TransferUtilityUploadRequest
                        {
                            InputStream = newMemoryStream,
                            Key = $"{profile.UserId}_{profileId}_{profile.ProfileName}",
                            BucketName = bucketName,
                            CannedACL = S3CannedACL.PublicRead
                        };

                        var fileTransferUtility = new TransferUtility(client);
                        await fileTransferUtility.UploadAsync(uploadRequest);

                        profilePicUrl = $"https://{bucketName}.s3-eu-west-1.amazonaws.com/{profile.UserId}_{profileId}_{profile.ProfileName}";

                        profile.ProfilePicture = profilePicUrl;
                        _context.SaveChanges();
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine($"An AmazonS3Exception was thrown: {exception.Message}");
                    }
                }
            }
            return RedirectToAction("Edit", "Profile", new { id = profileId });
        }
    }
}
