using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YesilEv.Core;
using YesilEv.DTO;

namespace YesilEv.Map
{
    public class KullaniciMapping
    {

        public static Kullanici KullaniciEkleDTOToKullanici(KullaniciEkleDTO kullanici)
        {
            return new Kullanici
            {
                Adi = kullanici.Adi,
                Soyadi = kullanici.Soyadi,
                Password = kullanici.Password,
                kullaniciAdi = kullanici.kullaniciAdi,
                KayitTarihi = DateTime.Now,
                AktifMi=kullanici.AktifMi
            };
        }
        public static Kullanici KullaniciGirisDTOtoKullanici(KullaniciGirisDTO kullanici)
        {
            return new Kullanici()
            {
                kullaniciAdi = kullanici.KullaniciAdi,
                Password = kullanici.Parola
            };
        }
        public static Kullanici KullaniciGuncelleDTOToKullanici(KullaniciGuncelleDTO kullanici)
        {
            return new Kullanici()
            {
                Adi = kullanici.Ad,
                AktifMi = true,
                Id = kullanici.Id,
                kullaniciAdi = kullanici.KullaniciAdi,
                Password = kullanici.Parola,
                KayitTarihi = DateTime.Now,
                Soyadi = kullanici.Soyad
            };
        }
        public static KullaniciGuncelleDTO KullaniciToKullaniciGuncelleDTO(Kullanici kullanici)
        {
            return new KullaniciGuncelleDTO()
            {
                Ad = kullanici.Adi,
                Soyad = kullanici.Soyadi,
                KullaniciAdi = kullanici.kullaniciAdi,
                Id = kullanici.Id,
                AktifMi = kullanici.AktifMi,
                Parola = kullanici.Password
            };
        }

    }
}
