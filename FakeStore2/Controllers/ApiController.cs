using FakeStore2.Models;
using FakeStore2.Persistence;
using FakeStore2.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FakeStore2.Controllers
{
    public class ApiController : Controller
    {
        private FakeStore2Entities _context = new FakeStore2Entities();

        //Get Orders of given customer: Api/Orders/Id
        [HttpGet]
        [Route("api/orders/{id?}")]
        public JsonResult Orders(int? id, int startRow = 0, int amountOfRows = 10)
        {

            if (id.HasValue)
            {
                //converts the db object into a Model class before sending it to front end
                var orders = _context.Orders
                    .Where(o => o.CostumerId == id)
                    .OrderBy(o => o.OrderId)
                    .Skip(startRow)
                    .Take(amountOfRows)
                    .Select(o => new OrdersModel()
                    {
                        CostumerId = o.CostumerId,
                        OrderDate = o.OrderDate.ToString(),
                        OrderId = o.OrderId,
                        Total = o.Total,
                        CostumerName = o.Costumer.FirstName,
                    })
                    .ToList();

                var costumerOrdersCount = _context.Orders.Where(o => o.CostumerId == id).Count();
                var numberOfPages = Math.Ceiling((double)costumerOrdersCount / amountOfRows);
                //return numberOfPages and orders
                return Json(new {orders, numberOfPages}, JsonRequestBehavior.AllowGet);
            }

            else
            {
                var orders = _context.Orders
                    .OrderBy(o => o.OrderId)
                    .Skip(startRow)
                    .Take(amountOfRows)
                    .Select(o => new OrdersModel()
                {
                    CostumerId = o.CostumerId,
                    OrderDate = o.OrderDate.ToString(),
                    OrderId = o.OrderId,
                    Total = o.Total,
                    CostumerName = o.Costumer.FirstName,
                })
                .ToList();

                var numberOfPages = Math.Ceiling((double)_context.Orders.Count() / amountOfRows);
                //return numberOfPages and orders
                return Json(new { orders, numberOfPages }, JsonRequestBehavior.AllowGet);
            }
            
        }

    }

    
}