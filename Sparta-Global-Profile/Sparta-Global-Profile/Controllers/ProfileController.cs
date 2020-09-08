using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System.IO;

using Sparta_Global_Profile.Models;

namespace Sparta_Global_Profile.Controllers
{
    public class ProfileController : Controller
    {
        private readonly SpartaGlobalProfileDbContext _context;

        public ProfileController(SpartaGlobalProfileDbContext context)
        {
            _context = context;
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
            Profile profile = new Profile()
            {
                UserId = UserId,
                ProfileId = ProfileId,
                StatusId = StatusId,
                ProfileName = ProfileName,
                ProfilePicture = ProfilePicture,
                Summary = Summary,
                CourseId = CourseId,
                Approved = Approved
            };

            if (id != profile.ProfileId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfileExists(profile.ProfileId))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId", profile.CourseId);
            ViewData["StatusId"] = new SelectList(_context.Status, "StatusId", "StatusId", profile.StatusId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", profile.UserId);
            return View(profile);
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

        // Export Word File
        public ActionResult CreateDocument()
        {
            var profileId = 1;
            Profile profile = _context.Profiles.Where(p => p.ProfileId == profileId)
                .Include(p => p.Course)
                .Include(p => p.Status)
                .Include(p => p.Assignments)
                .Include(p => p.SpartaProjects)
                .Include(p => p.Employment)
                .Include(p => p.Education).ThenInclude(e => e.Modules)
                .Include(p => p.Skills)
                .Include(p => p.Hobbies)
                .Include(p => p.Comments)
                .Include(p => p.Certifications)
                .FirstOrDefault();

            WordDocument document = new WordDocument();
            //Adding a new section to the document.
            WSection section = document.AddSection() as WSection;
            //Set Margin of the section
            section.PageSetup.Margins.All = 72;
            //Set page size of the section
            section.PageSetup.PageSize = new Syncfusion.Drawing.SizeF(612, 792);

            //Create Paragraph styles
            WParagraphStyle style = document.AddParagraphStyle("Normal") as WParagraphStyle;
            style.CharacterFormat.FontName = "Calibri";
            style.CharacterFormat.FontSize = 11f;
            style.ParagraphFormat.BeforeSpacing = 0;
            style.ParagraphFormat.AfterSpacing = 8;
            style.ParagraphFormat.LineSpacing = 13.8f;

            style = document.AddParagraphStyle("Heading 1") as WParagraphStyle;
            style.ApplyBaseStyle("Normal");
            style.CharacterFormat.FontName = "Calibri Light";
            style.CharacterFormat.FontSize = 16f;
            style.CharacterFormat.TextColor = Syncfusion.Drawing.Color.FromArgb(46, 116, 181);
            style.ParagraphFormat.BeforeSpacing = 12;
            style.ParagraphFormat.AfterSpacing = 0;
            style.ParagraphFormat.Keep = true;
            style.ParagraphFormat.KeepFollow = true;
            style.ParagraphFormat.OutlineLevel = OutlineLevel.Level1;

            // Create Paragraph with Text Range
            IWParagraph paragraph = section.HeadersFooters.Header.AddParagraph();
            paragraph.ApplyStyle("Normal");
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            WTextRange textRange = paragraph.AppendText($"Meet {profile.ProfileName}") as WTextRange;
            textRange.CharacterFormat.FontSize = 30f;
            textRange.CharacterFormat.FontName = "Verdana";
            textRange.CharacterFormat.TextColor = Syncfusion.Drawing.Color.Black;

            //Appends paragraph Name
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.FirstLineIndent = 36;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            foreach(var grade in profile.Education)
            {
                textRange = paragraph.AppendText($"{grade.Grade}") as WTextRange;
            }
            textRange.CharacterFormat.FontSize = 12f;

            //Appends paragraph Course
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.FirstLineIndent = 36;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            textRange = paragraph.AppendText($"{profile.Course.CourseName}") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;

            //Appends paragraph Summary
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.FirstLineIndent = 36;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            textRange = paragraph.AppendText($"{profile.Summary}") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;

            //Appends paragraph Skills
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.FirstLineIndent = 36;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            foreach(var skill in profile.Skills)
            {
                textRange = paragraph.AppendText($"{skill.SkillName}\n") as WTextRange;
            }
            textRange.CharacterFormat.FontSize = 12f;

            //Appends paragraph Academy Experience
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.FirstLineIndent = 36;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            textRange = paragraph.AppendText($"{profile.Course.AcademyExperience}") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;

            //Appends paragraph Sparta Projects
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.FirstLineIndent = 36;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            foreach (var project in profile.SpartaProjects)
            {
                textRange = paragraph.AppendText($"{project.ProjectName}\n") as WTextRange;
                foreach(var link in project.ProjectLinks)
                {
                    textRange = paragraph.AppendText($"{link.LinkText}") as WTextRange;
                }
                textRange = paragraph.AppendText($"{project.ProjectBio}\n") as WTextRange;
            }
            textRange.CharacterFormat.FontSize = 12f;

            // Appends Paragraph Employment
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.FirstLineIndent = 36;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            foreach (var employ in profile.Employment)
            {
                textRange = paragraph.AppendText($"{employ.CompanyName}\n") as WTextRange;
                textRange = paragraph.AppendText($"{employ.Position}\n") as WTextRange;
                textRange = paragraph.AppendText($"{employ.StartDate - employ.EndDate}\n") as WTextRange;
                textRange = paragraph.AppendText($"{employ.Summary}\n") as WTextRange;
            }
            textRange.CharacterFormat.FontSize = 12f;

            // Appends Paragraph Education
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.FirstLineIndent = 36;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            foreach (var edu in profile.Education)
            {
                textRange = paragraph.AppendText($"{edu.Establishment}\n") as WTextRange;
                textRange = paragraph.AppendText($"{edu.Qualification}\n") as WTextRange;
                textRange = paragraph.AppendText($"{edu.StartDate} - {edu.EndDate}\n") as WTextRange;
                textRange = paragraph.AppendText($"{edu.Grade}\n") as WTextRange;
                foreach(var module in edu.Modules)
                {
                    textRange = paragraph.AppendText($"{module.ModuleName}\n") as WTextRange;
                    textRange = paragraph.AppendText($"{module.CourseYear}\n") as WTextRange;
                }
            }
            textRange.CharacterFormat.FontSize = 12f;
            
            // Appends Paragraph Employment
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.FirstLineIndent = 36;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            foreach (var certification in profile.Certifications)
            {
                textRange = paragraph.AppendText($"{certification.CertificationName}\n") as WTextRange;
                textRange = paragraph.AppendText($"{certification.Summary}\n") as WTextRange;
            }
            textRange.CharacterFormat.FontSize = 12f;

            // Appends Paragraph Employment
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.FirstLineIndent = 36;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            foreach (var hobby in profile.Hobbies)
            {
                textRange = paragraph.AppendText($"{hobby.HobbyName}\n") as WTextRange;
                textRange = paragraph.AppendText($"{hobby.HobbyDescription}\n") as WTextRange;
            }
            textRange.CharacterFormat.FontSize = 12f;

            // Save 
            MemoryStream stream = new MemoryStream();
            document.Save(stream, FormatType.Docx);
            stream.Position = 0;

            //Download Word document in the browser
            return File(stream, "application/msword", "Sample.docx");
        }
        

        private bool ProfileExists(int id)
        {
            return _context.Profiles.Any(e => e.ProfileId == id);
        }
    }
}
