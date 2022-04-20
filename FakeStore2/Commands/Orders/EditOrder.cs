using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FakeStore2.Persistence;
using FakeStore2.Persistence.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using FakeStore2.Models;

namespace FakeStore2.Commands.Orders
{
    public static class EditOrder
    {
        public class Command : IRequest<OrdersModel>
        {
            public OrdersModel _order { get; }
            public Command(OrdersModel order)
            {
                this._order = order;
            }
        }

        public class Handler : IRequestHandler<Command, OrdersModel>
        {
            private readonly IFakeStore2Entities _context;

            public Handler(IFakeStore2Entities context)
            {
                _context = context;
            }

            public async Task<OrdersModel> Handle(Command request, CancellationToken cancellationToken)
            {
                var editingOrder = _context.Orders.FirstOrDefault(o => o.OrderId == request._order.OrderId);

                editingOrder.OrderDate = DateTime.Parse(request._order.OrderDate);
                editingOrder.Total = request._order.Total;
                editingOrder.CostumerId = request._order.CostumerId;

                _context.SaveChanges();

                var addedOrder = _context.Orders
                    .Where(o => o.OrderId == request._order.OrderId)
                    .Select(o => new OrdersModel()
                    {
                        OrderDate = request._order.OrderDate,
                        Total = request._order.Total,
                        CostumerId = request._order.CostumerId,
                    })
                    .FirstOrDefault();

                return addedOrder == null ? null : addedOrder;
            }

        }
    }
}