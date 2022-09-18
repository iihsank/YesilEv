using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YesilEv.Core;
using YesilEv.DTO.OdevDTO;

namespace YesilEv.Map.OdevMapping
{
    public class OdevMapping
    {
        public static UrunGetirDTO UrunToUrunGetirDTO(Urun urun)
        {
            return new UrunGetirDTO()
            {
                UrunId = urun.Id,
                Adı = urun.UrunAdi
            };
        }
    }
}
