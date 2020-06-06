using ChartJsTryouts.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChartJsTryouts.Web.Controllers.ClickableBarChart
{
    public class WeekViewViewModel
    {
        public Dictionary<string, DeliveriesCountOfDay[]> DelivererWeekStatistic { get; set; }
        public DateTime[] Days { get; set; }

        public WeekViewViewModel(Delivery[] deliveriers)
        {
            DelivererWeekStatistic = new Dictionary<string, DeliveriesCountOfDay[]>();

            // group deliveries per day
            var deliveriesPerDay = deliveriers
                .GroupBy(d => d.Creation,
                    (key, g) => new DayGroup { Day = key, Deliveries = g.ToArray() })
                .ToArray();

            var allDeliveriesPerDays = PrepareDeliveriesPerDayandDeliverer(deliveriesPerDay);

            // add supported days
            Days = allDeliveriesPerDays.Select(i => i.DeliveryCreation).Distinct().ToArray();

            MapDataToDisplay(allDeliveriesPerDays);
        }

        DeliveriesOfDay[] PrepareDeliveriesPerDayandDeliverer(DayGroup[] deliveriesPerDay)
        {
            var deliveriesPerDayList = new List<DeliveriesOfDay>();

            foreach (var dayDeliveries in deliveriesPerDay)
                deliveriesPerDayList.Add(FillForDay(dayDeliveries.Day, dayDeliveries.Deliveries));

            return deliveriesPerDayList.ToArray();
        }

        DeliveriesOfDay FillForDay(DateTime day, Delivery[] deliveries)
        {
            var deliveriesForDay = new DeliveriesOfDay(day);

            var deliverers = deliveries.Select(d => d.Deliverer).Distinct().ToArray();

            foreach (var deliverer in deliverers)
                FillForDay(deliveriesForDay, deliverer, deliveries.Where(d => d.Deliverer.Equals(deliverer)).ToArray());

            return deliveriesForDay;
        }


        void FillForDay(DeliveriesOfDay deliveriesForDay, string deliverer, Delivery[] deliveries)
        {
            deliveriesForDay.DeliveriesPerDeliverer.Add(deliverer, deliveries);
        }

        void MapDataToDisplay(DeliveriesOfDay[] allDeliveriesPerDays)
        {
            var deliverers = allDeliveriesPerDays
                .Select(d => d.DeliveriesPerDeliverer)
                .SelectMany(d => d.Keys)
                .Distinct()
                .ToArray();

            foreach (var deliverer in deliverers)
            {
                var deliveriesOfDeliverer = allDeliveriesPerDays
                    .SelectMany(t => t.DeliveriesPerDeliverer.Where(dd => dd.Key == deliverer).SelectMany(v => v.Value))
                    .ToArray();

                var groupedDeliveries = deliveriesOfDeliverer
                    .GroupBy(d => d.Creation, (key, g) => new { Creation = key, Deliveries = g.ToArray() })
                    .ToArray();

                DelivererWeekStatistic.Add(deliverer,
                    groupedDeliveries.Select(g => new DeliveriesCountOfDay { Day = g.Creation, Amount = g.Deliveries.Length }).ToArray());
            }
        }
    }

    public class DeliveriesCountOfDay
    {
        public DateTime Day { get; set; }
        public int Amount { get; set; }
    }

    public class DeliveriesOfDay
    {
        public DateTime DeliveryCreation { get; set; }
        public Dictionary<string, Delivery[]> DeliveriesPerDeliverer { get; set; }
        public DeliveriesOfDay(DateTime creation)
        {
            DeliveryCreation = creation;
            DeliveriesPerDeliverer = new Dictionary<string, Delivery[]>();
        }
    }

    class DayGroup
    {
        public DateTime Day { get; set; }
        public Delivery[] Deliveries { get; set; }
    }
}
