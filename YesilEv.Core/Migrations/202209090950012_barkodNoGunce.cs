namespace YesilEv.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class barkodNoGunce : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Urun", "BarkodNo", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Urun", "BarkodNo", c => c.String(nullable: false, maxLength: 150, fixedLength: true));
        }
    }
}
