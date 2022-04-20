using FakeStore2.Models;
using FakeStore2.Persistence;
using FakeStore2.Persistence.Interfaces;
using FakeStore2.ViewModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace FakeStore2.Queries.Orders
{
    public static class GetEditingOrder
    {
        public class Query : IRequest<OrderAndCustomerListViewModel>
        {
            public int _Id { get; }
            public Query(int Id)
            {
                this._Id = Id;
            }
        }

        public class Handler : IRequestHandler<Query, OrderAndCustomerListViewModel>
        {
            private readonly IFakeStore2Entities _context;

            public Handler(IFakeStore2Entities context)
            {
                _context = context;
            }

            public async Task<OrderAndCustomerListViewModel> Handle(Query request, CancellationToken cancellationToken)
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
                .FirstOrDefault(); ;

                var customerList = _context.Costumers
                    .Select(c => new CustomerModel()
                {
                        FirstName = c.FirstName,
                        CostumerId = c.CostumerId,
                })
                    .ToList();

                var orderAndCustomersList = new OrderAndCustomerListViewModel()
                {
                    Customers = customerList,
                    Order = order,
                };

                return orderAndCustomersList == null ? null : orderAndCustomersList;
            }

        }
    }

    internal class OrderAndCustomerViewModel
    {
        public OrderAndCustomerViewModel()
        {
        }

        public List<Costumer> CustomerList { get; set; }
        public OrdersModel Order { get; set; }
    }
}