using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YesilEv.DTO
{
    public class IcerikGuncelleDTO
    {
        public int Id { get; set; }
        public string Adı { get; set; }
        public string Aciklama { get; set; }
        public bool AktifMi { get; set; }
        public int RiskSeviyesi { get; set; }
    }
}
