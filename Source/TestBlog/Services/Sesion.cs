using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TestBlog.Interfaces;

namespace TestBlog.Services
{
    //Service layer for http session use in the app
    public class Sesion : ISesion
    {
        private readonly IHttpContextAccessor _context;

        public Sesion(IHttpContextAccessor context)
        {
            _context = context;
        }

        public void ValueSet(string field, string value)
        {
            _context.HttpContext.Session.SetString(field, value);
        }

        public string ValueGet(string field)
        {
            return _context.HttpContext.Session.GetString(field);
        }
    }
}
