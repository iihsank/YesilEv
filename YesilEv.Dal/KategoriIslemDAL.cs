using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YesilEv.Core;
using YesilEv.Dal.Concrete;
using YesilEv.DTO;
using YesilEv.Log;
using YesilEv.Map;

namespace YesilEv.Dal
{
    public class KategoriIslemDAL
    {
        NLOGG log = new NLOGG();
        public string KategoriEkle(KategoriEkleDTO dto)
        {
            KategoriDAL dal = new KategoriDAL();
            try
            {
                if (dal.GetAll().Any(a => a.Adi == dto.Ad && a.AktifMi == true))
                {
                    return "mevcut";
                }
                else
                {
                    dal.Add(KategoriMapping.KategoriEkleDTOToKategori(dto));
                    dal.MySaveChanges();
                    log.info(dto.Ad + " " + "isimli kategori eklenmiştir.");
                    return "basarili";
                }
            }
            catch (Exception e)
            {
                log.warning("KategoriIslemDAL KategoriEkle" + " " + e.Message);
            }
            return "basarisiz";

        }
        public List<KategoriGetirDTO> KategorileriGetir()
        {
            List<KategoriGetirDTO> kategoriler = new List<KategoriGetirDTO>();
            KategoriDAL dal = new KategoriDAL();
            List<Kategori> kategoris= dal.GetAll().Where(a => a.AktifMi == true).ToList();
            if (kategoris.Count > 0)
            {
                foreach (Kategori item in kategoris)
                {
                    kategoriler.Add(KategoriMapping.KategoriToKategoriGetirDTO(item));
                }
            }

            return kategoriler;
        }
        public string KategoriDüzenle(KategoriGuncelleDTO dto)
        {
            try
            {
                KategoriDAL dal = new KategoriDAL();
                if (dal.GetAll().Where(a => a.Adi == dto.Adı && a.AktifMi == true).ToList().Count > 1)
                {
                    return "mevcut";
                }
                else
                {
                    using (Model1 db = new Model1())
                    {
                        Kategori kategori = db.Kategoris.Find(dto.Id);
                        kategori.Adi = dto.Adı;
                        kategori.Aciklama = dto.Aciklama;
                        kategori.AktifMi = true;
                        db.Kategoris.Attach(kategori);
                        db.Entry(kategori).State = EntityState.Modified;
                        db.SaveChanges();
                        log.info(dto.Adı + " " + "isimli kategori güncellenmiştir.");
                        return "basarili";
                    }
                }
            }
            catch (Exception e)
            {
                log.warning("KategoriIslemDAL KategoriDuzenle" + e.Message);
            }
            return "basarisiz";
        }


        public bool SoftDelete(KategoriGetirDTO kategori)
        {
            using (Model1 db = new Model1())
            {
                KategoriDAL dal = new KategoriDAL();
                try
                {
                    Kategori kategori1 = db.Kategoris.Find(kategori.Id);
                    kategori1.AktifMi = false;
                    db.Kategoris.Attach(kategori1);
                    db.Entry(kategori1).State = EntityState.Modified;
                    db.SaveChanges();
                    log.info(kategori.Adı + " " + "isimli kategori silinmistir.");
                    return true;
                }
                catch (Exception e)
                {
                    log.warning("KategoriIslemDAL SoftDelete" + e.Message);
                }
                return false;
            }
        }
    }
}
