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
using FakeStore2.Reads.Customer;
using MediatR;
using System.Threading.Tasks;
using FakeStore2.Queries.Customer;

namespace FakeStore2.Controllers
{
    public class CostumersController : Controller
    {
        private readonly IFakeStore2Entities _context;

        public CostumersController(IFakeStore2Entities context)
        {
            this._context = context;
        }

        private readonly IMediator _mediator;

        public CostumersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //GET: /Customers
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var response = await _mediator.Send(new GetAllCustomers.Query());
            return View(response);
        }


        //GET: /Costumers/Details/1
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var response = await _mediator.Send(new GetCustomerDetails.Query(id));
            return View(response);
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
        public async Task<ActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var response = await _mediator.Send(new GetEditingCustomer.Query(id));
            return View(response);
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
        public async Task<ActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var response = await _mediator.Send(new GetDeletingCustomer.Query(id));
            return View(response);
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

        /*protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }*/
    }
}
