using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vehicle_Assembly.Models
{
    public class Vehicle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int vehicle_id { get; set; }
        public string model { get; set; }
        public string color { get; set; }
        public string engine { get; set; }
    }

}
