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
        public System.DateTime OrderDate { get; set; }
        public decimal Total { get; set; }

        public virtual Costumer Costumer { get; set; }
    }
}