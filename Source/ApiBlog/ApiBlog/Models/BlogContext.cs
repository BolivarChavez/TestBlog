using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Methods that excute operations on the database, executing stored procedures and mapping the results to the appropiate models

namespace ApiBlog.Models
{
    public class BlogContext : DbContext
    {
        //Database Context
        public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostList> PostLists { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Output> Outputs { get; set; }

        //Call store procedure for the log in operation
        public IQueryable<User> LoginUser(string username, string password)
        {
            string sql = "EXEC Blog_Login_User @i_user_name, @i_user_password";

            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@i_user_name", Value = username },
                new SqlParameter { ParameterName = "@i_user_password", Value = password }
            };

            return this.Users.FromSqlRaw<User>(sql, parms.ToArray());
        }

        //Call store procedure for insertion of a post comment
        public IQueryable<Output> InsertComment(Int32 postid, string text, string author)
        {
            string sql = "EXEC Blog_Comment_Insert @i_comment_post_id, @i_comment_text, @i_comment_author, @o_return, @o_msg";

            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@i_comment_post_id", Value = postid },
                new SqlParameter { ParameterName = "@i_comment_text", Value = text },
                new SqlParameter { ParameterName = "@i_comment_author", Value = author },
                new SqlParameter { ParameterName = "@o_return", Value = 0 },
                new SqlParameter { ParameterName = "@o_msg", Value = "" }
            };

            return this.Outputs.FromSqlRaw<Output>(sql, parms.ToArray());
        }

        //Call store procedure for a new user insertion
        public IQueryable<Output> InsertUser(string username, string fullname, string password, string usertype, string userstatus)
        {
            string sql = "EXEC Blog_Insert_User @i_user_name, @i_user_fullname, @i_user_password, @i_user_type, @i_user_status, @o_return, @o_msg";

            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@i_user_name", Value = username },
                new SqlParameter { ParameterName = "@i_user_fullname", Value = fullname },
                new SqlParameter { ParameterName = "@i_user_password", Value = password },
                new SqlParameter { ParameterName = "@i_user_type", Value = usertype },
                new SqlParameter { ParameterName = "@i_user_status", Value = userstatus },
                new SqlParameter { ParameterName = "@o_return", Value = 0 },
                new SqlParameter { ParameterName = "@o_msg", Value = "" }
            };

            return this.Outputs.FromSqlRaw<Output>(sql, parms.ToArray());
        }

        //Call store procedure for the post deletion
        public IQueryable<Output> DeletePost(Int32 postid, string poststatus)
        {
            string sql = "EXEC Blog_Post_Delete @i_post_id, @i_post_status, @o_return, @o_msg";

            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@i_post_id", Value = postid },
                new SqlParameter { ParameterName = "@i_post_status", Value = poststatus },
                new SqlParameter { ParameterName = "@o_return", Value = 0 },
                new SqlParameter { ParameterName = "@o_msg", Value = "" }
            };

            return this.Outputs.FromSqlRaw<Output>(sql, parms.ToArray());
        }

        //Call store procedure for post edition
        public IQueryable<Output> EditPost(Int32 postid, string posttitle, string posttext)
        {
            string sql = "EXEC Blog_Post_Edit @i_post_id, @i_post_title, @i_post_text, @o_return, @o_msg";

            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@i_post_id", Value = postid },
                new SqlParameter { ParameterName = "@i_post_title", Value = posttitle },
                new SqlParameter { ParameterName = "@i_post_text", Value = posttext },
                new SqlParameter { ParameterName = "@o_return", Value = 0 },
                new SqlParameter { ParameterName = "@o_msg", Value = "" }
            };

            return this.Outputs.FromSqlRaw<Output>(sql, parms.ToArray());
        }

        //Call store procedure for the insertion of a new post
        public IQueryable<Output> InsertPost(Int32 authorid, string posttitle, string posttext, string status, DateTime postdate, Int32 editorid)
        {
            string sql = "EXEC Blog_Post_Insert @i_post_author_id, @i_post_title, @i_post_text, @i_post_status, @i_post_date, @i_post_editor, @o_return, @o_msg";

            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@i_post_author_id", Value = authorid },
                new SqlParameter { ParameterName = "@i_post_title", Value = posttitle },
                new SqlParameter { ParameterName = "@i_post_text", Value = posttext },
                new SqlParameter { ParameterName = "@i_post_status", Value = status },
                new SqlParameter { ParameterName = "@i_post_date", Value = postdate },
                new SqlParameter { ParameterName = "@i_post_editor", Value = editorid },
                new SqlParameter { ParameterName = "@o_return", Value = 0 },
                new SqlParameter { ParameterName = "@o_msg", Value = "" }
            };

            return this.Outputs.FromSqlRaw<Output>(sql, parms.ToArray());
        }

        //Call store procedure for publishing a post
        public IQueryable<Output> PublishPost(Int32 postid, string status, DateTime postdate, Int32 editorid)
        {
            string sql = "EXEC Blog_Post_Publish @i_post_id, @i_post_status, @i_post_date, @i_post_editor, @o_return, @o_msg";

            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@i_post_id", Value = postid },
                new SqlParameter { ParameterName = "@i_post_status", Value = status },
                new SqlParameter { ParameterName = "@i_post_date", Value = postdate },
                new SqlParameter { ParameterName = "@i_post_editor", Value = editorid },
                new SqlParameter { ParameterName = "@o_return", Value = 0 },
                new SqlParameter { ParameterName = "@o_msg", Value = "" }
            };

            return this.Outputs.FromSqlRaw<Output>(sql, parms.ToArray());
        }

        //Call store procedure for the the listing of comments 
        public IQueryable<Comment> QueryCommentList(Int32 postid)
        {
            string sql = "EXEC Blog_Query_Comment_List @i_comment_post_id";

            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@i_comment_post_id", Value = postid }
            };

            return this.Comments.FromSqlRaw<Comment>(sql, parms.ToArray());
        }

        //Call store procedure for listing published and not published posts
        public IQueryable<PostList> QueryPostList(Int32 authorid, string status)
        {
            string sql = "EXEC Blog_Query_Post_List @i_post_author_id, @i_post_status";

            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@i_post_author_id", Value = authorid },
                new SqlParameter { ParameterName = "@i_post_status", Value = status }
            };

            return this.PostLists.FromSqlRaw<PostList>(sql, parms.ToArray());
        }

        //Call store procedure for the show all the information of an unique post
        public IQueryable<Post> QueryPostSingle(Int32 postid)
        {
            string sql = "EXEC Blog_Query_Post_Single @i_post_id";

            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@i_post_id", Value = postid }
            };

            return this.Posts.FromSqlRaw<Post>(sql, parms.ToArray());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
