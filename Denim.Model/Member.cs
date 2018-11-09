using System.ComponentModel.DataAnnotations;

namespace Denim.Model
{
    public class Member
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string OfficeId { get; set; }

        public int? DepartmentId { get; set; }

        public Department Department { get; set; }

        public int? IpNumber { get; set; }

        public bool? IsAllowMotion { get; set; }
        
        public bool? IsAllowTypo { get; set; }

        public bool? IsAllowLogo { get; set; }

        public bool? IsAllowBrand { get; set; }

        public bool? IsAllowVFX { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
