using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vehicle_Assembly.Models
{
    [Table("assembles")]
    [PrimaryKey(nameof(vehicle_id), nameof(NIC))]
    public class AssembleModel
    {
        public int vehicle_id { get; set; }
        public int NIC { get; set; }
        public DateOnly date { get; set; }
        public bool isCompleted { get; set; }

        [ForeignKey("vehicle_id")]
        public VehicleModel Vehicle { get; set; }
        [ForeignKey("NIC")]
        public WorkerModel Worker { get; set; }
    }
}
