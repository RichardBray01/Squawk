using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Squawk.Models;

namespace Squawk.Controllers
{
    public class SampleTypesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: SampleTypes
        public ActionResult Index()
        {
            return View(db.SampleTypes.ToList());
        }

        // GET: SampleTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SampleType sampleType = db.SampleTypes.Find(id);
            if (sampleType == null)
            {
                return HttpNotFound();
            }
            return View(sampleType);
        }

        // GET: SampleTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SampleTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] SampleType sampleType)
        {
            if (ModelState.IsValid)
            {
                db.SampleTypes.Add(sampleType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sampleType);
        }

        // GET: SampleTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SampleType sampleType = db.SampleTypes.Find(id);
            if (sampleType == null)
            {
                return HttpNotFound();
            }
            return View(sampleType);
        }

        // POST: SampleTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] SampleType sampleType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sampleType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sampleType);
        }

        // GET: SampleTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SampleType sampleType = db.SampleTypes.Find(id);
            if (sampleType == null)
            {
                return HttpNotFound();
            }
            return View(sampleType);
        }

        // POST: SampleTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SampleType sampleType = db.SampleTypes.Find(id);
            db.SampleTypes.Remove(sampleType);
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
