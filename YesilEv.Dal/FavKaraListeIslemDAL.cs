using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YesilEv.Core;
using YesilEv.Log;

namespace YesilEv.Dal
{
    public class FavKaraListeIslemDAL
    {
        NLOGG log = new NLOGG();
        public string KaraListEkle(int kulId,int UrunId)
        {
            try
            {
                using (Model1 db = new Model1())
                {
                    if (db.FavoriKaraListes.Any(a => a.KullaniciId == kulId && a.UrunId == UrunId))
                    {
                        return "mevcut";
                    }
                    else
                    {
                        db.FavoriKaraListes.Add(new FavoriKaraListe()
                        {
                            FavoriMi = false,
                            KullaniciId = kulId,
                            UrunId = UrunId,
                            AktifMi = true
                        });
                        db.SaveChanges();
                        log.info(kulId + " " + "ıd li kullanıcı" + " " + UrunId + " " + "ıd li ürünü kara listesine eklemiştir.");
                        return "basarili";
                    }
                }
            }
            catch (Exception e)
            {
                log.warning("FavKaraListeIslemDAL KaraListEkle" + e.Message);
            }
            return "basarisiz";
        }
        public string FavListEkle(int kulId, int UrunId)
        {
            try
            {
                using (Model1 db = new Model1())
                {
                    if (db.FavoriKaraListes.Any(a => a.KullaniciId == kulId && a.UrunId == UrunId))
                    {
                        return "mevcut";
                    }
                    else
                    {
                        db.FavoriKaraListes.Add(new FavoriKaraListe()
                        {
                            FavoriMi = true,
                            KullaniciId = kulId,
                            UrunId = UrunId,
                            AktifMi = true
                        });
                        db.SaveChanges();
                        log.info(kulId + " " + "ıd li kullanıcı" + " " + UrunId + " " + "ıd li ürünü favori listesine eklemiştir.");
                        return "basarili";
                    }
                }
            }
            catch (Exception e)
            {
                log.warning("FavKaraListeIslemDAL FavListEkle" + e.Message);
            }
            return "basarisiz";
            
        }
    }
}
