namespace YesilEv.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kategoriTablosuDuzenlendi : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Kategori", "Aciklama", c => c.String(nullable: false, maxLength: 128, fixedLength: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Kategori", "Aciklama", c => c.String(nullable: false, maxLength: 10, fixedLength: true));
        }
    }
}
