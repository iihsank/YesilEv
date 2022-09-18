using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YesilEv.Core;
using YesilEv.DTO;

namespace YesilEv.Map
{
    public class UreticiMapping
    {
        public static Uretici UreticiEkleDTOToUretici(UreticiEkleDTO tedarikci)
        {
            return new Uretici()
            {
                Adı = tedarikci.Ad,
                Adres = tedarikci.Adres,
                AktifMi=true
            };
        }
        public static UreticiGetirDTO UreticiToUreticiGetirDTO(Uretici uretici)
        {
            return new UreticiGetirDTO()
            {
                Id = uretici.Id,
                Ad = uretici.Adı,
                Adres = uretici.Adres
            };
        }
        public static Uretici UreticiGuncelleDTOToUretici(UreticiGuncelleDTO uretici)
        {
            return new Uretici()
            {
                Id = uretici.Id,
                Adı = uretici.Ad,
                Adres = uretici.Adres,
                AktifMi = uretici.AktifMi
            };
        }
    }
}
