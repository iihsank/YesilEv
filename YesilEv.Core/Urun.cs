namespace YesilEv.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Urun")]
    public partial class Urun
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Urun()
        {
            FavoriKaraListes = new HashSet<FavoriKaraListe>();
            UrunIceriks = new HashSet<UrunIcerik>();
        }

        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(MAX)")]
        public string BarkodNo { get; set; }

        [Required]
        [StringLength(150)]
        public string UrunAdi { get; set; }

        public int KategoriId { get; set; }

        public bool? OnayliMi { get; set; }

        public string ResimUrl { get; set; }

        public string Resim2Url { get; set; }

        public int UreticiId { get; set; }

        public int KullaniciId { get; set; }

        public DateTime EklenmeTarih { get; set; }

        public int? OnaylayanId { get; set; }

        public DateTime? OnaylanmaTarihi { get; set; }

        public bool AktifMi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FavoriKaraListe> FavoriKaraListes { get; set; }

        public virtual Kategori Kategori { get; set; }

        public virtual Kullanici Kullanici { get; set; }

        public virtual Kullanici Kullanici1 { get; set; }

        public virtual Uretici Uretici { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UrunIcerik> UrunIceriks { get; set; }
    }
}
