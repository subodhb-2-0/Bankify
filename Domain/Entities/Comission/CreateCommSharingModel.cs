using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Comission
{
    public class CreateCommSharingModel
    {
        public string CommSharingModelName { get; set; }
        public int CommSharingModelStatus { get; set; }
        public int UserId { get; set; }
        public DateTime creationdate { get; set; }
    }
}
