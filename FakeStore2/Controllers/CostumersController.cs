using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FakeStore2.Persistence;

namespace FakeStore2.Controllers
{
    public class CostumersController : Controller
    {
        private FakeStore2Entities db = new FakeStore2Entities();

        // GET: Costumers
        public ActionResult Index()
        {

            return View(db.Costumers.ToList());
        }

        // GET: Costumers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Costumer costumer = db.Costumers.Find(id);
            if (costumer == null)
            {
                return HttpNotFound();
            }
            return View(costumer);
        }

        // GET: Costumers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Costumers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CostumerId,FirstName,LastName,isActive,CostumerSince")] Costumer costumer)
        {
            if (ModelState.IsValid)
            {
                db.Costumers.Add(costumer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(costumer);
        }

        // GET: Costumers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Costumer costumer = db.Costumers.Find(id);
            if (costumer == null)
            {
                return HttpNotFound();
            }
            return View(costumer);
        }

        // POST: Costumers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CostumerId,FirstName,LastName,isActive,CostumerSince")] Costumer costumer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(costumer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(costumer);
        }

        // GET: Costumers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Costumer costumer = db.Costumers.Find(id);
            if (costumer == null)
            {
                return HttpNotFound();
            }
            return View(costumer);
        }

        // POST: Costumers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Costumer costumer = db.Costumers.Find(id);
            db.Costumers.Remove(costumer);
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
