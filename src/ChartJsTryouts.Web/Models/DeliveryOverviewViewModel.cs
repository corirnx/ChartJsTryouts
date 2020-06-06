using ChartJsTryouts.Lib.Models;
using System;
using System.Collections.Generic;

namespace ChartJsTryouts.Web.Models
{
    public class DeliveryOverviewViewModel
    {
        public OverviewDay[] Days { get; set; }

        public Dictionary<DateTime, int> PeterSmith { get; set; }
        public Dictionary<DateTime, int> RobertPope { get; set; }
        public Dictionary<DateTime, int> SashaHerrman { get; set; }
        public Dictionary<DateTime, int> MonicaSnyder { get; set; }
        public Dictionary<DateTime, int> BeatrixZimmermann { get; set; }

        public DeliveryOverviewViewModel()
        {
            PeterSmith = new Dictionary<DateTime, int>();
            RobertPope = new Dictionary<DateTime, int>();
            SashaHerrman = new Dictionary<DateTime, int>();
            MonicaSnyder = new Dictionary<DateTime, int>();
            BeatrixZimmermann = new Dictionary<DateTime, int>();
        }
    }

    public class OverviewDay
    {
        public DateTime DeliveryCreation { get; set; }

        public Delivery[] PeterSmith { get; set; }
        public Delivery[] RobertPope { get; set; }
        public Delivery[] SashaHerrman { get; set; }
        public Delivery[] MonicaSnyder { get; set; }
        public Delivery[] BeatrixZimmermann { get; set; }

        public Delivery[] GetAll
        {
            get
            {
                var list = new List<Delivery>();
                list.AddRange(PeterSmith);
                list.AddRange(RobertPope);
                list.AddRange(SashaHerrman);
                list.AddRange(MonicaSnyder);
                list.AddRange(BeatrixZimmermann);

                return list.ToArray();
            }
        }
    }
}
