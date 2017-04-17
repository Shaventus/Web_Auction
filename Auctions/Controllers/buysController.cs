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
    public class buysController : Controller
    {
        private auctionsEntities db = new auctionsEntities();

        // GET: buys
        public ActionResult Index()
        {
            var buy = db.buy.Include(b => b.account).Include(b => b.item);
            return View(buy.ToList());
        }

        // GET: buys/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            buy buy = db.buy.Find(id);
            if (buy == null)
            {
                return HttpNotFound();
            }
            return View(buy);
        }

        // GET: buys/Create
        public ActionResult Create()
        {
            ViewBag.account_idAccount = new SelectList(db.account, "idAccount", "name");
            ViewBag.item_iditem = new SelectList(db.item, "iditem", "name");
            return View();
        }

        // POST: buys/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "account_idAccount,item_iditem,date")] buy buy)
        {
            if (ModelState.IsValid)
            {
                db.buy.Add(buy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.account_idAccount = new SelectList(db.account, "idAccount", "name", buy.account_idAccount);
            ViewBag.item_iditem = new SelectList(db.item, "iditem", "name", buy.item_iditem);
            return View(buy);
        }

        // GET: buys/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            buy buy = db.buy.Find(id);
            if (buy == null)
            {
                return HttpNotFound();
            }
            ViewBag.account_idAccount = new SelectList(db.account, "idAccount", "name", buy.account_idAccount);
            ViewBag.item_iditem = new SelectList(db.item, "iditem", "name", buy.item_iditem);
            return View(buy);
        }

        // POST: buys/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "account_idAccount,item_iditem,date")] buy buy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(buy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.account_idAccount = new SelectList(db.account, "idAccount", "name", buy.account_idAccount);
            ViewBag.item_iditem = new SelectList(db.item, "iditem", "name", buy.item_iditem);
            return View(buy);
        }

        // GET: buys/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            buy buy = db.buy.Find(id);
            if (buy == null)
            {
                return HttpNotFound();
            }
            return View(buy);
        }

        // POST: buys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            buy buy = db.buy.Find(id);
            db.buy.Remove(buy);
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
