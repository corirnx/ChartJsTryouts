using System;

namespace ChartJsTryouts.Lib.Models
{
    public class Delivery
    {
        public int Id { get; set; }
        public DateTime Creation { get; set; }
        public TimeSpan DeliveryDuration { get; set; }
        public string Deliverer { get; set; }

    }
}
