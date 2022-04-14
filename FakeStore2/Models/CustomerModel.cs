using FakeStore2.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace FakeStore2.Models
{
    public class CustomerModel
    {
        public int CostumerId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public bool isActive { get; set; }

        [Required]
        public string CostumerSince { get; set; }
        public int? OrdersCount { get; set; } 

        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}