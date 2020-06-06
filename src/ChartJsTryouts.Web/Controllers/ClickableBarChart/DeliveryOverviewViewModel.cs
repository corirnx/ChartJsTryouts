using ChartJsTryouts.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChartJsTryouts.Web.Controllers.ClickableBarChart
{
    public class DeliveryOverviewViewModel
    {
        public OverviewDay[] Days { get; set; }
        public Dictionary<string, DelivererAmountPerDay[]> DelivererAmountPerDay { get; set; }

        public DeliveryOverviewViewModel()
        {
            DelivererAmountPerDay = new Dictionary<string, DelivererAmountPerDay[]>();
        }
    }

    public class DelivererAmountPerDay
    {
        public DateTime Day { get; set; }
        public int Amount { get; set; }
    }

    public class OverviewDay
    {
        public DateTime DeliveryCreation { get; set; }
        public Dictionary<string, Delivery[]> DeliveriesOfDay { get; set; }
        public Delivery[] GetAll
        {
            get
            {
                return DeliveriesOfDay.SelectMany(d => d.Value).ToArray();
            }
        }
    }
}
