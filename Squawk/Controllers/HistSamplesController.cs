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

        static Random r = new Random();

        // GET: HistSamples
        public ActionResult Index()
        {
            var histSamples = db.HistSamples.Include(h => h.Host).Include(h => h.SampleType);
            return View(histSamples.ToList());
        }

        public ActionResult CreateRandom()
        {

            // The following code can make the CPU bounce up and down
            // which is useful for testing out tools which calculate the "average" CPU 
            /*
                        while (true)
                        {
                            for (int xx = 0; xx < 100000000; xx++)
                            {
                                int  y = 888;
                                int x = 999;
                                int z = x * y / (x + 1) / (y + 1);
                                if (z > 7)
                                    z += 1;
                                else
                                    z -= 1;
                            }
                            System.Threading.Thread.Sleep(1000);

                        }
                        return RedirectToAction("Index");
            */

            DateTime dtBase = new DateTime(2015, 1, 1, 0, 15,0);
            double dbBase = 50;

            for (int i = 0; i < (3 * 7 * 24 * 4); i++)
            {
                HistSample histSample = new HistSample { dbValue = dbBase, dtSample = dtBase, HostId = 1005, SampleTypeId = 1 };

                db.HistSamples.Add(histSample);
                dtBase = dtBase.AddMinutes(15);
                dbBase += GetRandomIncrement(-3, +3);
                if (dbBase < 0)
                    dbBase = 0;
                if (dbBase > 100)
                    dbBase = 100;
            }

            db.SaveChanges();
            return RedirectToAction("Index");

        }


        static double GetRandomIncrement(double dbmin, double dbmax)
        {
            return Math.Floor(r.NextDouble() * (dbmax - dbmin + 1.0)) + dbmin;
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
