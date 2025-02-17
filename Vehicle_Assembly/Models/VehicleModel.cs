using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Vehicle_Assembly.Models
{
    [Table("vehicle")]
    public class VehicleModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int vehicle_id { get; set; }
        public string model { get; set; }
        public string color { get; set; }
        public string engine { get; set; }
    }
}
