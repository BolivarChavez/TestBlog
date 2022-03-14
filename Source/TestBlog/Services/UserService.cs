using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBlog.Models;
using TestBlog.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace TestBlog.Services
{
    public class UserService : IUser
    {
        public UserService(IConfiguration configuration, ISesion sesion)
        {
            Configuration = configuration;
            _sesion = sesion;
        }

        public IConfiguration Configuration { get; }
        private readonly ISesion _sesion;

        //Perform user sign in 
        public int LoginUser(List<string> L_Params)
        {
            string api_url = Configuration["ApiUrl"].Trim();
            List<User> L_user = new List<User>();
            int response;

            L_user = Operations.LoginUser(api_url, L_Params[0].ToString().Trim(), L_Params[1].ToString().Trim());
            var fuser = L_user.First();

            if (fuser.user_id != 0)
            {
                _sesion.ValueSet("user_id", fuser.user_id.ToString());
                _sesion.ValueSet("user_type", fuser.user_type.ToString());
                _sesion.ValueSet("full_name", fuser.user_fullname.ToString());

                if (fuser.user_type == "W")
                {
                    response = 1;
                }
                else
                {
                    response = 2;
                }
            }
            else
                response = 3;

            return response;
        }

        //Perform user Sign out 
        public int LogoutUser()
        {
            _sesion.ValueSet("user_id", "");
            _sesion.ValueSet("user_type", "");
            _sesion.ValueSet("full_name", "");
            _sesion.ValueSet("post_id", "");

            return 0;
        }
    }
}
