using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestBlog.Models
{
    //Model for the post data structure
    public class Post
    {
        [Key]
        public Int32 post_id { get; set; }
        public Int32 post_author_id { get; set; }
        [StringLength(250)]
        public string post_title { get; set; }
        [StringLength(4000)]
        public string post_text { get; set; }
        [StringLength(1)]
        public string post_status { get; set; }
        public DateTime post_date { get; set; }
        public Int32 post_editor { get; set; }
        public DateTime post_update_date { get; set; }
        public string author_name { get; set; }
    }
}
