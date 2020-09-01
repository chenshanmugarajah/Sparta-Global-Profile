using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sparta_Global_Profile.Models;
using Sparta_Global_Profile.ViewModel;

namespace Sparta_Global_Profile.Controllers
{
    public class FullProfileController : Controller
    {
        public IActionResult Index()
        {
            SpartaGlobalProfileDbContext spartaGlobal = new SpartaGlobalProfileDbContext();
            var myModel = new FullProfile();
            myModel.Users = spartaGlobal.Users.ToList();
            myModel.Profiles = spartaGlobal.Profiles.ToList();
            ViewBag.Users = new SelectList(spartaGlobal.Users.ToList());
            return View(myModel);
        }
    }
}
