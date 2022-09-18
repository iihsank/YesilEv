using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YesilEv.Core;
using YesilEv.Dal.Concrete;
using YesilEv.DTO.OdevDTO;
using YesilEv.Map.OdevMapping;

namespace YesilEv.Dal
{
    public class OdevDAL
    {
        public List<UrunGetirDTO> UrunleriGetir()
        {
            List<UrunGetirDTO> urunler = null;
            UrunDAL dal = new UrunDAL();
            using (Model1 db= new Model1())
            {
                foreach (Urun item in db.Uruns.Where(a => a.AktifMi == true))
                {
                    urunler.Add(OdevMapping.UrunToUrunGetirDTO(item));
                }
                return urunler;
            }
        }
        public int MaddeSayisi(int Id)
        {
            using(Model1 db= new Model1())
            {
                return db.UrunIceriks.Where(a => a.UrunId == Id).ToList().Count();
            }
        }
        public List<UrunGetirDTO> UrunGetir(string ara)
        {
            List<UrunGetirDTO> urunler = null;
            using(Model1 db= new Model1())
            {
                if (!(db.Iceriks.Any(a => a.Adı == ara)))
                {
                    return urunler;
                }
                else
                {
                   return  db.Iceriks.Where(a => a.Adı == ara).Join(db.UrunIceriks, a => a.Id, b => b.IcerikId, (a, b) => new { b.UrunId })
                        .Join(db.Uruns, a => a.UrunId, b => b.Id, (a, b) => new UrunGetirDTO { UrunId = b.Id, Adı = b.UrunAdi }).ToList();
                }
            }
        }
        public int Onay()
        {
            UrunDAL dal = new UrunDAL();

            List<Urun> urun = dal.GetAll().Where(a => a.OnayliMi == false && a.EklenmeTarih.Month == DateTime.Now.Month).ToList();
            List<Urun> uruns = null;
            using (Model1 db = new Model1())
            {
                return db.Uruns.Where(a => a.OnayliMi == false && a.EklenmeTarih.Month == DateTime.Now.Month).ToList().Count();
            }
        }
        public List<UrunGetirDTO> EnRiskliUrunler()
        {
            using(Model1 db=new Model1())
            {
                return db.Iceriks.Where(a => a.RiskDegeri == 5).Join(db.UrunIceriks, a => a.Id, b => b.UrunId, (a, b) => new { b.UrunId })
                    .Join(db.Uruns, a => a.UrunId, b => b.Id, (a, b) => new UrunGetirDTO { Adı = b.UrunAdi, UrunId = b.Id }).ToList();
            }
        }
        public List<UrunGetirrDTO> FavoriListIlkUc()
        {
            List<UrunGetirrDTO> urunler = null;
            using (Model1 db = new Model1())
            {
               return (from fl in db.FavoriKaraListes
                            join ur in db.Uruns on fl.UrunId equals ur.Id
                            where ur.AktifMi == true
                            group new { fl, ur } by ur.UrunAdi
                           into groups
                            select new UrunGetirrDTO
                            {
                                Ad = groups.Select(a => a.ur.UrunAdi).FirstOrDefault(),
                                Sayi = groups.Select(a => a.fl.UrunId == a.ur.Id).Count()
                            }).OrderBy(a => a.Sayi).Take(3).ToList();
            }
        }
        public Dictionary<string,int> KullaniciAdminSayisi()
        {
            Dictionary<string, int> donder = null;
            using(Model1 db= new Model1())
            {

                donder.Add("Admin", db.Kullanicis.Join(db.KullaniciRols, a => a.Id, b => b.KullaniciId, (a, b) => new { a.kullaniciAdi, b.RolId }).Where(a => a.RolId == 2).Count());
                donder.Add("Kullanici", db.Kullanicis.Join(db.KullaniciRols, a => a.Id, b => b.KullaniciId, (a, b) => new { a.kullaniciAdi, b.RolId }).Where(a => a.RolId == 1).Count());
                return donder;
            }
        }
        public List<UrunGetirrDTO> AlerjenListIlkUc()
        {
            List<UrunGetirrDTO> urunler = null;
            using (Model1 db = new Model1())
            {
                return (from u in db.Uruns
                        join ui in db.UrunIceriks on u.Id equals ui.UrunId
                        join i in db.Iceriks on ui.IcerikId equals i.Id
                        where i.RiskDegeri == 5
                        group new { u, ui, i } by u.UrunAdi
                        into groups
                        select new UrunGetirrDTO
                        {
                            Ad = groups.Select(a => a.u.UrunAdi).FirstOrDefault()
                        }).ToList();
            }
        }
        public void EnCokRiskliUrunTutanKullanicilar() 
        { 
            using(Model1 db = new Model1())
            {
                var a = (from k in db.Kullanicis
                         where k.AktifMi == true
                         join fl in db.FavoriKaraListes on k.Id equals fl.KullaniciId
                         join u in db.Uruns on fl.UrunId equals u.Id
                         join ui in db.UrunIceriks on u.Id equals ui.UrunId
                         join i in db.Iceriks on ui.IcerikId equals i.Id
                         where i.RiskDegeri == 5
                         group new { k, fl, u, ui, i } by k.Adi
                        into groups
                         select new
                         {
                             c = groups.Select(b => b.k.Adi).FirstOrDefault(),
                             e = groups.Select(b => b.u.UrunAdi).Count()
                         }
                        ).Take(3).ToList();
            }
        }

    }
}
