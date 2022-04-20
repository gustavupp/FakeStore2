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
    public class EditCustomer
    {
        public class Command : IRequest<Costumer>
        {
            public Costumer _customer { get; }
            public Command(Costumer customer)
            {
                this._customer = customer;
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
                var editingCostumer = _context.Costumers
                      .Where(c => c.CostumerId == request._customer.CostumerId)
                      .FirstOrDefault();

                editingCostumer.FirstName = request._customer.FirstName;
                editingCostumer.LastName = request._customer.LastName;
                editingCostumer.isActive = request._customer.isActive;
                editingCostumer.CostumerSince = request._customer.CostumerSince;

                _context.SaveChanges();

                var editedCustomer = _context.Costumers.FirstOrDefault(c => c.CostumerId == editingCostumer.CostumerId);

                return editedCustomer == null ? null : editedCustomer;
            }

        }
    }
}