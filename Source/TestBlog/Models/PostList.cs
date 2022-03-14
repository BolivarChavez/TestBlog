using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestBlog.Models
{
    //Model for the post list data structure
    public class PostList
    {
        [Key]
        public Int32 post_id { get; set; }
        public string post_title { get; set; }
        public string post_status { get; set; }
        public string user_fullname { get; set; }
        public DateTime post_date { get; set; }
        public DateTime post_update_date { get; set; }
    }
}
