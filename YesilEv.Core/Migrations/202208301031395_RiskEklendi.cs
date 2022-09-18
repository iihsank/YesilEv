namespace YesilEv.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RiskEklendi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Icerik", "RiskDegeri", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Icerik", "RiskDegeri");
        }
    }
}
