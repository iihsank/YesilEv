using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using YesilEv.Core;
using YesilEv.DTO;
using YesilEv.Log;
using YesilEv.Map;

namespace YesilEv.Dal.Concrete
{
    public class UrunIslemDAL
    {
        NLOGG log = new NLOGG();
        public bool softDelete(int Id)
        {
            try
            {
                using (Model1 db = new Model1())
                {
                    Urun urun = db.Uruns.Find(Id);
                    urun.AktifMi = false;
                    db.Uruns.Attach(urun);
                    db.Entry(urun).State = EntityState.Modified;
                    db.SaveChanges();
                    log.info(urun.UrunAdi + " " + "isimli ürün silinmiştir.");
                    return true;
                }
            }
            catch (Exception e)
            {
                log.warning("UrunIslemDAL softDelete" + e.Message);
            }
            return false;
        }
        public List<UrunGetirDTO> UrunleriGetir()
        {
            List<UrunGetirDTO> urunler = new List<UrunGetirDTO>();
            try
            {
                using (Model1 db = new Model1())
                {
                    urunler = (from ur in db.Uruns
                               where ur.AktifMi == true && ur.OnayliMi == true
                               join urIc in db.UrunIceriks on ur.Id equals urIc.UrunId
                               join ıc in db.Iceriks on urIc.IcerikId equals ıc.Id
                               group new { ur, urIc, ıc } by ur.Id
                                into groups
                               select new UrunGetirDTO()
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
                    return urunler;
                }
            }
            catch (Exception e)
            {
                log.warning("UrunIslemDAL UrunleriGetirr"+e.Message);
                throw;
            }
           
        }
        public List<UrunOnayDTO> OnayBekleyenUrunleriGetir()
        {
            try
            {
                using (Model1 db = new Model1())
                {
                    return db.Uruns.Where(a => a.AktifMi == true && a.OnayliMi == false).Select(a => new UrunOnayDTO()
                    {
                        Id = a.Id,
                        Ad = a.UrunAdi
                    }).ToList();
                   
                }
            }
            catch (Exception e)
            {
                log.warning("UrunIslemDAL OnayBekleyenUrunleriGetir" + e.Message);
                throw;
            }
           
        }

        public bool UrunOnayla(int Id)
        {
            try
            {
                using (Model1 db = new Model1())
                {
                    Urun urun = db.Uruns.Find(Id);
                    urun.OnaylanmaTarihi = DateTime.Now;
                    urun.OnayliMi = true;
                    db.Uruns.Attach(urun);
                    db.Entry(urun).State = EntityState.Modified;
                    db.SaveChanges();
                    log.info(urun.UrunAdi + " " + "isimli ürün onaylanmıştır.");
                    return true;
                }
            }
            catch (Exception e)
            {
                log.warning("UrunIslemDAL OnayBekleyenUrunleriGetir" + e.Message);
            }
            return false;
           
        }

        public string UrunEkle(UrunEkleDTO dto,List<int> iceriklerId)
        {
            using(TransactionScope tran = new TransactionScope())
            {
                try
                {
                    using(Model1 db = new Model1())
                    {
                        if (db.Uruns.Any(a => a.UrunAdi == dto.UrunAdi && a.AktifMi == true))
                        {
                            return "mevcut";
                        }
                        else
                        {
                            dto.OnaylayanId = dto.KullaniciId;
                            dto.OnaylanmaTarihi = DateTime.Now;
                            dto.AktifMi = true;
                            Urun urun= db.Uruns.Add(UrunMapping.UrunGetirDTOToUrun(dto));
                            foreach (int item in iceriklerId)
                            {
                                db.UrunIceriks.Add(new UrunIcerik
                                {
                                    IcerikId=item,
                                    AktifMi=true,
                                    UrunId=urun.Id
                                });
                            }
                            db.SaveChanges();
                            tran.Complete();
                            log.info(dto.UrunAdi + " " + " isimli ürün eklenmiştir.");
                            return "basarili";
                        }
                    }

                    
                }
                catch (Exception e)
                {
                    tran.Dispose();
                    log.warning("UrunIslemDAL UrunEkle" + e.Message);
                }
                return "basarisiz";
            }
        }
        public string UrunGuncelle(UrunGuncelleDTO dto, List<int> iceriklerId)
        {
            using(TransactionScope tran = new TransactionScope())
            {
                try
                {
                    using(Model1 db= new Model1())
                    {
                        if (db.Uruns.Where(a => a.UrunAdi == dto.UrunAdi && a.AktifMi == true).ToList().Count>1)
                        {
                            return "mevcut";
                        }
                        else
                        {
                            Urun urun= db.Uruns.Find(dto.Id);
                            urun.KategoriId = dto.KategoriId;
                            urun.UreticiId = dto.UreticiId;
                            urun.AktifMi = true;
                            urun.BarkodNo = dto.BarkodNo;
                            urun.EklenmeTarih = urun.EklenmeTarih;
                            urun.KullaniciId = urun.KullaniciId;
                            urun.OnaylayanId = urun.OnaylayanId;
                            urun.OnayliMi = true;
                            urun.ResimUrl = dto.Resim;
                            urun.Resim2Url = dto.Resim2;
                            urun.UrunAdi = dto.UrunAdi;
                            db.Uruns.Attach(urun);
                            db.Entry(urun).State = EntityState.Modified;
                            foreach (UrunIcerik item in db.UrunIceriks.Where(a=>a.UrunId==urun.Id).ToList())
                            {
                                db.UrunIceriks.Remove(item);
                            }
                            foreach (int item in iceriklerId)
                            {
                                db.UrunIceriks.Add(new UrunIcerik()
                                {
                                   IcerikId=item,
                                    AktifMi = true,
                                    UrunId=urun.Id
                                });
                            }
                            db.SaveChanges();
                            tran.Complete();
                            log.info(dto.UrunAdi + " " + " isimli ürün güncellenmiştir.");
                            return "basarili";
                        }
                    }
                }
                catch (Exception e)
                {
                    tran.Dispose();
                    log.warning("UrunIslemDAL UrunGuncelle" + e.Message);
                }
                return "basarisiz";
            }
        }
    }
}
