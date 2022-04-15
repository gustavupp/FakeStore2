using FakeStore2.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FakeStore2.Models
{
    public class OrdersModel
    {

        [Display(Name = "Order Id")]
        public int OrderId { get; set; }

        [Display(Name = "Costumer Id")]
        public int CostumerId { get; set; }

        [Display(Name = "Date")]
        public string OrderDate { get; set; }

        public decimal Total { get; set; }
        public Costumer Costumer { get; set; }

        [Display(Name = "Name")]
        public string CostumerName { get; set; }    

    }
}