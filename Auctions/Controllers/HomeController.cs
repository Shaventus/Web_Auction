using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Auctions.Models;

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

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(account a)
        {
            if (ModelState.IsValid)
            {
                using (auctionsEntities db = new auctionsEntities())
                {
                    var v = db.account.Where(u => u.nick.Equals(a.nick) && u.password.Equals(a.password)).FirstOrDefault();
                    if (v != null)
                    {
                        Session["LogedUserID"] = v.idAccount.ToString();
                        Session["LoggesAs"] = v.nick.ToString();
                        return RedirectToAction("Index");
                    }

                }

            }
            return View(a);
        }
        public ActionResult AfterLoged()
        {
            return View();

        }

      
    }
}