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
    public class favoritesController : Controller
    {
        private auctionsEntities db = new auctionsEntities();

        // GET: favorites
        public ActionResult Index()
        {
            var favorite = db.favorite.Include(f => f.account).Include(f => f.item);
            return View(favorite.ToList());
        }

        // GET: favorites/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            favorite favorite = db.favorite.Find(id);
            if (favorite == null)
            {
                return HttpNotFound();
            }
            return View(favorite);
        }

        // GET: favorites/Create
        public ActionResult Create()
        {
            ViewBag.account_idAccount = new SelectList(db.account, "idAccount", "name");
            ViewBag.item_iditem = new SelectList(db.item, "iditem", "name");
            return View();
        }

        // POST: favorites/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idfavorite,account_idAccount,item_iditem")] favorite favorite)
        {
            if (ModelState.IsValid)
            {
                db.favorite.Add(favorite);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.account_idAccount = new SelectList(db.account, "idAccount", "name", favorite.account_idAccount);
            ViewBag.item_iditem = new SelectList(db.item, "iditem", "name", favorite.item_iditem);
            return View(favorite);
        }

        // GET: favorites/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            favorite favorite = db.favorite.Find(id);
            if (favorite == null)
            {
                return HttpNotFound();
            }
            ViewBag.account_idAccount = new SelectList(db.account, "idAccount", "name", favorite.account_idAccount);
            ViewBag.item_iditem = new SelectList(db.item, "iditem", "name", favorite.item_iditem);
            return View(favorite);
        }

        // POST: favorites/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idfavorite,account_idAccount,item_iditem")] favorite favorite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(favorite).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.account_idAccount = new SelectList(db.account, "idAccount", "name", favorite.account_idAccount);
            ViewBag.item_iditem = new SelectList(db.item, "iditem", "name", favorite.item_iditem);
            return View(favorite);
        }

        // GET: favorites/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            favorite favorite = db.favorite.Find(id);
            if (favorite == null)
            {
                return HttpNotFound();
            }
            return View(favorite);
        }

        // POST: favorites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            favorite favorite = db.favorite.Find(id);
            db.favorite.Remove(favorite);
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
