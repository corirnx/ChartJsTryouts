using ChartJsTryouts.Lib;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChartJsTryouts.Web.Controllers.ClickableBarChart
{
    public class ClickableBarChartController : Controller
    {
        DeliveryManager _deliveryManager;

        public ClickableBarChartController()
        {
            _deliveryManager = new DeliveryManager();
        }

        public IActionResult ClickableBar()
        {
            var deliveriers = _deliveryManager.GetDeliveriesOfWeek();

            var vm = new DeliveryOverviewViewModel();

            var groupedDeliveries = deliveriers.GroupBy(d => d.Creation, (key, g) => new { Creation = key, Deliveries = g.ToList() })
                .ToArray();

            var list = new List<OverviewDay>();
            foreach (var day in groupedDeliveries)
            {
                var ob = new OverviewDay
                {
                    DeliveryCreation = day.Creation,
                    DeliveriesOfDay = new Dictionary<string, Lib.Models.Delivery[]>()
                };

                var deliverersDistinct = day.Deliveries.Select(d => d.Deliverer).Distinct().ToArray();

                foreach (var deliverer in deliverersDistinct)
                {
                    ob.DeliveriesOfDay.Add(deliverer, day.Deliveries.Where(d => d.Deliverer.Equals(deliverer)).ToArray());
                }

                list.Add(ob);
            }

            vm.Days = list.ToArray();

            var deliverers = vm.Days.Select(d => d.DeliveriesOfDay).SelectMany(d => d.Keys).Distinct().ToArray();

            foreach (var deliverer in deliverers)
            {
                var data = vm.Days.SelectMany(t => t.DeliveriesOfDay.Where(dd => dd.Key == deliverer).SelectMany(v => v.Value)).ToArray();
                var groupd = data.GroupBy(d => d.Creation, (key, g) => new { Creation = key, Deliveries = g.ToArray() }).ToArray();

                var list2 = new List<DelivererAmountPerDay>();
                foreach (var g in groupd)
                {
                    var o = new DelivererAmountPerDay
                    {
                        Day = g.Creation,
                        Amount = g.Deliveries.Length
                    };

                    list2.Add(o);
                }

                vm.DelivererAmountPerDay.Add(deliverer, list2.ToArray());
            }

            return View("DeliveryOverview", vm);
        }


        public IActionResult OfDay(string day)
        {
            var date = DateTime.Parse(day);

            var deliveries = _deliveryManager.GetDeliveriesOfDay(date);

            return View("Deliveries", deliveries);
        }
    }
}
