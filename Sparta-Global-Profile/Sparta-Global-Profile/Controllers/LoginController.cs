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
        public ActionResult Authorize(Sparta_Global_Profile.Models.User userModel)
        {
            var userDetails = db.Users.Where(x => x.UserEmail == userModel.UserEmail && x.UserPassword == userModel.UserPassword).Include(x=> x.UserType).FirstOrDefault();
           
            if (userDetails == null)
            {
                ModelState.AddModelError("UserPassword", "Invalid login attempt.");
                return View("Index");
            }
                
            else 
            {
                ViewData["UserEmail"] = userDetails.UserEmail;
                HttpContext.Session.SetString("UserId", userDetails.UserId.ToString());
                if(userDetails.UserType.UserTypeName == "client") return RedirectToAction("Index", "Profile");
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
