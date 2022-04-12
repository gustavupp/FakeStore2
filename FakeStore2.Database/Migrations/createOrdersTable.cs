using FluentMigrator;


namespace FakeStore2.Database.Migrations
{ 
    [Migration(202204100600, "Create Orders Table")]
public class createOrdersTable : ForwardOnlyMigration
{
    public override void Up()
    {
        Execute.Sql(@"CREATE TABLE [Orders] (
    OrderId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    CostumerId INT NOT NULL CONSTRAINT FK_Costumers_CostumerId REFERENCES Costumers (CostumerId), 
    OrderDate DATETIME NOT NULL,
    Total DECIMAL(18,2) NOT NULL,
    );");
    }
}
}
