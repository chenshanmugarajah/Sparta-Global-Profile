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
    public class SkillsController : Controller
    {
        private readonly SpartaGlobalProfileDbContext _context;

        public SkillsController(SpartaGlobalProfileDbContext context)
        {
            _context = context;
        }

        // GET: Skills
        public async Task<IActionResult> Index()
        {
            HttpContext context = HttpContext;
            var profileId = Int32.Parse(context.Session.GetString("ProfileId"));

            var spartaGlobalProfileDbContext = _context.Skills.Where(s => s.ProfileId == profileId).Include(s => s.Profile);
            return View(await spartaGlobalProfileDbContext.ToListAsync());
        }

        // GET: Skills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skill = await _context.Skills
                .Include(s => s.Profile)
                .FirstOrDefaultAsync(m => m.SkillId == id);
            if (skill == null)
            {
                return NotFound();
            }

            return View(skill);
        }

        // GET: Skills/Create
        public IActionResult Create()
        {
            //ViewData["ProfileId"] = new SelectList(_context.Profiles, "ProfileId", "ProfileId");
            return View();
        }

        // POST: Skills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int SkillId, string SkillName)
        {
            //public async Task<IActionResult> Create([Bind("SkillId,SkillName,ProfileId")] Skill skill)
            HttpContext context = HttpContext;
            var profileId = Int32.Parse(context.Session.GetString("ProfileId"));

            Skill skill = new Skill()
            {
                SkillId = SkillId,
                SkillName = SkillName,
                ProfileId = profileId
            };

            if (ModelState.IsValid)
            {
                _context.Add(skill);
                await _context.SaveChangesAsync();
                return Redirect("~/profile");
            }
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "ProfileId", "ProfileId", skill.ProfileId);
            return View(skill);
        }

        // GET: Skills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skill = await _context.Skills.FindAsync(id);
            if (skill == null)
            {
                return NotFound();
            }
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "ProfileId", "ProfileId", skill.ProfileId);
            return View(skill);
        }

        // POST: Skills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int SkillId, string SkillName)
        {
            //public async Task<IActionResult> Create([Bind("SkillId,SkillName,ProfileId")] Skill skill)
            HttpContext context = HttpContext;
            var profileId = Int32.Parse(context.Session.GetString("ProfileId"));

            Skill skill = new Skill()
            {
                SkillId = SkillId,
                SkillName = SkillName,
                ProfileId = profileId
            };

            if (id != skill.SkillId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(skill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkillExists(skill.SkillId))
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
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "ProfileId", "ProfileId", skill.ProfileId);
            return View(skill);
        }

        // GET: Skills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skill = await _context.Skills
                .Include(s => s.Profile)
                .FirstOrDefaultAsync(m => m.SkillId == id);
            if (skill == null)
            {
                return NotFound();
            }

            return View(skill);
        }

        // POST: Skills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SkillExists(int id)
        {
            return _context.Skills.Any(e => e.SkillId == id);
        }
    }
}
