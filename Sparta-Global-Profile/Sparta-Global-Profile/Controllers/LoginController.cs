using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Sparta_Global_Profile.Models;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing;

namespace Sparta_Global_Profile.Controllers
{
    public class LoginController : Controller
    {
        SpartaGlobalProfileDbContext db = new SpartaGlobalProfileDbContext();

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Authorize(User userModel)
        {
            var user = db.Users.Where(x => x.UserEmail == userModel.UserEmail).FirstOrDefault();

            if (user == null)
            {
                ModelState.AddModelError("UserEmail", "User with that Email does not exist!");
                return View("Index");
            }
            var profile = db.Profiles.Where(p => p.UserId == user.UserId).FirstOrDefault();
            var password = user.UserPassword;
            var decryptedPassword = Helper.DecryptCipherTextToPlainText(password);

            if ((userModel.UserPassword != decryptedPassword))
            {
                ModelState.AddModelError("UserPassword", "Incorrect Password");
                return View("Index");
            }
                
            else 
            {
                ViewData["UserEmail"] = user.UserEmail;
                HttpContext.Session.SetString("UserId", user.UserId.ToString());
                HttpContext.Session.SetString("UserTypeId", user.UserTypeId.ToString());
                HttpContext.Session.SetString("UserEmail", user.UserEmail);
                if(profile != null)
                {
                    HttpContext.Session.SetString("ProfileId", profile.ProfileId.ToString());
                }

                if (user.UserTypeId == 2 || user.UserTypeId == 3 || user.UserTypeId == 4 || user.UserTypeId == 5)
                {
                    return RedirectToAction("Index", "Profile");
                }
                if (user.UserTypeId == 1)
                {
                    var routeId = profile.ProfileId;
                    return RedirectToAction("Details", "Profile", new { id = routeId  });
                }
                

                return View("Index");
            }
        }


        public  IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }
        public void ValidationMessage(string key, string alert, string value)
        {
            try
            {
                TempData.Remove(key);
                TempData.Add(key, value);
                TempData.Add("alertType", alert);
            }
            catch
            {
                Debug.WriteLine("TempDataMessage Error");
            }
 
        }
    }
}
