using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBlog.Interfaces
{
    public interface IUser
    {
        public string LoginUser(List<string> L_Params);
        public string InsertUser(List<string> L_Params);
    }
}
