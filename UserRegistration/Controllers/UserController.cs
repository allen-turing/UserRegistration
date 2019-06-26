using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserRegistration.Models;
namespace UserRegistration.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult AddOrEdit(int id=0)
        {
            User userModel = new User();    
            return View(userModel);
        }

        // Post: User
        [HttpPost]
        public ActionResult AddOrEdit(User userModel)
        {
            using (DbbModels dbModel = new DbbModels())
            {
                if (dbModel.Users.Any(x => x.Username == userModel.Username))
                {
                    ViewBag.DuplicateMessage = "User Name Already Exist";
                    return View("AddOrEdit", userModel);
                }
                dbModel.Users.Add(userModel);
                dbModel.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registration Successful.";
            return View("AddOrEdit", new User()); 
        }
    }
}