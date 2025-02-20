﻿using Vehicle_Assembly.Attributes;
using Vehicle_Assembly.Attributes.ValidationAttributes;

namespace Vehicle_Assembly.DTOs.Requests
{
    public class PutAssembleRequest
    {
        [AssembleRequestValidation]
        public int assignee_id { get; set; }
        public int vehicle_id { get; set; }
        public int nic { get; set; }
        public DateOnly date { get; set; }
        public bool isCompleted { get; set; }
    }
}
