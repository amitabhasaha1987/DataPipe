using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SolsticeDataServices.DAL.Entities
{
    // The raw structure of "User" table with the column name and the related mapper class is "UserMapper.cs"
    public class Users
    {
        [Key]
        public Int64 UserId { get; set; }
        [Required(ErrorMessage = "This field is mandatory")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "This field is mandatory")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "This field is mandatory")]
        public string Company { get; set; }
        [Required(ErrorMessage = "This field is mandatory")]
        public string Contact { get; set; }
        [Required(ErrorMessage = "This field is mandatory")]
        public string EmailId { get; set; }
        public string Password { get; set; }
        public Guid LinkedAccessKey { get; set; }
        [Key]
        public Guid LastUsedLinkedAccessKey { get; set; }
        public bool IsBlocked { get; set; }

        [DefaultValue(false)]
        public bool IsSuperAdmin { get; set; }
        public bool IsVerified { get; set; }
        [DefaultValue(true)]
        public DateTime? AddTime { get; set; }
        public virtual ICollection<Device> LogicalDevices { get; set; }
    }
}
