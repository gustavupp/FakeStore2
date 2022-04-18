using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeStore2.Persistence.Interfaces
{
    public interface IFakeStore2Entities
    {
        DbSet<Costumer> Costumers { get; set; }
        DbSet<Order> Orders { get; set; }
        int SaveChanges();
        void Dispose();
    }
}
