using FluentMigrator;

namespace OzonEdu.MerchandiseApi.Migrator.Migrations
{
    [Migration(1)]
    public class InitSchema : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create
                .Table("merch_packs")
                .WithColumn("id").AsInt64().Identity().PrimaryKey()
                .WithColumn("type").AsInt32().NotNullable()
                .WithColumn("clothing_size").AsInt32().Nullable()
                .WithColumn("items").AsCustom("jsonb").NotNullable();

            Create
                .Table("merchs")
                .WithColumn("id").AsInt64().Identity().PrimaryKey()
                .WithColumn("merchpack_id").AsInt64().NotNullable()
                .WithColumn("employee_id").AsInt64().NotNullable()
                .WithColumn("created_utc").AsDateTime2().NotNullable()
                .WithColumn("status").AsInt32().NotNullable()
                .WithColumn("mode").AsInt32().NotNullable();
        }
    }
}
