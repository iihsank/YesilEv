using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using YesilEv.Core;
using YesilEv.Dal.Concrete;
using YesilEv.DTO;
using YesilEv.Log;
using YesilEv.Map;

namespace YesilEv.Dal
{
    public class KullaniciIslemDAL
    {
        NLOGG log = new NLOGG();
        public List<KullaniciGetirDTO> KullanicilariGetir()
        {
            try
            {
                using (Model1 db = new Model1())
                {
                    return db.Kullanicis.Where(a => a.AktifMi == true).Join(db.KullaniciRols, a => a.Id, b => b.KullaniciId, (a, b) =>
                               new { a.Adi, a.Soyadi, a.Id, b.RolId,}).Join(db.Rols, a => a.RolId, b => b.Id, (a, b) =>
                               new KullaniciGetirDTO()
                               {
                                   Ad = a.Adi,
                                   Id = a.Id,
                                   RolId=b.Id,
                                   Soyad = a.Soyadi,
                                   Rolu = b.RolAdi
                               }).ToList();
                }
            }
            catch (Exception e)
            {
                log.warning("KullaniciIslemDAL"+" "+"KullanicilariGetir"+ e.Message);
                throw;
            }
           
        }
        public bool SoftDelete(KullaniciGetirDTO dto)
        {
            try
            {
                using(Model1 db = new Model1())
                {
                    Kullanici kul= db.Kullanicis.Find(dto.Id);
                    kul.AktifMi = false;
                    db.Kullanicis.Attach(kul);
                    db.Entry(kul).State = EntityState.Modified;
                    db.SaveChanges();
                    log.info(kul.Adi + " " + kul.Soyadi + "kayıt silinmiştir.");
                    return true;
                }
            }
            catch (Exception e)
            {
                log.warning("KullaniciIslemDAL" + " " + "SoftDelete" + e.Message);
            }
            return false;
        }
        public string KullaniciGuncelle(KullaniciGuncelleDTO dto)
        {
            try
            {
                KullaniciDAL dal = new KullaniciDAL();
                if (dal.GetAll().Where(a => a.kullaniciAdi == dto.KullaniciAdi).ToList().Count > 1)
                {
                    return "mevcut";
                }
                else
                {
                    using(Model1 db = new Model1())
                    {
                        Kullanici kul= db.Kullanicis.Find(dto.Id);
                        kul.Adi = dto.Ad;
                        kul.Soyadi = dto.Soyad;
                        kul.kullaniciAdi = dto.KullaniciAdi;
                        kul.KayitTarihi = DateTime.Now;
                        kul.AktifMi = true;
                        kul.Password = dto.Parola;                      
                        db.SaveChanges();
                        log.info(kul.Adi + " " + kul.Soyadi + " kullanici kaydını güncellenmiştir.");
                        return "basarili";
                    }
                }
            }
            catch (Exception e)
            {
                log.warning("KullaniciIslemDAL" + " " + "KullaniciGuncelle" + e.Message);             
            }
            return "basarisiz";

        }
        public KullaniciGuncelleDTO KullaniciGetir(int Id)
        {
            try
            {
                KullaniciDAL dal = new KullaniciDAL();
                return KullaniciMapping.KullaniciToKullaniciGuncelleDTO(dal.GetByID(Id));

            }
            catch (Exception e)
            {
                log.warning("KullaniciIslemDAL" + " " + "KullaniciGetir" + e.Message);
                throw;
            }
        }
        public string KullaniciEkle(KullaniciEkleDTO dto)
        {
            
            using(TransactionScope tran = new TransactionScope())
            {
                try
                {
                    using (Model1 db = new Model1())
                    {
                        if (db.Kullanicis.Any(a => a.kullaniciAdi == dto.kullaniciAdi))
                        {
                            return "mevcut";
                        }
                        else
                        {
                            KullaniciDAL dal = new KullaniciDAL();
                            dto.AktifMi = true;
                            dal.Add(KullaniciMapping.KullaniciEkleDTOToKullanici(dto));
                            dal.MySaveChanges();
                            Kullanici kul = db.Kullanicis.Where(a => a.kullaniciAdi == dto.kullaniciAdi).FirstOrDefault();
                            KullaniciRolDAL dal1 = new KullaniciRolDAL();
                            dal1.Add(new KullaniciRol() { KullaniciId = kul.Id, RolId = 1, VerilisTarhi = DateTime.Now, AktifMi = true });
                            dal1.MySaveChanges();
                            tran.Complete();
                            log.info(dto.Adi+" "+dto.Soyadi+"isimli kayıt olmuştur.");
                            return "basarili";
                        }
                    }
                }
                catch (Exception e)
                {
                    tran.Dispose();
                    log.warning("KullaniciIslemDAL" + " " + "KullaniciEkle" + e.Message);
                }
                return "basarisiz";
            }          
        }
        public KullaniciGosterDTO KullaniciGiris(KullaniciGirisDTO kullanici)
        {
            using (Model1 db = new Model1())
            {
                KullaniciGosterDTO kul = null;
                try
                {
                    if (db.Kullanicis.Any(a => a.kullaniciAdi == kullanici.KullaniciAdi && a.Password == kullanici.Parola))
                    {
                        
                        kul= db.Kullanicis.Where(a => a.kullaniciAdi == kullanici.KullaniciAdi)
                           .Join(db.KullaniciRols, a => a.Id, b => b.KullaniciId,(a, b) => new { a.kullaniciAdi, a.Adi, a.Id, a.Password, a.Soyadi, b.RolId })
                           .Join(db.Rols, a => a.RolId, b => b.Id, (a, b) =>
                           new KullaniciGosterDTO
                           {
                               Ad = a.Adi,
                               KullaniciId = a.Id,
                               Soyad = a.Soyadi,
                               KullaniciAdi = a.kullaniciAdi,
                               Sifre = a.Password,
                               Rol = b.RolAdi
                           }).FirstOrDefault();
                        log.info(kullanici.KullaniciAdi + "isimli kullanıcı giriş yapmıştır.");
                        return kul;
                    }
                    else
                    {
                        log.warning("KullaniciIslemDAL" + " " + "KullaniciEkle" + " Kullanici Kaydı mevcut değil.");
                        return kul;
                    }
                    
                }
                catch (Exception e)
                {
                    log.warning("KullaniciIslemDAL" + " " + "KullaniciGiris" + e.Message);
                    throw;
                }

            }
        }
        public List<UrunGetirDTO> KaraListeGetir(int Id)
        {
            try
            {
                using (Model1 db = new Model1())
                {
                    return (from kul in db.Kullanicis
                            where kul.Id == Id
                            join list in db.FavoriKaraListes on kul.Id equals list.KullaniciId
                            where list.FavoriMi == false
                            join ur in db.Uruns on list.UrunId equals ur.Id
                            where ur.AktifMi == true
                            join urıc in db.UrunIceriks on ur.Id equals urıc.UrunId
                            join ıc in db.Iceriks on urıc.IcerikId equals ıc.Id
                            group new { kul, ur, list, urıc, ıc } by ur.UrunAdi
                                into groups
                            select new UrunGetirDTO
                            {
                                Id = groups.Select(a => a.ur.Id).FirstOrDefault(),
                                Ad = groups.Select(a => a.ur.UrunAdi).FirstOrDefault(),
                                BarkodNo = groups.Select(a => a.ur.BarkodNo).FirstOrDefault(),
                                Resim = groups.Select(a => a.ur.ResimUrl).FirstOrDefault(),
                                Resim2=groups.Select(a=>a.ur.Resim2Url).FirstOrDefault(),
                                UreticiId = groups.Select(a => a.ur.UreticiId).FirstOrDefault(),
                                KategoriId = groups.Select(a => a.ur.KategoriId).FirstOrDefault(),
                                Icerikler = groups.Select(a => new IcerikGeDTO() { Id = a.ıc.Id, Ad = a.ıc.Adı, Risk = a.ıc.RiskDegeri }).Distinct().ToList()

                            }).ToList();
                }
            }
            catch (Exception e)
            {
                log.warning("KullaniciIslemDAL" + " " + "KaraListeGetir" + e.Message);
                throw;
            }
        }
        public List<UrunGetirDTO> FavListeGetir(int Id)
        {
            try
            {
                using (Model1 db = new Model1())
                {
                    return (from kul in db.Kullanicis
                            where kul.Id == Id
                            join list in db.FavoriKaraListes on kul.Id equals list.KullaniciId
                            where list.FavoriMi == true
                            join ur in db.Uruns on list.UrunId equals ur.Id
                            where ur.AktifMi == true
                            join urıc in db.UrunIceriks on ur.Id equals urıc.UrunId
                            join ıc in db.Iceriks on urıc.IcerikId equals ıc.Id
                            group new { kul, ur, list, urıc, ıc } by ur.UrunAdi
                                into groups
                            select new UrunGetirDTO
                            {
                                Id = groups.Select(a => a.ur.Id).FirstOrDefault(),
                                Ad = groups.Select(a => a.ur.UrunAdi).FirstOrDefault(),
                                BarkodNo = groups.Select(a => a.ur.BarkodNo).FirstOrDefault(),
                                Resim = groups.Select(a => a.ur.ResimUrl).FirstOrDefault(),
                                Resim2 = groups.Select(a => a.ur.Resim2Url).FirstOrDefault(),
                                UreticiId = groups.Select(a => a.ur.UreticiId).FirstOrDefault(),
                                KategoriId = groups.Select(a => a.ur.KategoriId).FirstOrDefault(),
                                Icerikler = groups.Select(a => new IcerikGeDTO() { Id = a.ıc.Id, Ad = a.ıc.Adı, Risk = a.ıc.RiskDegeri }).Distinct().ToList()

                            }).ToList();
                }
            }
            catch (Exception e)
            {
                log.warning("KullaniciIslemDAL" + " " + "FavListeGetir" + e.Message);
                throw;
            }
        }
        public bool ListeCikar(int kulId,int urunId)
        {
            try
            {
                using (Model1 db = new Model1())
                {
                    FavoriKaraListe list = db.FavoriKaraListes.Where(a => a.KullaniciId == kulId && a.UrunId == urunId).FirstOrDefault();
                    db.FavoriKaraListes.Remove(list);
                    db.SaveChanges();
                    log.info(kulId.ToString() + " Id li kullanıcı " + urunId.ToString() + " Id li ürünü listesinden çıkarmıştır.");
                return true;
                }
            }
            catch (Exception e)
            {
                log.warning("KullaniciIslemDAL" + " " + "ListeCikar" + e.Message);
            }
            return false;
        }

    }
}
