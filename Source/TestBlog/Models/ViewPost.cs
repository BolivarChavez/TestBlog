using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestBlog.Models
{
    //Model for posts and comments review
    public class ViewPost
    {
        public List<Post> Posts { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
