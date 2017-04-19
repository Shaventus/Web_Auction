using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auctions.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult index()
        {
            return View();
        }

        public ActionResult shop()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        public ActionResult login()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult blog_single()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult blog()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult cart()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult checkout()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult contact_us()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult product_details()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}