using FakeStore2.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FakeStore2.Models
{
    public class OrdersModel
    {
        public int OrderId { get; set; }
        public int CostumerId { get; set; }
        public string OrderDate { get; set; }
        public decimal Total { get; set; }

        public string Costumer { get; set; }
    }
}