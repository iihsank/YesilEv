namespace YesilEv.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class resim2eklendi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Urun", "Resim2Url", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Urun", "Resim2Url");
        }
    }
}
