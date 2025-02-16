namespace Vehicle_Assembly.Models
{
    public class AssembleRequest
    {
        public int vehicle_id { get; set; }
        public int nic { get; set; }
        public DateOnly date { get; set; }
        public bool isCompleted { get; set; }
    }
}
