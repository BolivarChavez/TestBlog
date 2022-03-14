using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestBlog.Interfaces
{
    public interface IPost
    {
        public string InsertPost(List<string> L_Params);
        public string PublishPost(List<string> L_Params);
        public string DeletePost(List<string> L_Params);
        public List<string> QueryPost(List<string> L_Params);
    }
}
