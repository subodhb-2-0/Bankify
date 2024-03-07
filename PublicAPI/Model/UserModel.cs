using System.ComponentModel.DataAnnotations;

namespace PublicAPI.Model
{

    public class UserModel
    {
        public UserModel()
        {
            MiddleName = String.Empty;
        }

        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string LoginId { get; set; }

        [Required]
        [EmailAddress]
        public string EmailId { get; set; }
        [Required]
        [RegularExpression("^[0-9]{10}$")]
        public string MobileNumber { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int DepartmentId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int DesignationId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int ReportsTo { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int RoleId { get; set; }
        public int CreatedBy { get; set; }

    }
}
