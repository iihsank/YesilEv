using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YesilEv.DTO
{
    public class IcerikGetirDTO
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Aciklama { get; set; }
        public int RiskSeviyesi { get; set; }

        public override string ToString()
        {
            return Ad;
        }
    }
}
