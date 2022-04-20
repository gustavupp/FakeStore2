using FakeStore2.Models;
using FakeStore2.Persistence.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace FakeStore2.Queries.Api
{
    public class GetCustomerOrders
    {
        //int? id, int startRow = 0, int amountOfRows = 10, string searchInput = ""
        public class Query : IRequest<OrdersAndPagesModel>
        {
            public int? _Id { get; }
            public int _StartRow { get; }
            public int _AmountOfRows { get; }
            public string _SearchInput { get; }

            public Query(int? Id, int startRow, int amountOfRows, string searchInput)
            {
                this._Id = Id;
                this._StartRow = startRow;
                this._AmountOfRows = amountOfRows;
                this._SearchInput = searchInput;
            }
        }

        public class Handler : IRequestHandler<Query, OrdersAndPagesModel>
        {
            private readonly IFakeStore2Entities _context;

            public Handler(IFakeStore2Entities context)
            {
                _context = context;
            }

            public async Task<OrdersAndPagesModel> Handle(Query request, CancellationToken cancellationToken)
            {

                if (request._Id.HasValue)
                {
                    var orders = _context.Orders
                    .Where(o => o.CostumerId == request._Id && (o.Costumer.FirstName.Contains(request._SearchInput) ||
                     o.Total.ToString().Contains(request._SearchInput)))
                    .OrderBy(o => o.OrderId)
                    .Skip(request._StartRow)
                    .Take(request._AmountOfRows)
                    .Select(o => new OrdersModel()
                    {
                        CostumerId = o.CostumerId,
                        OrderDate = o.OrderDate.ToString(),
                        OrderId = o.OrderId,
                        Total = o.Total,
                        CostumerName = o.Costumer.FirstName,
                    })
                    .ToList();

                    var costumerOrdersCount = _context.Orders.Where(o => o.CostumerId == request._Id).Count();
                    var numberOfPages = Math.Ceiling((double)costumerOrdersCount / request._AmountOfRows);

                    var OrdersAndPages = new OrdersAndPagesModel()
                    {
                        Orders = orders,
                        NumberOfPages = numberOfPages
                    };

                    return OrdersAndPages == null ? null : OrdersAndPages;
                }
            else
                {
                    var ordersWithoutPagination = _context.Orders
                    .Where(o => (o.Costumer.FirstName.Contains(request._SearchInput)) ||
                     o.Total.ToString().Contains(request._SearchInput)).ToList();

                    var orders = ordersWithoutPagination.OrderBy(o => o.OrderId)
                        .Skip(request._StartRow)
                        .Take(request._AmountOfRows)
                        .Select(o => new OrdersModel()
                        {
                            CostumerId = o.CostumerId,
                            OrderDate = o.OrderDate.ToString(),
                            OrderId = o.OrderId,
                            Total = o.Total,
                            CostumerName = o.Costumer.FirstName,
                        }).ToList();

                    var numberOfPages = Math.Ceiling((double)ordersWithoutPagination.Count() / request._AmountOfRows);

                    var OrdersAndPages = new OrdersAndPagesModel()
                    {
                        Orders = orders,
                        NumberOfPages = numberOfPages
                    };

                    return OrdersAndPages == null ? null : OrdersAndPages;
                }
               
            }
        }
    }
}