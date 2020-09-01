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
            var password = db.Users.Where(u => u.UserEmail == userModel.UserEmail).Select(u => u.UserPassword).FirstOrDefault();
            var decryptedPassword = Helper.DecryptCipherTextToPlainText(password);
            var userDetails = db.Users.Where(x => x.UserEmail == userModel.UserEmail).FirstOrDefault();
            if (userDetails == null || (userModel.UserPassword != decryptedPassword))
            {
                ModelState.AddModelError("UserEmail", "Invalid login attempt.");
                return View("Index");
            }
                
            else 
            {
                HttpContext.Session.SetString("UserId", userDetails.UserEmail); 
                return RedirectToAction("Index", "Home");
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
