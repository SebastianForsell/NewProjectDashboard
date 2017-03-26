using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MainSite.Models;

namespace MainSite.Controllers
{
    public class RegisterController : Controller
    {
        SystemUser model = new SystemUser();
        private Queries queries = new Queries();
        // GET: Register
        public ActionResult Register()
        {
            return View(model);
        }
        [HttpPost]
        public ActionResult Register(SystemUser model)
        {
            if (checkRegisterBoxes(model))
            {
                return View(model);
            }
            if (queries.checkMe(model))
            {
                ViewBag.UserExist = "Användaren finns redan!";
                return View(model);
            }
            queries.registerMe(model);
            ViewBag.Success = "Användare registrerad!";
            return View(model);
        }

        private bool checkRegisterBoxes(SystemUser model)
        {
            if (model.Username != null)
            {
                if (model.Username.Length <= 2 || model.Username.Length >= 50)
                {
                    ViewBag.UsernameError = "Username must be 3 - 50 characters long!";
                    return true;
                }
            }
            else
            {
                ViewBag.UsernameError = "Username cannot be empty!";
                return true;
            }
            if (model.SaltedPassword != null)
            {
                if (model.SaltedPassword.Length <= 9 || model.SaltedPassword.Length > 25)
                {
                    ViewBag.PasswordError = "Password must be 10 - 25 characters long!";
                    return true;
                }
            }
            else
            {
                ViewBag.PasswordError = "Password cannot be empty!";
                return true;
            }
            if (model.Email != null)
            {
                if (!model.Email.Contains("@") && !model.Email.Contains("."))
                {
                    ViewBag.EmailError = "Email not in correct format!";
                    return true;
                }
            }
            else
            {
                ViewBag.EmailError = "Email cannot be empty!";
                return true;
            }
            return false;
        }
    }
}