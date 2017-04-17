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
    public class sellsController : Controller
    {
        private auctionsEntities db = new auctionsEntities();

        // GET: sells
        public ActionResult Index()
        {
            var sell = db.sell.Include(s => s.account).Include(s => s.item);
            return View(sell.ToList());
        }

        // GET: sells/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sell sell = db.sell.Find(id);
            if (sell == null)
            {
                return HttpNotFound();
            }
            return View(sell);
        }

        // GET: sells/Create
        public ActionResult Create()
        {
            ViewBag.account_idAccount = new SelectList(db.account, "idAccount", "name");
            ViewBag.item_iditem = new SelectList(db.item, "iditem", "name");
            return View();
        }

        // POST: sells/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "account_idAccount,item_iditem,date")] sell sell)
        {
            if (ModelState.IsValid)
            {
                db.sell.Add(sell);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.account_idAccount = new SelectList(db.account, "idAccount", "name", sell.account_idAccount);
            ViewBag.item_iditem = new SelectList(db.item, "iditem", "name", sell.item_iditem);
            return View(sell);
        }

        // GET: sells/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sell sell = db.sell.Find(id);
            if (sell == null)
            {
                return HttpNotFound();
            }
            ViewBag.account_idAccount = new SelectList(db.account, "idAccount", "name", sell.account_idAccount);
            ViewBag.item_iditem = new SelectList(db.item, "iditem", "name", sell.item_iditem);
            return View(sell);
        }

        // POST: sells/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "account_idAccount,item_iditem,date")] sell sell)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sell).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.account_idAccount = new SelectList(db.account, "idAccount", "name", sell.account_idAccount);
            ViewBag.item_iditem = new SelectList(db.item, "iditem", "name", sell.item_iditem);
            return View(sell);
        }

        // GET: sells/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sell sell = db.sell.Find(id);
            if (sell == null)
            {
                return HttpNotFound();
            }
            return View(sell);
        }

        // POST: sells/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sell sell = db.sell.Find(id);
            db.sell.Remove(sell);
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
