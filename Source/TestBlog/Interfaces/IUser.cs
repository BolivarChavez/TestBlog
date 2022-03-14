using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestBlog.Interfaces
{
    public interface IUser
    {
        public int LoginUser(List<string> L_Params);

        public int LogoutUser();
    }
}
