using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiBlog.Models;
using ApiBlog.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

//Class for Post Services. Insert, update, publics, query and delete posts

namespace ApiBlog.Services
{
    public class PostServices : IPost
    {
        private readonly BlogContext _dbContext;
        public PostServices(BlogContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string InsertPost(List<string> L_Params)
        {
            string sql = "EXEC Blog_Post_Insert @i_post_author_id, @i_post_title, @i_post_text, @i_post_status, @i_post_date, @i_post_editor, @o_return, @o_msg";

            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@i_post_author_id", Value = Int32.Parse(L_Params[0].ToString().Trim()) },
                new SqlParameter { ParameterName = "@i_post_title", Value = L_Params[1].ToString().Trim() },
                new SqlParameter { ParameterName = "@i_post_text", Value = L_Params[2].ToString().Trim() },
                new SqlParameter { ParameterName = "@i_post_status", Value = L_Params[3].ToString().Trim() },
                new SqlParameter { ParameterName = "@i_post_date", Value = DateTime.Now },
                new SqlParameter { ParameterName = "@i_post_editor", Value = Int32.Parse(L_Params[4].ToString().Trim()) },
                new SqlParameter { ParameterName = "@o_return", Value = 0 },
                new SqlParameter { ParameterName = "@o_msg", Value = "" }
            };

            return (JsonConvert.SerializeObject(_dbContext.Outputs.FromSqlRaw<Output>(sql, parms.ToArray())));
        }

        public string EditPost(List<string> L_Params)
        {
            string sql = "EXEC Blog_Post_Edit @i_post_id, @i_post_title, @i_post_text, @o_return, @o_msg";

            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@i_post_id", Value = Int32.Parse(L_Params[0].ToString().Trim()) },
                new SqlParameter { ParameterName = "@i_post_title", Value = L_Params[1].ToString().Trim() },
                new SqlParameter { ParameterName = "@i_post_text", Value = L_Params[2].ToString().Trim() },
                new SqlParameter { ParameterName = "@o_return", Value = 0 },
                new SqlParameter { ParameterName = "@o_msg", Value = "" }
            };

            return (JsonConvert.SerializeObject(_dbContext.Outputs.FromSqlRaw<Output>(sql, parms.ToArray())));
        }

        public string PublishPost(List<string> L_Params)
        {
            string sql = "EXEC Blog_Post_Publish @i_post_id, @i_post_status, @i_post_date, @i_post_editor, @o_return, @o_msg";

            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@i_post_id", Value = Int32.Parse(L_Params[0].ToString().Trim()) },
                new SqlParameter { ParameterName = "@i_post_status", Value = L_Params[1].ToString().Trim() },
                new SqlParameter { ParameterName = "@i_post_date", Value = DateTime.Now },
                new SqlParameter { ParameterName = "@i_post_editor", Value = Int32.Parse(L_Params[2].ToString().Trim()) },
                new SqlParameter { ParameterName = "@o_return", Value = 0 },
                new SqlParameter { ParameterName = "@o_msg", Value = "" }
            };

            return (JsonConvert.SerializeObject(_dbContext.Outputs.FromSqlRaw<Output>(sql, parms.ToArray())));
        }

        public string DeletePost(List<string> L_Params)
        {
            string sql = "EXEC Blog_Post_Delete @i_post_id, @i_post_status, @o_return, @o_msg";

            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@i_post_id", Value = Int32.Parse(L_Params[0].ToString().Trim()) },
                new SqlParameter { ParameterName = "@i_post_status", Value = L_Params[1].ToString().Trim() },
                new SqlParameter { ParameterName = "@o_return", Value = 0 },
                new SqlParameter { ParameterName = "@o_msg", Value = "" }
            };

            return (JsonConvert.SerializeObject(_dbContext.Outputs.FromSqlRaw<Output>(sql, parms.ToArray())));
        }

        public string QueryPost(List<string> L_Params)
        {
            string JsonString = string.Empty;

            if (Int32.Parse(L_Params[2].ToString().Trim()) == 0)
            {
                string sql = "EXEC Blog_Query_Post_List @i_post_author_id, @i_post_status";

                List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@i_post_author_id", Value = Int32.Parse(L_Params[0].ToString().Trim()) },
                new SqlParameter { ParameterName = "@i_post_status", Value = L_Params[1].ToString().Trim() }
            };

                JsonString = JsonConvert.SerializeObject(_dbContext.PostLists.FromSqlRaw<PostList>(sql, parms.ToArray()));
            }
            else
            {
                string sql = "EXEC Blog_Query_Post_Single @i_post_id";

                List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@i_post_id", Value = Int32.Parse(L_Params[2].ToString().Trim()) }
            };

                JsonString = JsonConvert.SerializeObject(_dbContext.Posts.FromSqlRaw<Post>(sql, parms.ToArray()));
            }

            return JsonString;
        }
    }
}
