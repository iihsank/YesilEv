namespace YesilEv.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FavoriKaraListe")]
    public partial class FavoriKaraListe
    {
        public int Id { get; set; }

        public int UrunId { get; set; }

        public int KullaniciId { get; set; }

        public bool FavoriMi { get; set; }

        public bool AktifMi { get; set; }

        public virtual Kullanici Kullanici { get; set; }

        public virtual Urun Urun { get; set; }
    }
}
