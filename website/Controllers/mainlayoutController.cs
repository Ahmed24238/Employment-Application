using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace website.Controllers
{
    public class mainlayoutController : Controller
    {
        // GET: mainlayout
        public ActionResult Index()
        {
            return View();
        }
    }
}