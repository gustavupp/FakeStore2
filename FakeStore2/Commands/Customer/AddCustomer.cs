using FakeStore2.Models;
using FakeStore2.Persistence;
using FakeStore2.Persistence.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace FakeStore2.Commands.Customer
{
    public static class AddCustomer
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
                _context.Costumers.Add(request._customer);
                _context.SaveChanges();

                var addedCustomer = _context.Costumers.LastOrDefault();

                return addedCustomer == null ? null : addedCustomer;
            }

        }
    }
}