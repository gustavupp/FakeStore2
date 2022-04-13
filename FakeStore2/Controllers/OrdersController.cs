using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FakeStore2.Models;
using FakeStore2.Persistence;
using FakeStore2.ViewModel;

namespace FakeStore2.Controllers
{
    public class OrdersController : Controller
    {
        private FakeStore2Entities db = new FakeStore2Entities();

        // GET: All Orders
        public ActionResult Index()
        {
            var orders = db.Orders
                .Include(o => o.Costumer)
                .Select(o => new Models.OrdersModel() 
            {
            Costumer = o.Costumer.FirstName,
            CostumerId = o.CostumerId,
            OrderDate = o.OrderDate.ToString(),
            OrderId = o.OrderId,
            Total = o.Total,
            })
                .ToList();

            var costumers = db.Costumers
                .Include(c => c.Orders)
                .Select(c => new Models.CustomerModel()
                { 
                FirstName = c.FirstName,
                LastName = c.LastName,
                CostumerId = c.CostumerId,
                CostumerSince = c.CostumerSince.ToString(),
                isActive = c.isActive,
                })
                .ToList();

            var orderViewModel = new OrdersViewModel()
            {
                Costumers = costumers,
                Orders = orders
            };

            return View(orderViewModel);
        }

        // GET: Orders/CostumerOrders/Id
        [HttpGet]
        public ActionResult CostumerOrders(int id)
        {
                var orders = db.Orders
                .Where(o => o.CostumerId == id)
                .Select(o => new Models.OrdersModel()
                {
                    Costumer = o.Costumer.FirstName,
                    CostumerId = o.CostumerId,
                    OrderDate = o.OrderDate.ToString(),
                    OrderId = o.OrderId,
                    Total = o.Total,
                })
                .ToList();

                return View(orders);
        }

       

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = db.Orders
                .Where(o => o.OrderId == id)
                .Select(o => new OrdersModel() 
                {
                    Costumer = o.Costumer.FirstName,
                    CostumerId = o.CostumerId,
                    OrderDate = o.OrderDate.ToString(),
                    OrderId = o.OrderId,
                    Total = o.Total,
                })
                .SingleOrDefault(); ;

            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.CostumerId = new SelectList(db.Costumers, "CostumerId", "FirstName");
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,CostumerId,OrderDate,Total")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CostumerId = new SelectList(db.Costumers, "CostumerId", "FirstName", order.CostumerId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CostumerId = new SelectList(db.Costumers, "CostumerId", "FirstName", order.CostumerId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,CostumerId,OrderDate,Total")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CostumerId = new SelectList(db.Costumers, "CostumerId", "FirstName", order.CostumerId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
