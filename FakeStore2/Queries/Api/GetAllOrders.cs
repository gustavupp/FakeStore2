using FakeStore2.Models;
using FakeStore2.Persistence.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace FakeStore2.Queries.Api
{
    public static class GetAllOrders
    {
        public class Query : IRequest<List<OrdersModel>>
        {
        }

        public class Handler : IRequestHandler<Query, List<OrdersModel>>
        {
            private readonly IFakeStore2Entities _context;

            public Handler(IFakeStore2Entities context)
            {
                _context = context;
            }

            public async Task<List<OrdersModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var orders = _context.Orders
                .Select(o => new Models.OrdersModel()
                {
                    CostumerName = o.Costumer.FirstName,
                    CostumerId = o.CostumerId,
                    OrderDate = o.OrderDate.ToString(),
                    OrderId = o.OrderId,
                    Total = o.Total,
                })
                .ToList();

                return orders == null ? null : orders;
            }
        }
    }
}