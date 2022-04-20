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
    public static class GetDeletingOrder
    {
        public class Query : IRequest<OrdersModel>
        {
            public int _Id { get; }
            public Query(int Id)
            {
                this._Id = Id;
            }
        }

        public class Handler : IRequestHandler<Query, OrdersModel>
        {
            private readonly IFakeStore2Entities _context;

            public Handler(IFakeStore2Entities context)
            {
                _context = context;
            }

            public async Task<OrdersModel> Handle(Query request, CancellationToken cancellationToken)
            {
                var order = _context.Orders
                .Where(o => o.OrderId == request._Id)
                .Select(o => new OrdersModel()
                {
                    CostumerName = o.Costumer.FirstName,
                    CostumerId = o.CostumerId,
                    OrderDate = o.OrderDate.ToString(),
                    OrderId = o.OrderId,
                    Total = o.Total,
                })
                .SingleOrDefault(); ;

                return order == null ? null : order;
            }

        }
    }
}