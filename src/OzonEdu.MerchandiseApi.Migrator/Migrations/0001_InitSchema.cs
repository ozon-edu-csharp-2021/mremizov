using FluentMigrator;

namespace OzonEdu.MerchandiseApi.Migrator.Migrations
{
    [Migration(1)]
    public class InitSchema : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create
                .Table("MerchPacks")
                .WithColumn("Id").AsInt64().Identity().PrimaryKey()
                .WithColumn("Type").AsInt32().NotNullable()
                .WithColumn("Items").AsCustom("jsonb").NotNullable();

            Create
                .Table("Merchs")
                .WithColumn("Id").AsInt64().Identity().PrimaryKey()
                .WithColumn("MerchpackId").AsInt64().NotNullable()
                .WithColumn("EmployeeId").AsInt64().NotNullable()
                .WithColumn("CreatedUtc").AsDateTime2().NotNullable()
                .WithColumn("Status").AsInt32().NotNullable()
                .WithColumn("Mode").AsInt32().NotNullable();
        }
    }
}
