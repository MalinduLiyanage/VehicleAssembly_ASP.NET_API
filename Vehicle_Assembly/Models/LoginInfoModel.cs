using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Vehicle_Assembly.Models
{
    [Table("logininfo")]
    public class LoginInfoModel
    {
        [Key]
        public string? email { get; set; }
        public string? jwt { get; set; }
    }
}
