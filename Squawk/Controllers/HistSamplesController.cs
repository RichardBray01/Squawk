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
    public class HistSamplesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: HistSamples
        public ActionResult Index()
        {
            var histSamples = db.HistSamples.Include(h => h.Host).Include(h => h.SampleType);
            return View(histSamples.ToList());
        }

        // GET: HistSamples/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistSample histSample = db.HistSamples.Find(id);
            if (histSample == null)
            {
                return HttpNotFound();
            }
            return View(histSample);
        }

        // GET: HistSamples/Create
        public ActionResult Create()
        {
            ViewBag.HostId = new SelectList(db.Hosts, "Id", "Name");
            ViewBag.SampleTypeId = new SelectList(db.SampleTypes, "Id", "Name");
            return View();
        }

        // POST: HistSamples/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,HostId,SampleTypeId,dtSample,dbValue")] HistSample histSample)
        {
            if (ModelState.IsValid)
            {
                db.HistSamples.Add(histSample);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HostId = new SelectList(db.Hosts, "Id", "Name", histSample.HostId);
            ViewBag.SampleTypeId = new SelectList(db.SampleTypes, "Id", "Name", histSample.SampleTypeId);
            return View(histSample);
        }

        // GET: HistSamples/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistSample histSample = db.HistSamples.Find(id);
            if (histSample == null)
            {
                return HttpNotFound();
            }
            ViewBag.HostId = new SelectList(db.Hosts, "Id", "Name", histSample.HostId);
            ViewBag.SampleTypeId = new SelectList(db.SampleTypes, "Id", "Name", histSample.SampleTypeId);
            return View(histSample);
        }

        // POST: HistSamples/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HostId,SampleTypeId,dtSample,dbValue")] HistSample histSample)
        {
            if (ModelState.IsValid)
            {
                db.Entry(histSample).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HostId = new SelectList(db.Hosts, "Id", "Name", histSample.HostId);
            ViewBag.SampleTypeId = new SelectList(db.SampleTypes, "Id", "Name", histSample.SampleTypeId);
            return View(histSample);
        }

        // GET: HistSamples/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistSample histSample = db.HistSamples.Find(id);
            if (histSample == null)
            {
                return HttpNotFound();
            }
            return View(histSample);
        }

        // POST: HistSamples/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HistSample histSample = db.HistSamples.Find(id);
            db.HistSamples.Remove(histSample);
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
