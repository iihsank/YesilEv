using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YesilEv.DTO.OdevDTO
{
    public class UrunGetirDTO
    {
        public int UrunId { get; set; }
        public string Adı { get; set; }
        public override string ToString()
        {
            return Adı;
        }
    }
}
