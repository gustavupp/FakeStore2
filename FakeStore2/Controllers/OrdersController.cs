using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FakeStore2.Models;
using FakeStore2.Persistence;
using FakeStore2.Persistence.Interfaces;
using FakeStore2.Queries.Orders;
using FakeStore2.ViewModel;
using MediatR;

namespace FakeStore2.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IFakeStore2Entities _context;

        public OrdersController(IFakeStore2Entities context)
        {
            _context = context;
        }

        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }


        //GET: /Customers
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var response = await _mediator.Send(new GetAllOrders.Query());
            return View(response);
        }

        //Get: /Orders/Datatable
        public ActionResult Datatable()
        {
            return View(nameof(Datatable));
        }



        // GET: Orders/CostumerOrders/Id
        [HttpGet]
        public async Task<ActionResult> CostumerOrders(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var response = await _mediator.Send(new GetCustomerOrder.Query(id));

            if (response == null)
            {
                return HttpNotFound();
            }

            return View(response);
        }


        // GET: Orders/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var response = await _mediator.Send(new GetOrderDetails.Query(id));
            return View(response);
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


        // GET: Orders/Details/5
        public async Task<ActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var response = await _mediator.Send(new GetEditingOrder.Query(id));

            if (response == null)
            {
                return HttpNotFound();
            }

            return View(response);
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
        public async Task<ActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var response = await _mediator.Send(new GetDeletingOrder.Query(id));

            if (response == null)
            {
                return HttpNotFound();
            }

            return View(response);
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

       /* protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }*/

    }
}
