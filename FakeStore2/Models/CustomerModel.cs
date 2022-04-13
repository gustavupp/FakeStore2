using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace FakeStore2.Models
{
    public class CustomerModel
    {

        public int CostumerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool isActive { get; set; }
        public string CostumerSince { get; set; }

        public virtual ICollection<OrdersModel> Orders { get; set; } = new HashSet<OrdersModel>();
    }
}