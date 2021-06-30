using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using website.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(db.categories.ToList());
        }

        public ActionResult Details(int JobId)
        {
            var job = db.jobs.Find(JobId);

            if (job == null)
            {
                return HttpNotFound();
            }
            Session["JobId"] = JobId;
            return View(job);
        }
        [Authorize]
        public ActionResult Apply() 
        {


            return View();
        }
        [HttpPost]
        public ActionResult Apply (string Message)
        {
            var UserId = User.Identity.GetUserId();
            var JobId = (int)Session["JobId"];
            var check = db.JobApply.Where(a => a.JobId == JobId && a.UserId == UserId).ToList();
            if(check.Count<1)
            {
                var job = new JobApply();
                job.UserId = UserId;
                job.JobId = JobId;
                job.Message = Message;
                job.ApplyDate = DateTime.Now;
                db.JobApply.Add(job);
                db.SaveChanges();
                ViewBag.Result = "You Success Apply for this Job";

            }
            else 
            {
                ViewBag.Result = "You Had Apply for this Job Befor";


            }

            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}