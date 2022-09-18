namespace YesilEv.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Uretici")]
    public partial class Uretici
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Uretici()
        {
            Uruns = new HashSet<Urun>();
        }

        public int Id { get; set; }

        [Required]
        public string Adı { get; set; }

        [Required]
        public string Adres { get; set; }

        public bool AktifMi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Urun> Uruns { get; set; }
    }
}
