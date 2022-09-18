using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YesilEv.DTO
{
    public class UreticiGetirDTO
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Adres { get; set; }

        public override string ToString()
        {
            return Ad;
        }
    }
}
