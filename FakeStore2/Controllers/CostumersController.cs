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
using FakeStore2.Commands.Customer;

namespace FakeStore2.Controllers
{
    public class CostumersController : Controller
    {

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
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        // POST: Costumers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Costumer customer)
        {
            //ModelState.IsValid checks if the object passed in matches the expected object
            if (!ModelState.IsValid)
            {
                return View(customer);
            }
            else
            {
               var response = _mediator.Send(new AddCustomer.Command(customer));
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
        public ActionResult Edit(Costumer customer)
        {
            if (ModelState.IsValid)
            {
                var response = _mediator.Send(new EditCustomer.Command(customer));
                return RedirectToAction(nameof(Index));
            }

            return View(customer);
        }

        // GET: Costumers/Delete/5
        [HttpGet]
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
        public async Task<ActionResult> DeleteConfirmed(int id)
        {

            var response = _mediator.Send(new DeleteCustomer.Command(id));
            return RedirectToAction(nameof(Index));
        }

    }
}
