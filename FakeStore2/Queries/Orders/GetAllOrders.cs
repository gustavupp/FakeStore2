using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FakeStore2.Persistence.Interfaces;
using FakeStore2.Persistence;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using FakeStore2.Models;
using FakeStore2.ViewModel;

namespace FakeStore2.Queries.Orders
{
    public static class GetAllOrders
    {
        public class Query : IRequest<OrdersViewModel>
        {
        }

        public class Handler : IRequestHandler<Query, OrdersViewModel>
        {
            private readonly IFakeStore2Entities _context;

            public Handler(IFakeStore2Entities context)
            {
                _context = context;
            }

            public async Task<OrdersViewModel> Handle(Query request, CancellationToken cancellationToken)
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

                var costumers = _context.Costumers
                    .Select(c => new CustomerModel()
                    {
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        CostumerId = c.CostumerId,
                        CostumerSince = c.CostumerSince.ToString(),
                        isActive = c.isActive,
                    })
                    .ToList();

                var orderViewModel = new OrdersViewModel()
                {
                    Costumers = costumers,
                    Orders = orders
                };

                return orderViewModel == null ? null : orderViewModel;
            }
        }
    }
}