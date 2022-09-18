using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YesilEv.DTO
{
    public class KategoriGetirDTO
    {
        public int Id { get; set; }
        public string Adı { get; set; }
        public string Aciklama { get; set; }
        public override string ToString()
        {
            return Adı;
        }
    }
}
