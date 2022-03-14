using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiBlog.Models;
using ApiBlog.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

//Class for user services. For now only the LoginUser method it is been used.

namespace ApiBlog.Services
{
    public class UserServices : IUser
    {
        private readonly BlogContext _dbContext;
        public UserServices(BlogContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string LoginUser(List<string> L_Params)
        {
            string sql = "EXEC Blog_Login_User @i_user_name, @i_user_password";

            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@i_user_name", Value = L_Params[0].ToString().Trim() },
                new SqlParameter { ParameterName = "@i_user_password", Value = L_Params[1].ToString().Trim() }
            };

            return (JsonConvert.SerializeObject(_dbContext.Users.FromSqlRaw<User>(sql, parms.ToArray())));
        }

        public string InsertUser(List<string> L_Params)
        {
            string sql = "EXEC Blog_Insert_User @i_user_name, @i_user_fullname, @i_user_password, @i_user_type, @i_user_status, @o_return, @o_msg";

            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@i_user_name", Value = L_Params[0].ToString().Trim() },
                new SqlParameter { ParameterName = "@i_user_fullname", Value = L_Params[1].ToString().Trim() },
                new SqlParameter { ParameterName = "@i_user_password", Value = L_Params[2].ToString().Trim() },
                new SqlParameter { ParameterName = "@i_user_type", Value = L_Params[3].ToString().Trim() },
                new SqlParameter { ParameterName = "@i_user_status", Value = L_Params[4].ToString().Trim() },
                new SqlParameter { ParameterName = "@o_return", Value = 0 },
                new SqlParameter { ParameterName = "@o_msg", Value = "" }
            };

            return (JsonConvert.SerializeObject(_dbContext.Outputs.FromSqlRaw<Output>(sql, parms.ToArray())));
        }

    }
}
