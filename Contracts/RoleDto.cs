using Contracts.Common;
using System.ComponentModel.DataAnnotations;

namespace Contracts
{
    public class RoleDto : BaseEntityDto
    {
        public int RoleId { get; set; }
        
        [Required]
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }

        [Required]
        public int Status { get; set; }

        public int Creator { get; set; }

        public DateTime CreationDate { get; set; }

        public int? Modifier { get; set; }

        public DateTime? ModificationDate { get; set; }

    }
}