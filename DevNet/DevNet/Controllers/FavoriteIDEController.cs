using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DevNet.Models;

namespace DevNet.Controllers
{
    public class FavoriteIDEController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FavoriteIDE
        public ActionResult Index()
        {
            return View(db.FavoriteIDEs.ToList());
        }

        // GET: FavoriteIDE/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FavoriteIDE favoriteIDE = db.FavoriteIDEs.Find(id);
            if (favoriteIDE == null)
            {
                return HttpNotFound();
            }
            return View(favoriteIDE);
        }

        // GET: FavoriteIDE/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FavoriteIDE/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FavoriteIDEID,FavoriteIDEName")] FavoriteIDE favoriteIDE)
        {
            if (ModelState.IsValid)
            {
                db.FavoriteIDEs.Add(favoriteIDE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(favoriteIDE);
        }

        // GET: FavoriteIDE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FavoriteIDE favoriteIDE = db.FavoriteIDEs.Find(id);
            if (favoriteIDE == null)
            {
                return HttpNotFound();
            }
            return View(favoriteIDE);
        }

        // POST: FavoriteIDE/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FavoriteIDEID,FavoriteIDEName")] FavoriteIDE favoriteIDE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(favoriteIDE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(favoriteIDE);
        }

        // GET: FavoriteIDE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FavoriteIDE favoriteIDE = db.FavoriteIDEs.Find(id);
            if (favoriteIDE == null)
            {
                return HttpNotFound();
            }
            return View(favoriteIDE);
        }

        // POST: FavoriteIDE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FavoriteIDE favoriteIDE = db.FavoriteIDEs.Find(id);
            db.FavoriteIDEs.Remove(favoriteIDE);
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
