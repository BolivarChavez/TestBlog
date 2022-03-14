using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestBlog.Interfaces
{
    public interface IComment
    {
        public string InsertComment(List<string> L_Params);
        public string QueryCommentList(List<string> L_Params);
    }
}
