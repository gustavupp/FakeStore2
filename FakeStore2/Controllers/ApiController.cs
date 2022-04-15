using FakeStore2.Models;
using FakeStore2.Persistence;
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
        public JsonResult Orders(int? id, int startRow = 0, int amountOfRows = 8)
        {
            var paginatedOrders = _context.Orders
                    .OrderBy(o => o.OrderId)
                    .Skip(startRow)
                    .Take(amountOfRows);

            if (id.HasValue)
            {
                //converts the db object into a Model class before sendint to front end
                var orders = paginatedOrders
                    .Where(o => o.CostumerId == id)
                    .Select(o => new OrdersModel()
                    {
                        CostumerId = o.CostumerId,
                        OrderDate = o.OrderDate.ToString(),
                        OrderId = o.OrderId,
                        Total = o.Total,
                        CostumerName = o.Costumer.FirstName,
                    })
                    .ToList();

                return Json(orders, JsonRequestBehavior.AllowGet);
            }

            else
            {
                var orders = paginatedOrders
                    .Select(o => new OrdersModel()
                {
                    CostumerId = o.CostumerId,
                    OrderDate = o.OrderDate.ToString(),
                    OrderId = o.OrderId,
                    Total = o.Total,
                    CostumerName = o.Costumer.FirstName,
                })
                .ToList();

                return Json(orders, JsonRequestBehavior.AllowGet);
            }
            
        }

    }

    
}