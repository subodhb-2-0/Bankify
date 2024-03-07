using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Role
{
    public class PageDetailsByRoleDto
    {
        public int parentid { get; set; }
        public int pageid { get; set; }
        public string pagename { get; set; }
        public string pagepath { get; set; }
        public int pagesourse { get; set; }
        public string rolename { get; set; }
        public int pagemodeid { get; set; }
    }
}
