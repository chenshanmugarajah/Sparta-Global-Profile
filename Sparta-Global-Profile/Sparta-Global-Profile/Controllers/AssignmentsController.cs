using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sparta_Global_Profile.Models;

namespace Sparta_Global_Profile.Controllers
{
    public class AssignmentsController : Controller
    {
        private readonly SpartaGlobalProfileDbContext _context;

        public AssignmentsController(SpartaGlobalProfileDbContext context)
        {
            _context = context;
        }

        // GET: Assignments
        public async Task<IActionResult> Index(int? id)
        {
            HttpContext context = HttpContext;
            var userId = context.Session.GetString("UserId");
            var userTypeId = context.Session.GetString("UserTypeId");
            var profileId = context.Session.GetString("ProfileId");

            ViewData["Type"] = "Student";
            if (id != null)
            {
                if ((userTypeId == "1" && id.ToString() != profileId))
                {
                    return RedirectToAction("Index", "Assignments", new { id = Int32.Parse(profileId) });
                }
                var spartaGlobalProfileDbContext = _context.Assignments.Where(s => s.ProfileId == id).Include(s => s.Profile);
                ViewData["ProfileId"] = id;
                ViewData["ProfileName"] = (_context.Profiles.Where(p => p.ProfileId == id).First()).ProfileName;
                return View(await spartaGlobalProfileDbContext.ToListAsync());
            }
            else
            {
                var spartaGlobalProfileDbContext = _context.Assignments.Include(s => s.Profile);
                return await RedirectByUserType(View(await spartaGlobalProfileDbContext.ToListAsync()));
            }
        }
        //public async Task<IActionResult> RedirectByUserType(string userId, string userTypeId, string profileId, IIncludableQueryable<out TEntity, out TProfile)
        public async Task<IActionResult> RedirectByUserType(ViewResult view)
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
                return RedirectToAction("Index", "Assignments", new { id = Int32.Parse(profileId) });
            }
            if (userTypeId == "2")
            {
                return RedirectToAction("Index", "Profiles");
            }
            else
            {
                return view;
            }
        }

        // GET: Assignments/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var assignment = await _context.Assignments
        //        .Include(a => a.Profile)
        //        .FirstOrDefaultAsync(m => m.AssignmentId == id);
        //    if (assignment == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(assignment);
        //}

        // GET: Assignments/Create
        public IActionResult Create(int? id)
        {
            if (id != null)
            {
                ViewData["ProfileId"] = new SelectList(_context.Profiles.Where(p => p.ProfileId == id), "ProfileId", "ProfileName");
                ViewData["Profile"] = id.ToString();
            }
            else
            {
                ViewData["ProfileId"] = new SelectList(_context.Profiles, "ProfileId", "ProfileName");
                ViewData["Profile"] = "0";
            }

            return View();
        }

        // POST: Assignments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AssignmentId,StartDate,EndDate,CompanyName,Position,Summary,ProfileId")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assignment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Assignments", new { id = assignment.ProfileId });
            }
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "ProfileId", "ProfileId", assignment.ProfileId);
            return View(assignment);
        }

        // GET: Assignments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignments.FindAsync(id);
            if (assignment == null)
            {
                return NotFound();
            }

            ViewData["ProfileId"] = new SelectList(_context.Profiles.Where(p => p.ProfileId == assignment.ProfileId), "ProfileId", "ProfileName", assignment.ProfileId);
            ViewData["Profile"] = _context.Profiles.Where(p => p.ProfileId == assignment.ProfileId).First();
            return View(assignment);
        }

        // POST: Assignments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AssignmentId,StartDate,EndDate,CompanyName,Position,Summary,ProfileId")] Assignment assignment)
        {
            if (id != assignment.AssignmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssignmentExists(assignment.AssignmentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Assignments", new { id = assignment.ProfileId });
            }
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "ProfileId", "ProfileId", assignment.ProfileId);
            return View(assignment);
        }

        // GET: Assignments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignments
                .Include(a => a.Profile)
                .FirstOrDefaultAsync(m => m.AssignmentId == id);
            if (assignment == null)
            {
                return NotFound();
            }

            ViewData["Profile"] = _context.Profiles.Where(p => p.ProfileId == assignment.ProfileId).First();
            return View(assignment);
        }

        // POST: Assignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assignment = await _context.Assignments.FindAsync(id);
            _context.Assignments.Remove(assignment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Assignments", new { id = assignment.ProfileId });
        }

        private bool AssignmentExists(int id)
        {
            return _context.Assignments.Any(e => e.AssignmentId == id);
        }


    }
}
