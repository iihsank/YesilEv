using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YesilEv.Core;
using YesilEv.DTO;


namespace YesilEv.Map
{
    public class UrunMapping
    {
        public static Urun UrunGetirDTOToUrun(UrunEkleDTO urun)
        {
            return new Urun()
            {

                BarkodNo = urun.BarkodNo,
                UrunAdi = urun.UrunAdi,
                KategoriId = urun.KategoriId,
                OnayliMi = urun.OnayliMi,
                ResimUrl = urun.Resim,
                Resim2Url=urun.Resim2,
                UreticiId = urun.UreticiId,
                KullaniciId = urun.KullaniciId,
                EklenmeTarih = DateTime.Now,
                AktifMi = urun.AktifMi,
                OnaylayanId=urun.OnaylayanId,
                OnaylanmaTarihi=urun.OnaylanmaTarihi
                
            };
        }
        public static UrunOnayDTO UrunToUrunOnayDTO(Urun urun)
        {
            return new UrunOnayDTO()
            {
                Id = urun.Id,
                Ad = urun.UrunAdi
            };
        }
    }
}
