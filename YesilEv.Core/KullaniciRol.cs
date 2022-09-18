namespace YesilEv.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KullaniciRol")]
    public partial class KullaniciRol
    {
        public int Id { get; set; }

        public int KullaniciId { get; set; }

        public int RolId { get; set; }

        public DateTime VerilisTarhi { get; set; }

        public bool AktifMi { get; set; }

        public virtual Kullanici Kullanici { get; set; }

        public virtual Rol Rol { get; set; }
    }
}
