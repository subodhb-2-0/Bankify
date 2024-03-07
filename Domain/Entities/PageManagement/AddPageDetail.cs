using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.PageManagement
{
    public class AddPageDetail
    {
        public string PageName { get; set; }
        public string PagePath { get; set; }
        public string PageCode { get; set; }
        public int? ParentId { get; set; }
        public int Creator { get; set; }
    }
}
