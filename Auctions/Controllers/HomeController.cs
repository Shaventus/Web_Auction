﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using Auctions.Models;
using System.Net.Mail;

namespace Auctions.Controllers
{
    public class HomeController : Controller
    {
        private auctionsEntities db = new auctionsEntities();


        public ActionResult index(String searchString)
        {
          
            return View();
        }
        public ActionResult white()
        {
            return View();
        }
        public ActionResult question ()
        {
            return View();
        }
        public ActionResult  regulations()
        {
            return View();
        }
        public ActionResult black()
        {
            return View();
        } 

        public ActionResult Chat()
        {
            return View();
        }

        public ActionResult MyAuctions()
        {
            var sell = db.sell.Include(s => s.account).Include(s => s.item);
            return View(sell.ToList());
        }

        public ActionResult observed()
        {
            var favorite = db.favorite.Include(f => f.account).Include(f => f.item);
            return View(favorite.ToList());
        }

        public ActionResult ebooks()
        {
            return View();
        }

        public ActionResult payu()
        {
            return View();
        }

        public ActionResult bought()
        {
            var buy = db.buy.Include(s => s.account).Include(s => s.item);
            return View(buy.ToList());
        }

        public ActionResult comments()
        {
            return View();
        }

        public ActionResult shoppingSettings()
        {
            return View();
        }
     
            
        public ActionResult adminaccounts()
        {
            var account = db.account.Include(a => a.ban);
            return View(account.ToList());
        }

        public ActionResult shop(String name, String category)
        {

            var item = from s in db.item
                       select s;
            item = db.item.Include(i => i.category);
            ViewBag.category = new SelectList(db.category, "idcategory", "name");
            if (!String.IsNullOrEmpty(name))
            {
                item = item.Where(c => c.name.Contains(name));

                if (!String.IsNullOrEmpty(category))
                {
                    int cat = Convert.ToInt32(category);
                    return View(item.Where(x => x.category_idcategory == cat));
                }
                else
                {
                    return View(item);
                }
            }
            else return View(item);

        }

        public ActionResult profile()
        {
            if (Session["LogedUserID"] != null)
            {
                int id = int.Parse((String)Session["LogedUserID"]);
                if (id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                account account = db.account.Find(id);
                if (account == null)
                {
                    return HttpNotFound();
                }
                ViewBag.idAccount = new SelectList(db.ban, "account_idAccount", "date", account.idAccount);
                return View(account);
            }
            return View();
        }

        public ActionResult favorites()
        {
            var favorite = db.favorite.Include(f => f.account).Include(f => f.item);
            return View(favorite.ToList());
        }

        public ActionResult cart(int id)
        {
            if (id != 0)
            {
                if (Session["cart"] == null)
                {
                    List<item> cart = new List<item>();
                    cart.Add(db.item.Find(id));
                    Session["cart"] = cart;
                }
                else
                {
                    List<item> cart = (List<item>)Session["cart"];
                    cart.Add(db.item.Find(id));
                    Session["cart"] = cart;
                }
            }
             return View((List<item>)Session["cart"]);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult profile([Bind(Include = "idAccount,name,surname,password,nick,icon,promo,age")] account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idAccount = new SelectList(db.ban, "account_idAccount", "date", account.idAccount);
            return View(account);
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
                role role = db.role.Find(2);
                role.account.Add(account);
                db.SaveChangesAsync();
                
                db.SaveChanges();
                SmtpClient client = new SmtpClient();
                client.Credentials = new NetworkCredential("userName", "password");
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                String from = "someone@somewhere.com";
                String to = account.icon;
                MailMessage mailMessage = new MailMessage(from, to); 
                mailMessage.Subject = "Hello There";
                mailMessage.Body = "Hello my friend!";
                client.Send(mailMessage);
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
                        Session["RoleId"] = v.role.First().idrole;
                        

                        int r = int.Parse(Session["RoleId"].ToString());

                        if(r == 2)
                        {
                            return RedirectToAction("profile");
                        } else if( r == 1)
                        {
                            return RedirectToAction("adminaccounts");
                        }
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

