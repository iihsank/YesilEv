using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YesilEv.DTO
{
    public class UrunGetirDTO
    {
        public int Id { get; set; }
        public int KategoriId { get; set; }
        public int UreticiId { get; set; }
        public string BarkodNo { get; set; }
        public string Ad { get; set; }
        public string Resim { get; set; }
        public string Resim2 { get; set; }
        public List<IcerikGeDTO> Icerikler { get; set; }
        public override string ToString()
        {
            return Ad;
        }
    }
}
