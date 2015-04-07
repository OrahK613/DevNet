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
    public class SoftwareSpecialtyController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SoftwareSpecialty
        public ActionResult Index()
        {
            return View(db.SoftwareSpecialties.ToList());
        }

        // GET: SoftwareSpecialty/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SoftwareSpecialty softwareSpecialty = db.SoftwareSpecialties.Find(id);
            if (softwareSpecialty == null)
            {
                return HttpNotFound();
            }
            return View(softwareSpecialty);
        }

        // GET: SoftwareSpecialty/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SoftwareSpecialty/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SoftwareSpecialtyID,SoftwareSpecialtyName")] SoftwareSpecialty softwareSpecialty)
        {
            if (ModelState.IsValid)
            {
                db.SoftwareSpecialties.Add(softwareSpecialty);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(softwareSpecialty);
        }

        // GET: SoftwareSpecialty/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SoftwareSpecialty softwareSpecialty = db.SoftwareSpecialties.Find(id);
            if (softwareSpecialty == null)
            {
                return HttpNotFound();
            }
            return View(softwareSpecialty);
        }

        // POST: SoftwareSpecialty/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SoftwareSpecialtyID,SoftwareSpecialtyName")] SoftwareSpecialty softwareSpecialty)
        {
            if (ModelState.IsValid)
            {
                db.Entry(softwareSpecialty).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(softwareSpecialty);
        }

        // GET: SoftwareSpecialty/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SoftwareSpecialty softwareSpecialty = db.SoftwareSpecialties.Find(id);
            if (softwareSpecialty == null)
            {
                return HttpNotFound();
            }
            return View(softwareSpecialty);
        }

        // POST: SoftwareSpecialty/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SoftwareSpecialty softwareSpecialty = db.SoftwareSpecialties.Find(id);
            db.SoftwareSpecialties.Remove(softwareSpecialty);
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
