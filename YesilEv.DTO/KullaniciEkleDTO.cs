using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YesilEv.DTO
{
    public class KullaniciEkleDTO
    {
        public string Adi { get; set; }

        public string Soyadi { get; set; }

        public string kullaniciAdi { get; set; }

        public string Password { get; set; }

        public DateTime KayitTarihi { get; set; }

        public bool AktifMi { get; set; }
    }
}
