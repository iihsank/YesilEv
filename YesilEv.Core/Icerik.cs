namespace YesilEv.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Icerik")]
    public partial class Icerik
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Icerik()
        {
            UrunIceriks = new HashSet<UrunIcerik>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Adı { get; set; }

        [Required]
        public string Aciklamasi { get; set; }

        public bool AktifMi { get; set; }
        public int RiskDegeri { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UrunIcerik> UrunIceriks { get; set; }
    }
}
