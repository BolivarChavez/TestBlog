using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestBlog.Models
{
    //Model for the comment data structure
    public class Comment
    {
        [Key]
        public Int32 comment_id { get; set; }
        public Int32 comment_post_id { get; set; }
        [StringLength(4000)]
        public string comment_text { get; set; }
        [StringLength(150)]
        public string comment_author { get; set; }
        public DateTime comment_date { get; set; }
    }
}
