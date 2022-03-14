using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestBlog.Interfaces
{
    public interface ISesion
    {
        public void ValueSet(string field, string value);
        public string ValueGet(string field);
    }
}
