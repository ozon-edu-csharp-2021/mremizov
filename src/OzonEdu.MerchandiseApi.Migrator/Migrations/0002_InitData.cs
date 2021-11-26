using FluentMigrator;

namespace OzonEdu.MerchandiseApi.Migrator.Migrations
{
    [Migration(2)]
    public class InitData : ForwardOnlyMigration
    {
        public override void Up()
        {
            Execute.Sql(@"
                INSERT INTO merch_packs(type, items)
                VALUES
                    (10, '[""1""]'),
	                (20, '[""2""]'),
	                (30, '[""3""]'),
	                (40, '[""4""]'),
	                (50, '[""5""]')
                ON CONFLICT DO NOTHING
            ");
        }
    }
}
