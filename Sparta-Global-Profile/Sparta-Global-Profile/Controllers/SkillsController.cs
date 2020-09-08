﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IActionResult> Index(int? id)
        {
            ViewData["Type"] = "Student";
            if (id != null)
            {
                var spartaGlobalProfileDbContext = _context.Skills.Where(s => s.ProfileId == id).Include(s => s.Profile);
                ViewData["ProfileId"] = id;
                ViewData["ProfileName"] = (_context.Profiles.Where(p => p.ProfileId == id).First()).ProfileName;
                return View(await spartaGlobalProfileDbContext.ToListAsync());
            } else
            {
                ViewData["Type"] = "All";
                var spartaGlobalProfileDbContext = _context.Skills.Include(s => s.Profile);
                return View(await spartaGlobalProfileDbContext.ToListAsync());
            }
        }

        // GET: Skills/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var skill = await _context.Skills
        //        .Include(s => s.Profile)
        //        .FirstOrDefaultAsync(m => m.SkillId == id);
        //    if (skill == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(skill);
        //}

        // GET: Skills/Create
        public IActionResult Create(int? id)
        {
            if (id != null)
            {
                ViewData["ProfileId"] = new SelectList(_context.Profiles.Where(p => p.ProfileId == id), "ProfileId", "ProfileName");
                ViewData["Profile"] = id.ToString();
            } else
            {
                ViewData["ProfileId"] = new SelectList(_context.Profiles, "ProfileId", "ProfileName");
                ViewData["Profile"] = "0";
            }
            return View();
        }

        // POST: Skills/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SkillId,SkillName,ProfileId")] Skill skill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(skill);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Skills", new { id = skill.ProfileId });
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

            ViewData["ProfileId"] = new SelectList(_context.Profiles.Where(p => p.ProfileId == skill.ProfileId), "ProfileId", "ProfileName", skill.ProfileId);
            ViewData["Profile"] = _context.Profiles.Where(p => p.ProfileId == skill.ProfileId).First();
            return View(skill);
        }

        // POST: Skills/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SkillId,SkillName,ProfileId")] Skill skill)
        {
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
                return RedirectToAction("Index", "Skills", new { id = skill.ProfileId });
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

            ViewData["Profile"] = _context.Profiles.Where(p => p.ProfileId == skill.ProfileId).First();
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
            return RedirectToAction("Index", "Skills", new { id = skill.ProfileId });
        }

        private bool SkillExists(int id)
        {
            return _context.Skills.Any(e => e.SkillId == id);
        }
    }
}
