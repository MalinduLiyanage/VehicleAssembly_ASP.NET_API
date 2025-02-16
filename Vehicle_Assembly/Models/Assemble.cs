using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vehicle_Assembly.Models
{
    [PrimaryKey(nameof(vehicle_id), nameof(NIC))]
    public class Assemble
    {
        public int vehicle_id { get; set; }
        public int NIC { get; set; }
        public DateTime date { get; set; }
        public bool isCompleted { get; set; }

        [ForeignKey("vehicle_id")]
        public Vehicle Vehicle { get; set; }
        [ForeignKey("NIC")]
        public Worker Worker { get; set; }
    }

}
