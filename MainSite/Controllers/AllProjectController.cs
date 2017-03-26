using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MainSite.Models;
using System.Threading.Tasks;

namespace MainSite.Controllers
{
    public class AllProjectController : Controller
    {
        
        Queries query = new Queries();
        // GET: AllProject
        public ActionResult Projects()
        {
            var Kundprojekts = query.getProjects();
            return View(Kundprojekts);
        }
        [HttpPost]
        public ActionResult addProject(int customerID, Projekt newProject)
        {
            query.addProject(newProject, customerID);
            var updatedProjects = query.getProjects();
            return View("Projects", updatedProjects);
        }

        [HttpPost]
        public PartialViewResult ItemTable(int id)
        {
            //hämta från DB vars ID stämmer med projektItem, spara i en lista, skicka ut i view
            var projektItems = query.getProjectItem(id);
            return PartialView("ItemTable", projektItems);
        }
        [HttpPost]
        public ActionResult addItem(ProjektItem newItem, int id)
        {
            query.addProjectItem(newItem);
            var updatedProjektItems = query.getProjectItem(id);
            return PartialView("ItemTable", updatedProjektItems);
        }
        [HttpPost]
        public PartialViewResult ItemTableAddComment(Kommentarer newComment, int id)
        {
            //sparar ny kommentar i DB och sedan skickar ut den updaterade tabellen
            query.addHours(newComment);
            var updatedProjektItems = query.getProjectItem(id);
            return PartialView("ItemTable", updatedProjektItems);
        }
    }
}