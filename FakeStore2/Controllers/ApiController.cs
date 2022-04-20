
using FakeStore2.Queries.Api;
using MediatR;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FakeStore2.Controllers
{
    public class ApiController : Controller
    {

        private readonly IMediator _mediator;

        public ApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //get all orders for datatable
        [HttpGet]
        [Route("api/orders/all")]
        public async Task<JsonResult> AllOrders()
        {
            var response = await _mediator.Send(new GetAllOrders.Query());
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        //Get Orders of given customer: Api/Orders/Id
        [HttpGet]
        [Route("api/orders/{id?}")]
        public async Task<JsonResult> Orders(int? id, int startRow = 0, int amountOfRows = 10, string searchInput = "")
        {
            var response = await _mediator.Send(new GetCustomerOrders.Query(id,startRow, amountOfRows,searchInput));
            return Json(new { response.Orders, response.NumberOfPages }, JsonRequestBehavior.AllowGet);
        }
            
    }
}