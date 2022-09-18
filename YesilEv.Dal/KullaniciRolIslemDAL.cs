using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using YesilEv.Core;
using YesilEv.Dal.Concrete;
using YesilEv.DTO;
using YesilEv.Log;

namespace YesilEv.Dal
{
    public class KullaniciRolIslemDAL
    {
        public bool AdminYap(AdminYapDTO dto)
        {
            NLOGG log = new NLOGG();
            using(TransactionScope tran = new TransactionScope())
            {
                try
                {
                    KullaniciRolDAL dal = new KullaniciRolDAL();
                    KullaniciRol kulRol =dal.GetAll().Where(a => a.KullaniciId == dto.KulId && a.RolId == dto.RolId).FirstOrDefault();
                    dal.Delete(kulRol);
                    dal.Add(new KullaniciRol()
                    {
                        AktifMi = true,
                        KullaniciId = dto.KulId,
                        RolId = 2,
                        VerilisTarhi = DateTime.Now
                    });
                    dal.MySaveChanges();
                    tran.Complete();
                    log.info(dto.KulId.ToString() + " " + "ıd li kullanıcı admin yapılmıştır.");
                    return true;
                }
                catch (Exception e)
                {                    
                    tran.Dispose();
                    log.warning("KullaniciRolIslemDAL AdminYap " + e.Message);   
                }
                return false;
            }
        }
    }
}
