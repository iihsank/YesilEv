using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YesilEv.Core;
using YesilEv.DTO;

namespace YesilEv.Map
{
    public class KategoriMapping
    {
        public static Kategori KategoriEkleDTOToKategori(KategoriEkleDTO kategori)
        {
            return new Kategori
            {
                Adi = kategori.Ad,
                Aciklama = kategori.Aciklama,
                AktifMi = true
            };
        }
        public static KategoriGetirDTO KategoriToKategoriGetirDTO(Kategori kategori)
        {
            return new KategoriGetirDTO
            {
                Id = kategori.Id,
                Adı = kategori.Adi,
                Aciklama = kategori.Aciklama
            };
        }
        public  static Kategori KategoriGetirDTOToKategori(KategoriGetirDTO kategori)
        {
            return new Kategori()
            {
                Adi = kategori.Adı,
                Aciklama = kategori.Aciklama,
                Id = kategori.Id
            };
        }
        public static Kategori KategoriGuncelleDTOToKategori(KategoriGuncelleDTO kategori)
        {
            return new Kategori()
            {
                Adi = kategori.Adı,
                Aciklama = kategori.Aciklama,
                Id = kategori.Id,
                AktifMi=kategori.AktifMi
            };
        }
    }
}
