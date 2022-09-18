using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YesilEv.DTO
{
    public class KullaniciGetirDTO
    {
        public int Id { get; set; }
        public int RolId { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Rolu { get; set; }
        public override string ToString()
        {
            return Ad + " " + Soyad;
        }
    }
}
