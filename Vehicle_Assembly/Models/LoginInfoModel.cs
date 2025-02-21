using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Vehicle_Assembly.Models
{
    [Table("logininfo")]
    public class LoginInfoModel
    {
        [Key]
        public required string email { get; set; }
        public required string jwt { get; set; }
    }
}
