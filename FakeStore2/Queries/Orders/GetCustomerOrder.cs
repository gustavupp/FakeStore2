using FakeStore2.Models;
using FakeStore2.Persistence.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace FakeStore2.Queries.Orders
{
    public static class GetCustomerOrder
    {
        public class Query : IRequest<List<OrdersModel>>
        {
            public int _Id { get; }
            public Query(int Id)
            {
                this._Id = Id;
            }
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
               .Where(o => o.CostumerId == request._Id)
               .Select(o => new OrdersModel()
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