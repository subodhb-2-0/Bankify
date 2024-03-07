using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Security
{
    public class JwtSettings
    {
        public bool ValidateIssuerSigningKey
        {
            get;
            set;
        }
        public string? IssuerSigningKey
        {
            get;
            set;
        }
        public bool ValidateIssuer
        {
            get;
            set;
        } = true;
        public string? ValidIssuer
        {
            get;
            set;
        }
        public bool ValidateAudience
        {
            get;
            set;
        } = true;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string ValidAudience
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            get;
            set;
        }
        public bool RequireExpirationTime
        {
            get;
            set;
        }
        public bool ValidateLifetime
        {
            get;
            set;
        } = true;
    }
}
