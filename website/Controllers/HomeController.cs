using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using website.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext JobDb = new ApplicationDbContext();
        [Authorize(Roles = "HR,admin,Person")]
        public ActionResult Index()
        {
            return View(JobDb.categories.ToList());
        }
        public ActionResult Details(int JobId)
        {
            var job = JobDb.jobs.Find(JobId);

            if (job == null)
            {
                return HttpNotFound();
            }
            Session["JobId"] = JobId;
            return View(job);
        }
        [Authorize(Roles = "Person")]
        public ActionResult Apply()
        {
            return View();
        
        }
        [HttpPost]
        [Authorize(Roles = "Person")]
        public ActionResult Apply(string message)
        {
            var UserId = User.Identity.GetUserId();
            var JobId = (int)Session["JobId"];
            var check = JobDb.ApplyForJobs.Where(a =>a.JobId == JobId && a.UserId == UserId).ToList();
            if (check.Count < 1)
            {
                var Job = new ApplyForJob();
                Job.JobId = JobId;
                Job.UserId = UserId;
                Job.Message = message;
                Job.ApplyDate = DateTime.Now;

                JobDb.ApplyForJobs.Add(Job);
                JobDb.SaveChanges();
                ViewBag.Result = "The job application was successful";



            }
            else
            {
                ViewBag.Result = "You have applied for the job once before";
            
            
            
            }

            return View();
        
        }
        [Authorize(Roles = "Person")]
        public ActionResult GetJobsByUser()
        {
            var UserId = User.Identity.GetUserId();
            var Jobs = JobDb.ApplyForJobs.Where(a => a.UserId == UserId);
            return View(Jobs.ToList());
        }
        public ActionResult DetailsJob(int id)
        {
            var job = JobDb.ApplyForJobs.Find(id);

            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }
        [Authorize(Roles = "Person")]
        public ActionResult EditApplyJob(int id)
        {
            var job = JobDb.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }
        [HttpPost]
        [Authorize(Roles = "Person")]
        public ActionResult EditApplyJob(ApplyForJob job)
        {
            if (ModelState.IsValid)
            {
                job.ApplyDate = DateTime.Now;
                JobDb.Entry(job).State = EntityState.Modified;

                JobDb.SaveChanges();
                return RedirectToAction("GetJobsByUser");
            }
            return View(job);
        }
        [Authorize(Roles = "Person")]
        public ActionResult Deletejobs(int id)
        {
            var job = JobDb.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }
        [HttpPost]
        [Authorize(Roles = "Person")]
        public ActionResult Deletejobs(ApplyForJob job)
        {

            
            var myjob = JobDb.ApplyForJobs.Find(job.Id);
            JobDb.ApplyForJobs.Remove(myjob);
            JobDb.SaveChanges();
            return RedirectToAction("GetJobsByUser");

        }
        [Authorize(Roles = "HR,admin")]
        public ActionResult HrRevioion()
        {
            var UserID = User.Identity.GetUserId();

            var HRWork = from app in JobDb.ApplyForJobs
                       join job in JobDb.jobs
                       on app.JobId equals job.id                     
                       where job.User.Id == UserID
                       select app;

            var H = from j in HRWork
                    group j by j.job.JobName
                          into gr
                    select new JobsApp
                    {
                        JobName = gr.Key,
                        Items = gr
                    };

            return View(H.ToList());
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(contact contact, HttpPostedFileBase fileUploader)
        {
            var mail = new MailMessage();
            var loginInfo = new NetworkCredential("work23184@gmail.com", "*38001304*Qq");
            mail.From = new MailAddress(contact.Email);
            mail.To.Add(new MailAddress("work23184@gmail.com"));
            mail.Subject = contact.Subject;
            mail.IsBodyHtml = true;
            string body = "The sender's name: " + contact.Name + "<br>" +
                            "Sender Mail: " + contact.Email + "<br>" +
                            "sender Phone:" + contact.phone + "<br>" +
                            "The title of the message, problem, or request: " + contact.Subject + "<br>" +
                            "The text of the message: <b>" + contact.Message + "</b>";
              if (fileUploader != null)
              {
                string fileName = Path.GetFileName(fileUploader.FileName);
                mail.Attachments.Add(new Attachment(fileUploader.InputStream, fileName));
              }
            mail.Body = body;
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(mail);
            return RedirectToAction("Index");
        }
        public ActionResult search()
        {

            return View();
        }
        [HttpPost]
        public ActionResult search(string SearchHere)
        {
            var search = JobDb.jobs.Where(s => s.JobName.Contains(SearchHere)
              || s.jobdescription.Contains(SearchHere)
              || s.category.categoryName.Contains(SearchHere) || s.category.Description.Contains(SearchHere)).ToList();
            return View(search);
        }
    }
}