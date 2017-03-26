using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace MainSite.Models
{
    public class Queries
    {
        PasswordManager manager = new PasswordManager();
        public void registerMe(SystemUser model)
        {
            testDashboardContext database = new testDashboardContext();
            
                SystemUser newUser = new SystemUser() { Username = model.Username, SaltedPassword = "I am Error", Email = model.Email, IsAdmin = model.IsAdmin };
                database.SystemUsers.Add(newUser);
                try
                {
                    database.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                {

                    throw new Exception(ex.ToString());
                }
            
        }
        public bool checkMe(SystemUser model)
        {
            testDashboardContext database = new testDashboardContext();
            IQueryable<SystemUser> checkUserQuery =
                from Username in database.SystemUsers
                where Username.Username == model.Username
                select Username;
            foreach (var user in checkUserQuery)
            {
                if (user.Username == model.Username)
                {
                    return true;
                }
            }

            return false;
        }
        public KundProjectModel getProjects()
        {
            KundProjectModel model = new KundProjectModel();
            testDashboardContext database = new testDashboardContext();
            model.Kunders = database.Kunders.ToList();
            model.Projekts = database.Projekts.ToList();
            return model;
        }

        public void addProject(Projekt newProject, int customerID)
        {
            testDashboardContext database = new testDashboardContext();
            Projekt newProjekt = new Projekt()
            {
                KundID = customerID,
                Name = newProject.Name,
                Status = newProject.Status,
                TotalTid = newProject.TotalTid,
                Deadline = newProject.Deadline,
                T = newProject.T,
                UTF = newProject.UTF
            };
            database.Projekts.Add(newProjekt);
            try
            {
                database.SaveChanges();
            }
            catch ( DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
        }
        public List<Projekt> getProjectItem(int id)
        {
                testDashboardContext database = new testDashboardContext();
                List<Projekt> projectItem = new List<Projekt>();
                projectItem = database.Projekts.Where(x => x.ID == id).ToList();
                return projectItem;
        }
        public void addProjectItem(ProjektItem newItem)
        {
            testDashboardContext database = new testDashboardContext();
            ProjektItem item = new ProjektItem()
            {
                ProjektID = newItem.ProjektID,
                Name = newItem.Name,
                UserID = newItem.UserID,
                UtveckladeTimmar = newItem.UtveckladeTimmar,
                TotalTid = newItem.TotalTid,
                Deadline = newItem.Deadline
            };
            database.ProjektItems.Add(item);
            try
            {
                database.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
        }
        public void addHours(Kommentarer newComment)
        {
            testDashboardContext database = new testDashboardContext();
            Kommentarer newKommentar = new Kommentarer() {
                ItemID =  newComment.ItemID, UserID = newComment.UserID,
                AntalTimmar = newComment.AntalTimmar, Kommentar = newComment.Kommentar,
                DateCreated = newComment.DateCreated
            };
            database.Kommentarers.Add(newKommentar);
            try
            {
                database.SaveChanges();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}