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

namespace FakeStore2.Controllers
{
    public class CostumersController : Controller
    {
        private FakeStore2Entities _context = new FakeStore2Entities();

        // GET: Costumers
        [HttpGet]
        public ActionResult Index()
        {
            var costumers = _context.Costumers
                .Include(c => c.Orders)
                .Select(c => new CustomerModel()
                {
                    CostumerId = c.CostumerId,
                    CostumerSince = c.CostumerSince.ToString(),
                     FirstName = c.FirstName,
                     LastName = c.LastName,
                     isActive = c.isActive,
                     Orders = c.Orders,
                     OrdersCount = c.Orders.Count(),
                })
                .ToList();

            return View(costumers);
        }

        // GET: Costumers/Details/5
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var costumer = _context.Costumers
                .Where(c => c.CostumerId == id)
                .Select(c => new CustomerModel()
                {
                    CostumerId = c.CostumerId,
                    CostumerSince = c.CostumerSince.ToString(),
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    isActive = c.isActive,
                    OrdersCount = c.Orders.Count()
                })
                .FirstOrDefault();

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Costumer costumer)
        {
            //ModelState.IsValid checks if the object passed in matches the expected object
            if (!ModelState.IsValid)
            {
                return View(costumer);
            }
            
            else
            {
                _context.Costumers.Add(costumer);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Costumers/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var costumer = _context.Costumers
                .Where(c => c.CostumerId == id)
                .Select(c => new CustomerModel() 
                {
                    CostumerId = c.CostumerId,
                    CostumerSince = c.CostumerSince.ToString(),
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    isActive = c.isActive,
                    Orders = c.Orders,
                })
                .FirstOrDefault();


            if (costumer == null)
            {
                return HttpNotFound();
            }
            return View(costumer);
        }

        // POST: Costumers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Costumer costumer)
        {
            if (ModelState.IsValid)
            {
                // _context.Entry(costumer).State = EntityState.Modified;
                var editingCostumer = _context.Costumers
                     .Where(c => c.CostumerId == costumer.CostumerId)
                     .FirstOrDefault();
                    
                editingCostumer.FirstName = costumer.FirstName;
                editingCostumer.LastName = costumer.LastName;
                editingCostumer.isActive = costumer.isActive;
                editingCostumer.CostumerSince = costumer.CostumerSince;

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
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

            var costumer = _context.Costumers
                .Where(c => c.CostumerId == id)
                .Select(c => new CustomerModel()
                {
                    CostumerId = c.CostumerId,
                    CostumerSince = c.CostumerSince.ToString(),
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    isActive = c.isActive,
                    OrdersCount = c.Orders.Count()
                })
                .FirstOrDefault();

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
            Costumer costumer = _context.Costumers.Find(id);

            bool hasOrders = _context.Orders.Any(o => o.Costumer.CostumerId == id);

            var allCostumerOrders = _context.Orders.Where(o => o.CostumerId == id).ToList();
            allCostumerOrders.ForEach(o => _context.Orders.Remove(o));
            _context.Costumers.Remove(costumer); 

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
