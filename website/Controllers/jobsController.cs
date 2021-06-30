using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using website.Models;
using System.IO;
using Microsoft.AspNet.Identity;

namespace website.Controllers
{
    
    public class jobsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: jobs
        public ActionResult Index()
        {
            var jobs = db.jobs.Include(j => j.category);
            return View(jobs.ToList());
        }

        // GET: jobs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            jobs jobs = db.jobs.Find(id);
            if (jobs == null)
            {
                return HttpNotFound();
            }
            return View(jobs);
        }

        // GET: jobs/Create
        public ActionResult Create()
        {
            ViewBag.categoryid = new SelectList(db.categories, "id", "categoryName");
            return View();
        }

        // POST: jobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( jobs job, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string path=Path.Combine(Server.MapPath("~/Uploads"),upload.FileName);
                upload.SaveAs(path);
                job.Jobphoto = upload.FileName;
                job.UserId = User.Identity.GetUserId();
                db.jobs.Add(job);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.categoryid = new SelectList(db.categories, "id", "categoryName", job.categoryid);
            return View(job);
        }

        // GET: jobs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            jobs jobs = db.jobs.Find(id);
            if (jobs == null)
            {
                return HttpNotFound();
            }
            ViewBag.categoryid = new SelectList(db.categories, "id", "categoryName", jobs.categoryid);
            return View(jobs);
        }

        // POST: jobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(jobs job, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string oldpath = Path.Combine(Server.MapPath("~/Uploads"), job.Jobphoto);
                if (upload != null)
                {
                    System.IO.File.Delete(oldpath);
                    string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                    upload.SaveAs(path);
                    job.Jobphoto = upload.FileName;
                }
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categoryid = new SelectList(db.categories, "id", "categoryName", job.categoryid);
            return View(job);
        }

        // GET: jobs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            jobs jobs = db.jobs.Find(id);
            if (jobs == null)
            {
                return HttpNotFound();
            }
            return View(jobs);
        }

        // POST: jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            jobs jobs = db.jobs.Find(id);
            db.jobs.Remove(jobs);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
