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

namespace FakeStore2.Commands.Customer
{
    public static class DeleteCustomer
    {
        public class Command : IRequest<Costumer>
        {
            public int _id { get; }
            public Command(int id)
            {
                this._id = id;
            }
        }

        public class Handler : IRequestHandler<Command, Costumer>
        {
            private readonly IFakeStore2Entities _context;

            public Handler(IFakeStore2Entities context)
            {
                _context = context;
            }

            public async Task<Costumer> Handle(Command request, CancellationToken cancellationToken)
            {
                var costumer = _context.Costumers.Find(request._id);

                bool hasOrders = _context.Orders.Any(o => o.Costumer.CostumerId == request._id);

                if (hasOrders)
                {
                    var allCostumerOrders = _context.Orders.Where(o => o.CostumerId == request._id).ToList();
                    allCostumerOrders.ForEach(o => _context.Orders.Remove(o));
                }
               
                _context.Costumers.Remove(costumer);
                _context.SaveChanges();

                var deletedCustomer = _context.Costumers.FirstOrDefault(c => c.CostumerId == request._id);

                return deletedCustomer == null ? null : deletedCustomer;
            }

        }
    }
}