using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace website.Controllers
{
    
    public class AdministrationController : Controller
    {
        ApplicationDbContext Admins = new ApplicationDbContext();
        // GET: Administration
        public ActionResult Index()
        {
            return View(Admins.Roles.ToList());
        }

        // GET: Administration/Details/5
        public ActionResult Details(string id)
        {
            var admin = Admins.Roles.Find(id);
            if (admin == null)
            {
                return HttpNotFound();

            }
            return View(admin);
        }

        // GET: Administration/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administration/Create
        [HttpPost]
        public ActionResult Create(IdentityRole admin)
        {

            // TODO: Add insert logic here
            if (ModelState.IsValid)
            {
                Admins.Roles.Add(admin);
                Admins.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin);
        }


        // GET: Administration/Edit/5
        public ActionResult Edit(string id)
        {
            var admin = Admins.Roles.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Administration/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include= "Id,Name")] IdentityRole admin)
        {
           if(ModelState.IsValid)
            {
                Admins.Entry(admin).State = EntityState.Modified;
                Admins.SaveChanges();
                return RedirectToAction("index");


            }
            return View(admin);
        }

        // GET: Administration/Delete/5
        public ActionResult Delete(string id)
        {
            var admin = Admins.Roles.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Administration/Delete/5
        [HttpPost]
        public ActionResult Delete(IdentityRole admin)
        {

                // TODO: Add delete logic here
                var ad= Admins.Roles.Find(admin.Id);
                Admins.Roles.Remove(ad);
                Admins.SaveChanges();
                return RedirectToAction("Index");
            
        }
    }
}
