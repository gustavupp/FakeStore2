
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FakeStore2.Commands.Orders;
using FakeStore2.Models;
using FakeStore2.Persistence;
using FakeStore2.Persistence.Interfaces;
using FakeStore2.Queries.Orders;
using FakeStore2.Reads.Customer;
using FakeStore2.ViewModel;
using MediatR;

namespace FakeStore2.Controllers
{
    public class OrdersController : Controller
    {
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
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var response = await _mediator.Send(new GetOrderDetails.Query(id));
            return View(response);
        }


        // GET: Orders/Create
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            //by using the ViewBag to send data to the View you don't have to create another class
            var response = await _mediator.Send(new GetAllCustomers.Query());
            ViewBag.CostumerId = new SelectList(response, "CostumerId", "FirstName");

            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Order order)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(new AddOrder.Command(order));
                return RedirectToAction(nameof(Index));
            }

            return View();
        }


        // GET: Orders/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var response = await _mediator.Send(new GetEditingOrder.Query(id));
            var customerList = await _mediator.Send(new GetAllCustomers.Query());
            ViewBag.CostumerId = new SelectList(customerList, "CostumerId", "FirstName", response.CostumerId);

            if (response == null)
            {
                return HttpNotFound();
            }

            return View(response);
        }


        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(OrdersModel order)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(new EditOrder.Command(order));
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }


        // GET: Orders/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            if (id == 0)
            {
                 new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var response = await _mediator.Send(new DeleteOrder.Command(id));
            return RedirectToAction(nameof(Index));
        }

    }
}
