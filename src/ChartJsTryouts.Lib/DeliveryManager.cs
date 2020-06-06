using ChartJsTryouts.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChartJsTryouts.Lib
{
    public class DeliveryManager
    {
        string[] _deliverer;
        Random _random;

        public DeliveryManager()
        {
            _deliverer = new[] { "Peter Smith", "Robert Pope", "Sasha Herrman", "Monica Snyder", "Beatrix Zimmermanm" };
            _random = new Random();
        }

        public Delivery[] GetDeliveriesOfWeek()
        {
            var totalDeliveries = new List<Delivery>();

            for (var d = 0; d < 7; d++)
            {
                var date = DateTime.Now.AddDays(-d);

                var randomAmount = _random.Next(4, 30);

                for (var r = 0; r < randomAmount; r++)
                    totalDeliveries.Add(new Delivery
                    {
                        Id = r,
                        Creation = date,
                        Deliverer = GetNextDeliverer(),
                        DeliveryDuration = GetNextDeliveryDuration()
                    });
            }

            return totalDeliveries.ToArray();
        }

        public Delivery[] GetDeliveriesOfDay(DateTime day)
        {
            var deliveries = GetDeliveriesOfWeek();

            return deliveries.Where(d => d.Creation.Date == day).ToArray();
        }

        string GetNextDeliverer()
        {
            var r = _random.Next(0, (_deliverer.Length - 1));
            return _deliverer[r];
        }

        TimeSpan GetNextDeliveryDuration()
        {
            return new TimeSpan(NextRandom(23), NextRandom(59), NextRandom(59));
        }

        int NextRandom(int max)
        {
            return _random.Next(0, max);
        }
    }
}
