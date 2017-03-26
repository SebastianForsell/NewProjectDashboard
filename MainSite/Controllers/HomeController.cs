using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MainSite.Models;

namespace MainSite.Controllers
{
    public class HomeController : Controller
    {
        private testDashboardContext database = new testDashboardContext();
        // GET: Home
        public ActionResult Index()
        {
            return View(database.SystemUsers.ToList());
        }
    }
}