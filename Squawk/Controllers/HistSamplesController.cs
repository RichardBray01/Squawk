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

        public ActionResult Index()
        {
            var histSamples = db.HistSamples.Include(h => h.Host).Include(h => h.SampleType);
            return View(histSamples.ToList());
        }

        enum SampleType
        {
            Cpu = 1,
            Disk = 2,
            Network = 3,
            Memory = 4
        };

        public ActionResult WriteRandomChartDataIntoDatabase()
        {

            DateTime dtSampleDate = new DateTime(2015, 1, 1, 0, 15,0);
            double dbCpu = 20;
            double dbDisk = 40;
            double dbNetwork = 60;
            double dbMemory = 80;

            for (int i = 0; i < (3 * 7 * 24 * 4); i++)
            {
                try
                {
                    HistSample histSample1 = new HistSample { dbValue = dbCpu, dtSample = dtSampleDate, HostId = 1, SampleTypeId = (int) SampleType.Cpu };
                    db.HistSamples.Add(histSample1);

                    HistSample histSample2 = new HistSample { dbValue = dbDisk, dtSample = dtSampleDate, HostId = 1, SampleTypeId = (int)SampleType.Disk };
                    db.HistSamples.Add(histSample2);

                    HistSample histSample3 = new HistSample { dbValue = dbNetwork, dtSample = dtSampleDate, HostId = 1, SampleTypeId = (int)SampleType.Network };
                    db.HistSamples.Add(histSample3);

                    HistSample histSample4 = new HistSample { dbValue = dbMemory, dtSample = dtSampleDate, HostId = 1, SampleTypeId = (int)SampleType.Memory };
                    db.HistSamples.Add(histSample4);

                    db.SaveChanges();

                    dtSampleDate = dtSampleDate.AddMinutes(15);

                    dbCpu += GetRandomIncrement(-3, +3);
                    if (dbCpu < 0)
                        dbCpu = 0;
                    if (dbCpu > 100)
                        dbCpu = 100;

                    dbDisk += GetRandomIncrement(-2, +2);
                    if (dbDisk < 0)
                        dbDisk = 0;
                    if (dbDisk > 100)
                        dbDisk = 100;

                    dbNetwork += GetRandomIncrement(-1, +1);
                    if (dbNetwork < 0)
                        dbNetwork = 0;
                    if (dbNetwork > 100)
                        dbNetwork = 100;

                    dbMemory += GetRandomIncrement(-1, +1);
                    if (dbMemory < 20)
                        dbMemory = 20;
                    if (dbMemory > 80)
                        dbMemory = 80;

                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception : " + e.Message);
                }

            }

            return RedirectToAction("Index");

        }

        public ActionResult MakeCPUGoUpAndDown()
        {

            // The following code can make the CPU bounce up and down
            // which is useful for testing out tools which calculate the "average" CPU 
            while (true)
            {
                for (int xx = 0; xx < 100000000; xx++)
                {
                    int y = 888;
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
