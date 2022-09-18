using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YesilEv.DTO
{
    public class UrunEkleDTO
    {

        public string BarkodNo { get; set; }
        public string UrunAdi { get; set; }
        public int KategoriId { get; set; }
        public bool OnayliMi { get; set; }
        public string Resim { get; set; }
        public string Resim2 { get; set; }
        public int UreticiId { get; set; }
        public int KullaniciId { get; set; }
        public DateTime KayitTarihi { get; set; }
        public int OnaylayanId { get; set; }
        public DateTime OnaylanmaTarihi { get; set; }
        public bool AktifMi { get; set; }
    }
}
