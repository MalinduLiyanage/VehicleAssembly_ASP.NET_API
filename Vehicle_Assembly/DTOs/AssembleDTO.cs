﻿namespace Vehicle_Assembly.DTOs
{
    public class AssembleDTO
    {
        public int vehicle_id { get; set; }
        public string model { get; set; }
        public string color { get; set; }
        public string engine { get; set; }
        public int NIC { get; set; }
        public string WorkerName { get; set; }
        public string job_role { get; set; }
        public DateOnly date { get; set; }
        public bool isCompleted { get; set; }
    }

}
