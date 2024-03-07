using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class RoleFoundException : NotFoundException
    {
        public RoleFoundException(int roleId) : 
            base($"The role with the identifier {roleId} found. Cann't be duplicate")
        {

        }
    }
}