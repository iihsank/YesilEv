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
    public class IcerikIslemDAL
    {
        NLOGG log = new NLOGG();
        public string IcerikEkle(IcerikEkleDTO dto)
        {
            try
            {
                IcerikDAL dal = new IcerikDAL();
                if (dal.GetAll().Any(a => a.Adı == dto.Ad && a.AktifMi == true))
                {
                    return "mevcut";
                }
                else
                {
                    dal.Add(IcerikMapping.IcerikEkleDTOToIcerik(dto));
                    dal.MySaveChanges();
                    log.info(dto.Ad + " " + "isimli içerik eklenmiştir.");
                    return "basarili";
                }
            }
            catch (Exception e)
            {
                log.warning("IcerikIslemDAL IcerikEkle" + e.Message);
            }
            return "basarisiz";
        }
        public List<IcerikGetirDTO> IcerikleriGetir()
        {
            List<IcerikGetirDTO> icerikler = new List<IcerikGetirDTO>();
            IcerikDAL dal = new IcerikDAL();
            List<Icerik> iceriks = dal.GetAll().Where(a => a.AktifMi == true).ToList();
            if (iceriks.Count > 0)
            {
                foreach (Icerik item in iceriks)
                {
                    icerikler.Add(IcerikMapping.IcerikToIcerikGetirDTO(item));
                }
            }
            return icerikler;
        }
        public List<IcerikGeDTO> IceriklerGetir()
        {
            List<IcerikGeDTO> icerikler = new List<IcerikGeDTO>();
            IcerikDAL dal = new IcerikDAL();
            List<Icerik> iceriks = dal.GetAll().Where(a => a.AktifMi == true).ToList();
            if (iceriks.Count > 0)
            {
                foreach (Icerik item in iceriks)
                {
                    icerikler.Add(IcerikMapping.IcerikToIcerikGeDTO(item));
                }
            }
            return icerikler;
        }
        public string IcerikGuncelle(IcerikGuncelleDTO dto)
        {
            try
            {
                IcerikDAL dal = new IcerikDAL();
                if (dal.GetAll().Where(a => a.Adı == dto.Adı && a.AktifMi == true).ToList().Count > 1)
                {
                    return "mevcut";
                }
                else
                {
                    using (Model1 db = new Model1())
                    {
                        Icerik ıcerik = db.Iceriks.Find(dto.Id);
                        ıcerik.Adı = dto.Adı;
                        ıcerik.Aciklamasi = dto.Aciklama;
                        ıcerik.AktifMi = true;
                        ıcerik.RiskDegeri = dto.RiskSeviyesi;
                        db.Iceriks.Attach(ıcerik);
                        db.Entry(ıcerik).State = EntityState.Modified;
                        db.SaveChanges();
                        log.info(dto.Adı + " " + "isimli içerik güncellenmiştir.");
                        return "basarili";
                    }
                }

            }
            catch (Exception e)
            {
                log.warning("IcerikIslemDAL IcerikGuncelle" + e.Message);
            }
            return "basarisiz";
        }
        public bool IcerikSoftDelete(IcerikGetirDTO dto)
        {

            using (Model1 db = new Model1())
            {
                IcerikDAL dal = new IcerikDAL();
                try
                {
                    Icerik ıcerik = db.Iceriks.Find(dto.Id);
                    ıcerik.AktifMi = false;
                    db.Iceriks.Attach(ıcerik);
                    db.Entry(ıcerik).State = EntityState.Modified;
                    db.SaveChanges();
                    log.info(dto.Ad + " " + "isimli içerik silinmiştir.");
                    return true;
                }
                catch (Exception e)
                {
                    log.warning("IcerikIslemDAL IcerikSoftDelete" + e.Message);

                }
                return false;
            }
        }
    }
}
