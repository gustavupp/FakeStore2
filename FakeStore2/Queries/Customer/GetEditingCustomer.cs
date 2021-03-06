using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FakeStore2.Models;
using FakeStore2.Persistence.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FakeStore2.Queries.Customer
{
    public static class GetEditingCustomer
    {
        public class Query : IRequest<CustomerModel>
        {
            public int Id { get; }
            public Query(int Id)
            {
                this.Id = Id;
            }
        }

        public class Handler : IRequestHandler<Query, CustomerModel>
        {
            private readonly IFakeStore2Entities _context;

            public Handler(IFakeStore2Entities context)
            {
                _context = context;
            }

            public async Task<CustomerModel> Handle(Query request, CancellationToken cancellationToken)
            {
                var customer = _context.Costumers
               .Where(c => c.CostumerId == request.Id)
               .Select(c => new CustomerModel()
               {
                   CostumerId = c.CostumerId,
                   CostumerSince = c.CostumerSince.ToString(),
                   FirstName = c.FirstName,
                   LastName = c.LastName,
                   isActive = c.isActive,
                   Orders = c.Orders,
               })
               .FirstOrDefault();

                return customer == null ? null : customer;
            }
        }
    }
}