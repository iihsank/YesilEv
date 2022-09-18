using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YesilEv.Core.Interfaces
{
    public interface IRepo<T> where T:class
    {
        void MySaveChanges();
    }
}
