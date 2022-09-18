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
    public class UreticiIslemDAL
    {
        NLOGG log = new NLOGG();
        public string UreticiEkle(UreticiEkleDTO dto)
        {
            try
            {
                UreticiDAL dal = new UreticiDAL();
                if (dal.GetAll().Any(a => a.Adı == dto.Ad && a.AktifMi == true))
                {
                    return "mevcut";
                }
                else
                {
                    dal.Add(UreticiMapping.UreticiEkleDTOToUretici(dto));
                    dal.MySaveChanges();
                    log.info(dto.Ad + " " + "isimli üretici eklenmiştir.");
                    return "basarili";
                }
            }
            catch (Exception e)
            {
                log.warning("UreticiIslemDAL UreticiEkle" + e.Message);
            }
            return "basarisiz";
        }

        public List<UreticiGetirDTO> UreticileriGetir()
        {
            List<UreticiGetirDTO> ureticiler = new List<UreticiGetirDTO>();
            UreticiDAL dal = new UreticiDAL();
            List<Uretici> ureticis = dal.GetAll().Where(a => a.AktifMi == true).ToList();
            if (ureticis.Count > 0)
            {
                foreach (Uretici item in ureticis)
                {

                    ureticiler.Add(UreticiMapping.UreticiToUreticiGetirDTO(item));
                }
            }
            return ureticiler;
        }
        public string UreticiGuncelle(UreticiGuncelleDTO dto)
        {
            try
            {
                UreticiDAL dal = new UreticiDAL();
                if (dal.GetAll().Where(a => a.Adı == dto.Ad && a.AktifMi == true).ToList().Count > 1)
                {
                    return "mevcut";
                }
                else
                {
                    using(Model1 db = new Model1())
                    {
                        Uretici uretici = db.Ureticis.Find(dto.Id);
                        uretici.Adı = dto.Ad;
                        uretici.Adres = dto.Adres;
                        uretici.AktifMi = true;
                        db.Ureticis.Attach(uretici);
                        db.Entry(uretici).State = EntityState.Modified;
                        db.SaveChanges();
                        log.info(dto.Ad + "isimli üretici güncellenmiştir.");
                        return "basarili";
                    }
                }

            }
            catch (Exception e)
            {
                log.warning("UreticiIslemDAL UreticiGuncelle "+ e.Message);
            }
            return "basarisiz";
        }
        public bool UreticiSoftDelete(UreticiGetirDTO dto)
        {
            
            using (Model1 db = new Model1())
            {
                UreticiDAL dal = new UreticiDAL();
                try
                {
                    Uretici uretici1 = db.Ureticis.Find(dto.Id);
                    uretici1.AktifMi = false;
                    db.Ureticis.Attach(uretici1);
                    db.Entry(uretici1).State = EntityState.Modified;
                    db.SaveChanges();
                    log.info(dto.Ad + "isimli üretici silinmiştir.");
                    return true;
                }
                catch (Exception e)
                {
                    log.warning("UreticiIslemDAL UreticiSoftDelete " + e.Message);
                }
                return false;
            }
        }

    }
}
