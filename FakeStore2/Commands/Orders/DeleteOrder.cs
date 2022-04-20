using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FakeStore2.Persistence;
using FakeStore2.Persistence.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FakeStore2.Commands.Orders
{
    public static class DeleteOrder
    {
        public class Command : IRequest<Order>
        {
            public int _id { get; }
            public Command(int id)
            {
                this._id = id;
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
                var order = _context.Orders.Find(request._id);
                _context.Orders.Remove(order);

                _context.SaveChanges();

                var deletedOrder = _context.Orders.FirstOrDefault();

                return deletedOrder == null ? null : deletedOrder;
            }

        }
    }
}