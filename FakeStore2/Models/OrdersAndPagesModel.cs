using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FakeStore2.Models
{
    public class OrdersAndPagesModel
    {
        public List<OrdersModel> Orders { get; set; }
        public double NumberOfPages { get; set; }
    }
}