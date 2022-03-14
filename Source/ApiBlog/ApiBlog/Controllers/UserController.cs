using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiBlog.Interfaces;
using ApiBlog.Models;

//Controller that manage the operations of sign in and insertion of a new user
namespace ApiBlog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUser Luser { get; set; }

        public UserController(IUser _user)
        {
            this.Luser = _user;
        }

        //Performs the signin in for a user 
        [HttpPost]
        [Route("LoginUser")]
        public async Task<string> LoginUser([FromBody] User UserParms)
        {
            string JSONString = string.Empty;
            List<string> L_Param = new List<string>();
            
            await Task.Run(() =>
            {
                L_Param.Add(UserParms.user_name.Trim());
                L_Param.Add(UserParms.user_password.Trim());
                JSONString = Luser.LoginUser(L_Param);
            });

            return JSONString;
        }

        //Insert a new user in the users table
        [HttpPost]
        [Route("InsertUser")]
        public async Task<string> InsertUser([FromBody] User UserParms)
        {
            string JSONString = string.Empty;
            List<string> L_Param = new List<string>();

            await Task.Run(() =>
            {
                L_Param.Add(UserParms.user_name.Trim());
                L_Param.Add(UserParms.user_fullname.Trim());
                L_Param.Add(UserParms.user_password.Trim());
                L_Param.Add(UserParms.user_type.Trim());
                L_Param.Add(UserParms.user_status.Trim());
                JSONString = Luser.InsertUser(L_Param);
            });

            return JSONString;
        }
    }
}
