using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBlog.Models;
using TestBlog.Interfaces;
using System.Text.Json;

namespace TestBlog.Services 
{
    public class PostService : IPost
    {
        public PostService(IConfiguration configuration, ISesion sesion)
        {
            Configuration = configuration;
            _sesion = sesion;
        }

        public IConfiguration Configuration { get; }
        private readonly ISesion _sesion;

        //Operation for the insertion of a new post
        public string InsertPost(List<string> L_Params)
        {
            string api_url = Configuration["ApiUrl"].Trim();
            List<Output> L_output = new List<Output>();
            string response;

            if (L_Params[0].ToString().Trim() == "0")
                L_output = Operations.InsertPost(api_url, Int32.Parse(L_Params[3].ToString().Trim()), L_Params[1].ToString().Trim(), L_Params[2].ToString().Trim(), "N", 0);
            else
                L_output = Operations.EditPost(api_url, Int32.Parse(L_Params[0].ToString().Trim()), L_Params[1].ToString().Trim(), L_Params[2].ToString().Trim());

            response = JsonSerializer.Serialize<List<Output>>(L_output);
            return response;
        }

        //Operation for post publishing
        public string PublishPost(List<string> L_Params)
        {
            string api_url = Configuration["ApiUrl"].Trim();
            List<Output> L_output = new List<Output>();
            string response;

            L_output = Operations.PublishPost(api_url, Int32.Parse(L_Params[0].ToString().Trim()), "A", Int32.Parse(L_Params[1].ToString().Trim()));
            response = JsonSerializer.Serialize<List<Output>>(L_output);

            return response;
        }

        //Operation for post deletion
        public string DeletePost(List<string> L_Params)
        {
            string api_url = Configuration["ApiUrl"].Trim();
            List<Output> L_output = new List<Output>();
            string response;

            L_output = Operations.DeletePost(api_url, Int32.Parse(L_Params[0].ToString().Trim()), "D");
            response = JsonSerializer.Serialize<List<Output>>(L_output);

            return response;
        }

        //Operation for query a post list
        public List<string> QueryPost(List<string> L_Params)
        {
            string api_url = Configuration["ApiUrl"].Trim();
            List<PostList> l_postlist = new List<PostList>();
            List<Post> l_post = new List<Post>();
            string user_id = string.Empty;
            string user_type = string.Empty;
            List<string> response = new List<string>();
            Post epost = new Post();

            user_id = _sesion.ValueGet("user_id");
            user_type = _sesion.ValueGet("user_type");

            //Query posts for publishing or deletion
            if (L_Params[0].ToString().Trim() == "editor")
            {
                if (user_id == null || user_id.ToString() == "" || user_id.ToString() == "0" || user_type != "E")
                {
                    response.Add("1");
                    response.Add("User has not loged in or User is not an Editor");
                }
                else
                {
                    Task<List<PostList>> task = Task.Run<List<PostList>>(async () => await Operations.GetPostLists(api_url, 0, "N"));
                    l_postlist = task.Result.ToList();

                    if (l_postlist.Count == 0)
                    {
                        response.Add("1");
                        response.Add("There are not post for aproval");
                    }
                    else
                    {
                        response.Add("0");
                        response.Add(JsonSerializer.Serialize<List<PostList>>(l_postlist));
                    }
                }
            }

            //Query a single post
            if (L_Params[0].ToString().Trim() == "unique")
            {
                _sesion.ValueSet("post_id", L_Params[1].ToString().Trim());
                Task<List<Post>> task = Task.Run<List<Post>>(async () => await Operations.GetPost(api_url, Int32.Parse(L_Params[1].ToString().Trim())));
                l_post = task.Result.ToList();

                if (l_post.Count > 0)
                {
                    response.Add("0");
                    response.Add(JsonSerializer.Serialize<List<Post>>(l_post));
                }
                else
                {
                    epost.post_id = 0;
                    epost.post_author_id = 0;
                    epost.post_title = "";
                    epost.post_text = "";
                    epost.post_status = "";
                    epost.post_date = DateTime.Now;
                    epost.post_editor = 0;
                    epost.post_update_date = DateTime.Now;
                    l_post.Add(epost);

                    response.Add("0");
                    response.Add(JsonSerializer.Serialize<List<Post>>(l_post));
                }
            }

            //Query all publihed posts
            if (L_Params[0].ToString().Trim() == "all")
            {
                Task<List<PostList>> task = Task.Run<List<PostList>>(async () => await Operations.GetPostLists(api_url, 0, "A"));
                l_postlist = task.Result.ToList();
                response.Add("0");
                response.Add(JsonSerializer.Serialize<List<PostList>>(l_postlist));
            }

            //Query posts for writer update
            if (L_Params[0].ToString().Trim() == "writer")
            {
                if (user_id == null || user_id.ToString() == "" || user_id.ToString() == "0" || user_type != "W")
                {
                    response.Add("1");
                    response.Add("User has not loged in or User is not an Writer");
                }
                else
                {
                    Task<List<PostList>> task = Task.Run<List<PostList>>(async () => await Operations.GetPostLists(api_url, Int32.Parse(user_id), "*"));
                    l_postlist = task.Result.ToList();

                    if (l_postlist.Count == 0)
                    {
                        response.Add("1");
                        response.Add("There are not post for edition");
                    }
                    else
                    {
                        response.Add("0");
                        response.Add(JsonSerializer.Serialize<List<PostList>>(l_postlist));
                    }
                }
            }

            return response;
        }
    }
}
