using System.ComponentModel.DataAnnotations;

namespace Contracts.Role
{
    public class EditRolePageDto
    {
        public int Creator { get; set; }
        public int Modifier { get; set; }
        public int RoleId { get; set; }
        [Required]
        public List<RolePagesedit> RolePages { get; set; }
    }
    public class RolePagesedit
    {
        public int PageId { get; set; }
        //public int Status { get; set; }
        public int Mode { get; set; }
        public int PageStatus { get; set; }
    }
}
