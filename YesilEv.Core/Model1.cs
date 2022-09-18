using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace YesilEv.Core
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=YesilEv")
        {
        }

        public virtual DbSet<FavoriKaraListe> FavoriKaraListes { get; set; }
        public virtual DbSet<Icerik> Iceriks { get; set; }
        public virtual DbSet<Kategori> Kategoris { get; set; }
        public virtual DbSet<Kullanici> Kullanicis { get; set; }
        public virtual DbSet<KullaniciRol> KullaniciRols { get; set; }
        public virtual DbSet<Rol> Rols { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Uretici> Ureticis { get; set; }
        public virtual DbSet<Urun> Uruns { get; set; }
        public virtual DbSet<UrunIcerik> UrunIceriks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Icerik>()
                .HasMany(e => e.UrunIceriks)
                .WithRequired(e => e.Icerik)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kategori>()
                .Property(e => e.Aciklama)
                .IsFixedLength();

            modelBuilder.Entity<Kategori>()
                .HasMany(e => e.Uruns)
                .WithRequired(e => e.Kategori)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kullanici>()
                .HasMany(e => e.FavoriKaraListes)
                .WithRequired(e => e.Kullanici)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kullanici>()
                .HasMany(e => e.KullaniciRols)
                .WithRequired(e => e.Kullanici)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kullanici>()
                .HasMany(e => e.Uruns)
                .WithRequired(e => e.Kullanici)
                .HasForeignKey(e => e.KullaniciId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kullanici>()
                .HasMany(e => e.Uruns1)
                .WithOptional(e => e.Kullanici1)
                .HasForeignKey(e => e.OnaylayanId);

            modelBuilder.Entity<Rol>()
                .HasMany(e => e.KullaniciRols)
                .WithRequired(e => e.Rol)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Uretici>()
                .HasMany(e => e.Uruns)
                .WithRequired(e => e.Uretici)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Urun>()
                .Property(e => e.BarkodNo)
                .IsFixedLength();

            modelBuilder.Entity<Urun>()
                .HasMany(e => e.FavoriKaraListes)
                .WithRequired(e => e.Urun)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Urun>()
                .HasMany(e => e.UrunIceriks)
                .WithRequired(e => e.Urun)
                .WillCascadeOnDelete(false);
        }
    }
}
