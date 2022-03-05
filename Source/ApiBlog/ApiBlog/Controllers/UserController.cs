using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiBlog.Models;
using Newtonsoft.Json;

//Controller that manage the operations of sign in and insertion of a new user
namespace ApiBlog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private BlogContext Context { get; }

        public UserController(BlogContext _context)
        {
            this.Context = _context;
        }

        //Performs the signin in for a user 
        [HttpPost]
        [Route("LoginUser")]
        public async Task<string> LoginUser([FromBody] User UserParms)
        {
            string JSONString = string.Empty;

            await Task.Run(() =>
            {
                List<User> Users = this.Context.LoginUser(UserParms.user_name.Trim(), UserParms.user_password.Trim()).ToList();
                JSONString = JsonConvert.SerializeObject(Users);
            });

            return JSONString;
        }

        //Insert a new user in the users table
        [HttpPost]
        [Route("InsertUser")]
        public async Task<string> InsertUser([FromBody] User UserParms)
        {
            string JSONString = string.Empty;

            await Task.Run(() =>
            {
                List<Output> Outputs = this.Context.InsertUser(UserParms.user_name.Trim(), UserParms.user_fullname.Trim(), UserParms.user_password.Trim(), UserParms.user_type.Trim(), UserParms.user_status.Trim()).ToList();
                JSONString = JsonConvert.SerializeObject(Outputs);
            });

            return JSONString;
        }
    }
}
