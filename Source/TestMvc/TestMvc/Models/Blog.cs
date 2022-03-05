using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestMvc.Models
{
    public class Blog
    {
        [Key]
        public Int32 post_id { get; set; }
        public Int32 post_author_id { get; set; }
        public string post_title { get; set; }
        public string post_text { get; set; }
        public string post_status { get; set; }
        public DateTime post_date { get; set; }
        public Int32 post_editor { get; set; }
        public DateTime post_update_date { get; set; }
    }
}
