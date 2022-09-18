namespace YesilEv.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UrunIcerik")]
    public partial class UrunIcerik
    {
        public int Id { get; set; }

        public int IcerikId { get; set; }

        public int UrunId { get; set; }

        public bool AktifMi { get; set; }

        public virtual Icerik Icerik { get; set; }

        public virtual Urun Urun { get; set; }
    }
}
