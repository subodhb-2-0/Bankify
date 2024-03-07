using Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Account
{
    public class ChangePasswordDto
    {
        public string userName { get; set; }
        public string currentPassword { get; set; }
        public string newPassword { get; set; }
    }
}
