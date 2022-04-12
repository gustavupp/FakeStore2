using FakeStore2.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FakeStore2.ViewModel
{
    public class OrdersViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Costumer> Costumers { get; set; }
    }
}