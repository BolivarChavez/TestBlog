using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiBlog.Models;
using ApiBlog.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

//Class for Comment services, insert and query comments related to a single post

namespace ApiBlog.Services
{
    public class CommentServices : IComment
    {
        private readonly BlogContext _dbContext;
        public CommentServices(BlogContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string InsertComment(List<string> L_Params)
        {
            string sql = "EXEC Blog_Comment_Insert @i_comment_post_id, @i_comment_text, @i_comment_author, @o_return, @o_msg";

            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@i_comment_post_id", Value = Int32.Parse(L_Params[0].ToString().Trim()) },
                new SqlParameter { ParameterName = "@i_comment_text", Value = L_Params[1].ToString().Trim() },
                new SqlParameter { ParameterName = "@i_comment_author", Value = L_Params[2].ToString().Trim() },
                new SqlParameter { ParameterName = "@o_return", Value = 0 },
                new SqlParameter { ParameterName = "@o_msg", Value = "" }
            };

            return (JsonConvert.SerializeObject(_dbContext.Outputs.FromSqlRaw<Output>(sql, parms.ToArray())));
        }

        public string QueryCommentList(List<string> L_Params)
        {
            string sql = "EXEC Blog_Query_Comment_List @i_comment_post_id";

            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@i_comment_post_id", Value = Int32.Parse(L_Params[0].ToString().Trim()) }
            };

            return (JsonConvert.SerializeObject(_dbContext.Comments.FromSqlRaw<Comment>(sql, parms.ToArray())));
        }
    }
}
