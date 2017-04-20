using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using Auctions.Models;

namespace Auctions.Controllers
{
    public class HomeController : Controller
    {
        private auctionsEntities db = new auctionsEntities();

        public ActionResult index()
        {
            return View();
        }

        public ActionResult shop()
        {
            ViewBag.Message = "Your application description page.";
            var item = db.item.Include(i => i.category);
            return View(item.ToList());
        }


        // GET: accounts/Create
        public ActionResult registr()
        {
            ViewBag.idAccount = new SelectList(db.ban, "account_idAccount", "date");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult registr([Bind(Include = "idAccount,name,surname,password,nick,icon,promo,age")] account account)
        {
            if (ModelState.IsValid)
            {
                db.account.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idAccount = new SelectList(db.ban, "account_idAccount", "date", account.idAccount);
            return View(account);
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
                        Session["LoggedAs"] = v.nick.ToString();
                        return RedirectToAction("shop");
                    }

                }

            }
            ViewBag.Message = "Niepoprawny login lub hasło";
            return View(a);

        }
        public ActionResult LogOut()
        {
            Session.Clear(); 
            return RedirectToAction("index");
        }

    }
}

