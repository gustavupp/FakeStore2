using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FakeStore2.Models;
using FakeStore2.Persistence;
using FakeStore2.Persistence.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FakeStore2.Commands.Orders
{
    public static class AddOrder
    {
        public class Command : IRequest<Order>
        {
            public Order _order { get; }
            public Command(Order order)
            {
                this._order = order;
            }
        }

        public class Handler : IRequestHandler<Command, Order>
        {
            private readonly IFakeStore2Entities _context;

            public Handler(IFakeStore2Entities context)
            {
                _context = context;
            }

            public async Task<Order> Handle(Command request, CancellationToken cancellationToken)
            {
                    _context.Orders.Add(request._order);
                    _context.SaveChanges();

                var addedOrder = _context.Orders.FirstOrDefault();

                return addedOrder == null ? null : addedOrder;
            }

        }
    }
}