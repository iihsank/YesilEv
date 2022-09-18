using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YesilEv.Core;
using YesilEv.DTO;

namespace YesilEv.Map
{
    public class IcerikMapping
    {
        public static Icerik IcerikEkleDTOToIcerik(IcerikEkleDTO ıcerik)
        {
            return new Icerik()
            {
                Adı = ıcerik.Ad,
                Aciklamasi = ıcerik.Aciklama,
                RiskDegeri=ıcerik.RiskSeviyesi,
                AktifMi = true
            };
        }
        public static IcerikGetirDTO IcerikToIcerikGetirDTO(Icerik ıcerik)
        {
            return new IcerikGetirDTO()
            {
                Id = ıcerik.Id,
                Ad = ıcerik.Adı,
                Aciklama = ıcerik.Aciklamasi,
                RiskSeviyesi=ıcerik.RiskDegeri
                
            };
        }
        public static IcerikGeDTO IcerikToIcerikGeDTO(Icerik icerik)
        {
            return new IcerikGeDTO()
            {
                Id = icerik.Id,
                Ad = icerik.Adı,
                Risk = icerik.RiskDegeri
            };
        }
    }
}
