using System.ComponentModel.DataAnnotations;

namespace Vehicle_Assembly.Models
{
    public class Vehicle
    {
        [Key]
        public int vehicle_id { get; set; }
        public string model { get; set; }
        public string color { get; set; }
        public string engine { get; set; }
    }

}
