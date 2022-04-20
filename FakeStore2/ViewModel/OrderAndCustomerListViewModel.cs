using FakeStore2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FakeStore2.ViewModel
{
    public class OrderAndCustomerListViewModel
    {
        public List<CustomerModel> Customers { get; set; }
        public OrdersModel Order { get; set; }
    }
}