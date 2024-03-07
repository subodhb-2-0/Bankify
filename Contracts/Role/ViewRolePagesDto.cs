using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Role
{
    public class ViewRolePagesDto
    {
        public int pageid { get; set; }
        public string pagename { get; set; }
        public int parentid { get; set; }
        public int pagemodeparam { get; set; }
        public int pagesource { get; set; }
        public int rolepageid { get; set; }
        public int roleid { get; set; }
    }
}

