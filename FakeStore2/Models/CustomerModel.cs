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
        [Display(Name = "Id")]
        public int CostumerId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Is active?")]
        public bool isActive { get; set; }

        [Required]
        [Display(Name = "Costumer Since")]
        public string CostumerSince { get; set; }
        public int? OrdersCount { get; set; } 

        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}