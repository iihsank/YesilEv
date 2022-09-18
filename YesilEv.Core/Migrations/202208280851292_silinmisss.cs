namespace YesilEv.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class silinmisss : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.FavoriKaraListe", "AktifMi");
            DropColumn("dbo.Kullanici", "AktifMi");
            DropColumn("dbo.KullaniciRol", "AktifMi");
            DropColumn("dbo.Rol", "AktifMi");
            DropColumn("dbo.Urun", "AktifMi");
            DropColumn("dbo.Kategori", "AktifMi");
            DropColumn("dbo.Uretici", "AktifMi");
            DropColumn("dbo.UrunIcerik", "AktifMi");
            DropColumn("dbo.Icerik", "AktifMi");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Icerik", "AktifMi", c => c.Boolean(nullable: false));
            AddColumn("dbo.UrunIcerik", "AktifMi", c => c.Boolean(nullable: false));
            AddColumn("dbo.Uretici", "AktifMi", c => c.Boolean(nullable: false));
            AddColumn("dbo.Kategori", "AktifMi", c => c.Boolean(nullable: false));
            AddColumn("dbo.Urun", "AktifMi", c => c.Boolean(nullable: false));
            AddColumn("dbo.Rol", "AktifMi", c => c.Boolean(nullable: false));
            AddColumn("dbo.KullaniciRol", "AktifMi", c => c.Boolean(nullable: false));
            AddColumn("dbo.Kullanici", "AktifMi", c => c.Boolean(nullable: false));
            AddColumn("dbo.FavoriKaraListe", "AktifMi", c => c.Boolean(nullable: false));
        }
    }
}
