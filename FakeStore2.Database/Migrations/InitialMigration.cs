using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeStore2.Database.Migrations
{ 
    [Migration(20220410, "Initial Migration")]
public class InitialMigration : ForwardOnlyMigration
{
    public override void Up()
    {
        Execute.Sql(@"CREATE TABLE [Costumers] (
    CostumerId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    FirstName VARCHAR(255) NOT NULL,
    LastName VARCHAR(255) NOT NULL,
    isActive BIT NOT NULL,
    CostumerSince DATETIME NOT NULL,
    );");
    }
}
}
