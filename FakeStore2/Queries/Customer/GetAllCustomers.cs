using FakeStore2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FakeStore2.Persistence.Interfaces;
using FakeStore2.Persistence;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace FakeStore2.Reads.Customer
{
  
    public static class GetAllCustomers
    {
        public class Query : IRequest<List<CustomerModel>>
        {
        }

        public class Handler : IRequestHandler<Query, List<CustomerModel>>
        {
            private readonly IFakeStore2Entities _context;

            public Handler(IFakeStore2Entities context)
            {
                 _context = context;
            }

            public async Task<List<CustomerModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var customers = _context.Costumers
                         .Select(c => new CustomerModel()
                         {
                             CostumerId = c.CostumerId,
                             CostumerSince = c.CostumerSince.ToString(),
                             FirstName = c.FirstName,
                             LastName = c.LastName,
                             isActive = c.isActive,
                             Orders = c.Orders,
                             OrdersCount = c.Orders.Count(),
                         })
                         .ToList();

                return customers == null ? null : customers;
            }
        }
    }
}
