using FakeStore2.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FakeStore2.Controllers
{
    public class ApiController : Controller
    {
        private FakeStore2Entities db = new FakeStore2Entities();

        //Get Orders of given customer: Api/Orders/Id
        [HttpGet]
        public JsonResult Orders(int? id)
        {
            if (id.HasValue)
            {
                //converts the db object into a Model class before sendint to front end
                var orders = db.Orders
                    .Where(o => o.CostumerId == id)
                    .Select(o => new Models.OrdersModel()
                    {
                        CostumerId = o.CostumerId,
                        OrderDate = o.OrderDate.ToString(),
                        OrderId = o.OrderId,
                        Total = o.Total,
                        Costumer = o.Costumer.FirstName,
                    })
                    .ToList();

                return Json(orders, JsonRequestBehavior.AllowGet);
            }

            else
            {
                var orders = db.Orders.Select(o => new
                {
                    CostumerId = o.CostumerId,
                    OrderDate = o.OrderDate.ToString(),
                    OrderId = o.OrderId,
                    Total = o.Total,
                    Costumer = o.Costumer.FirstName,
                })
                .ToList();

                return Json(orders, JsonRequestBehavior.AllowGet);
            }
            
        }
    }
}