using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YesilEv.DTO
{
    public class FavKaraListGetirDTO
    {
        public string KulAdi { get; set; }
        public List<UrunGetirDTO> urunler { get; set; }
    }
}
