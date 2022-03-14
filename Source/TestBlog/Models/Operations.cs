using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft;

namespace TestBlog.Models
{
    //Web api operations 
    public class Operations
    {
        //Query list of posts for edition, publishing and review
        public static async Task<List<PostList>> GetPostLists(string apiurl, Int32 author, string status)
        {
            HttpClient _client = new HttpClient();
            List<PostList> l_postlist = new List<PostList>();

            var uri = new Uri(string.Format(apiurl + "Post/QueryPostList/{0}/{1}", author.ToString(), status));
            var response = await _client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                l_postlist = JsonConvert.DeserializeObject<List<PostList>>(content);
            }

            return l_postlist;
        }

        //Query post detail
        public static async Task<List<Post>> GetPost(string apiurl, Int32 postid)
        {
            HttpClient _client = new HttpClient();
            List<Post> lpost = new List<Post>();

            var uri = new Uri(string.Format(apiurl + "Post/QueryPost/{0}", postid.ToString()));
            var response = await _client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                lpost = JsonConvert.DeserializeObject<List<Post>>(content);
            }

            return lpost;
        }

        //Query post comments
        public static async Task<List<Comment>> GetComments(string apiurl, Int32 postid)
        {
            HttpClient _client = new HttpClient();
            List<Comment> lcomment = new List<Comment>();

            var uri = new Uri(string.Format(apiurl + "Comment/QueryComment/{0}", postid.ToString()));
            var response = await _client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                lcomment = JsonConvert.DeserializeObject<List<Comment>>(content);
            }

            return lcomment;
        }

        //Insert new comment for a post
        public static List<Output> InsertComment(string apiurl, Int32 postid, string comment, string author)
        {
            Comment ncomment = new Comment();
            List<Output> cresponse = new List<Output>();

            ncomment.comment_id = 0;
            ncomment.comment_post_id = postid;
            ncomment.comment_text = comment;
            ncomment.comment_author = author;
            ncomment.comment_date = DateTime.Now;

            var request = (HttpWebRequest)WebRequest.Create(apiurl + "Comment/InsertComment");
            request.ContentType = "application/json";
            request.Method = "POST";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(ncomment);
                streamWriter.Write(json);
            }

            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd().ToString();
                cresponse = JsonConvert.DeserializeObject<List<Output>>(result);
            }

            return cresponse;
        }

        //Insert a new post
        public static List<Output> InsertPost(string apiurl, Int32 author, string title, string stext, string status, Int32 editor)
        {
            Post npost = new Post();
            List<Output> cresponse = new List<Output>();

            npost.post_id = 0;
            npost.post_author_id = author;
            npost.post_title = title;
            npost.post_text = stext;
            npost.post_status = status;
            npost.post_date = DateTime.Now;
            npost.post_editor = 0;
            npost.post_update_date = DateTime.Now;

            var request = (HttpWebRequest)WebRequest.Create(apiurl + "Post/InsertPost");
            request.ContentType = "application/json";
            request.Method = "POST";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(npost);
                streamWriter.Write(json);
            }

            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd().ToString();
                cresponse = JsonConvert.DeserializeObject<List<Output>>(result);
            }

            return cresponse;
        }

        //Publish a post
        public static List<Output> PublishPost(string apiurl, Int32 postid, string status, Int32 editor)
        {
            Post npost = new Post();
            List<Output> cresponse = new List<Output>();

            npost.post_id = postid;
            npost.post_author_id = 0;
            npost.post_title = "";
            npost.post_text = "";
            npost.post_status = status;
            npost.post_date = DateTime.Now;
            npost.post_editor = editor;
            npost.post_update_date = DateTime.Now;

            var request = (HttpWebRequest)WebRequest.Create(apiurl + "Post/PublishPost");
            request.ContentType = "application/json";
            request.Method = "POST";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(npost);
                streamWriter.Write(json);
            }

            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd().ToString();
                cresponse = JsonConvert.DeserializeObject<List<Output>>(result);
            }

            return cresponse;
        }

        //Delete a post
        public static List<Output> DeletePost(string apiurl, Int32 postid, string status)
        {
            Post npost = new Post();
            List<Output> cresponse = new List<Output>();

            npost.post_id = postid;
            npost.post_author_id = 0;
            npost.post_title = "";
            npost.post_text = "";
            npost.post_status = status;
            npost.post_date = DateTime.Now;
            npost.post_editor = 0;
            npost.post_update_date = DateTime.Now;

            var request = (HttpWebRequest)WebRequest.Create(apiurl + "Post/DeletePost");
            request.ContentType = "application/json";
            request.Method = "POST";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(npost);
                streamWriter.Write(json);
            }

            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd().ToString();
                cresponse = JsonConvert.DeserializeObject<List<Output>>(result);
            }

            return cresponse;
        }

        //Edit a post
        public static List<Output> EditPost(string apiurl, Int32 postid, string title, string stext)
        {
            Post npost = new Post();
            List<Output> cresponse = new List<Output>();

            npost.post_id = postid;
            npost.post_author_id = 0;
            npost.post_title = title;
            npost.post_text = stext;
            npost.post_status = "";
            npost.post_date = DateTime.Now;
            npost.post_editor = 0;
            npost.post_update_date = DateTime.Now;

            var request = (HttpWebRequest)WebRequest.Create(apiurl + "Post/EditPost");
            request.ContentType = "application/json";
            request.Method = "POST";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(npost);
                streamWriter.Write(json);
            }

            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd().ToString();
                cresponse = JsonConvert.DeserializeObject<List<Output>>(result);
            }

            return cresponse;
        }

        //Poccess user sign in
        public static List<User> LoginUser(string apiurl, string username, string password)
        {
            User nuser = new User();
            List<User> luser = new List<User>();

            nuser.user_id = 0;
            nuser.user_name = username;
            nuser.user_fullname = "";
            nuser.user_password = password;
            nuser.user_type = "";
            nuser.user_status = "";
            nuser.user_last_update = DateTime.Now;

            var request = (HttpWebRequest)WebRequest.Create(apiurl + "User/LoginUser");
            request.ContentType = "application/json";
            request.Method = "POST";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(nuser);
                streamWriter.Write(json);
            }

            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd().ToString();
                luser = JsonConvert.DeserializeObject<List<User>>(result);
            }

            return luser;
        }
    }
}
