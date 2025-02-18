using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vehicle_Assembly.Models
{
    [Table("admin")]
    public class AdminModel
    {
        [Key]
        public int NIC { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
    }
}
