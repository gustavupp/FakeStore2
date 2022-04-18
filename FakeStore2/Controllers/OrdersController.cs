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
using FakeStore2.Persistence.Interfaces;
using FakeStore2.ViewModel;

namespace FakeStore2.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IFakeStore2Entities _context;

        public OrdersController(IFakeStore2Entities context)
        {
            _context = context;
        }

        // GET: All Orders
        public ActionResult Index()
        {
            var orders = _context.Orders
                .Include(o => o.Costumer)
                .Select(o => new Models.OrdersModel() 
            {
            CostumerName = o.Costumer.FirstName,
            CostumerId = o.CostumerId,
            OrderDate = o.OrderDate.ToString(),
            OrderId = o.OrderId,
            Total = o.Total,
            })
                .ToList();

            var costumers = _context.Costumers
                .Include(c => c.Orders)
                .Select(c => new CustomerModel()
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
                var orders = _context.Orders
                .Where(o => o.CostumerId == id)
                .Select(o => new OrdersModel()
                {
                    CostumerName = o.Costumer.FirstName,
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
            var order = _context.Orders
                .Where(o => o.OrderId == id)
                .Select(o => new OrdersModel() 
                {
                    CostumerName = o.Costumer.FirstName,
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
            //by using the ViewBag to send data to the View you don't have to create another class
            ViewBag.CostumerId = new SelectList(_context.Costumers, "CostumerId", "FirstName");

            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Orders.Add(order);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var order = _context.Orders
                .Where(o => o.OrderId == id)
                .Select(o => new OrdersModel()
                {
                    CostumerName = o.Costumer.FirstName,
                    CostumerId = o.CostumerId,
                    OrderDate = o.OrderDate.ToString(),
                    OrderId = o.OrderId,
                    Total = o.Total,
                }
                ).FirstOrDefault();


            if (order == null)

            {
                return HttpNotFound();
            }

            ViewBag.CostumerId = new SelectList(_context.Costumers, "CostumerId", "FirstName", order.CostumerId);
            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrdersModel order)
        {
            if (ModelState.IsValid)
            {

                var editingOrder = _context.Orders.FirstOrDefault(o => o.OrderId == order.OrderId);

                editingOrder.OrderDate = DateTime.Parse(order.OrderDate);
                editingOrder.Total = order.Total;
                editingOrder.CostumerId = order.CostumerId;

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var order = _context.Orders
                .Where(o => o.OrderId == id)
                .Select(o => new OrdersModel()
                {
                    OrderId = o.OrderId,
                    CostumerName = o.Costumer.FirstName,
                    CostumerId= o.CostumerId,
                    OrderDate= o.OrderDate.ToString(),
                    Total = o.Total,
                    })
                .FirstOrDefault();

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
            Order order = _context.Orders.Find(id);
            _context.Orders.Remove(order);
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
