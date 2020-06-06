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

            var groupedDeliveries = deliveriers.GroupBy(d => d.Creation, (key, g) => new { Creation = key, Deliveries = g.ToList() });

            var list = new List<OverviewDay>();
            foreach (var day in groupedDeliveries)
            {
                var ob = new OverviewDay
                {
                    PeterSmith = day.Deliveries.Where(a => a.Deliverer.Contains("smith", System.StringComparison.InvariantCultureIgnoreCase)).ToArray(),
                    SashaHerrman = day.Deliveries.Where(a => a.Deliverer.Contains("herrman", System.StringComparison.InvariantCultureIgnoreCase)).ToArray(),
                    MonicaSnyder = day.Deliveries.Where(a => a.Deliverer.Contains("snyder", System.StringComparison.InvariantCultureIgnoreCase)).ToArray(),
                    RobertPope = day.Deliveries.Where(a => a.Deliverer.Contains("pope", System.StringComparison.InvariantCultureIgnoreCase)).ToArray(),
                    BeatrixZimmermann = day.Deliveries.Where(a => a.Deliverer.Contains("zimmermann", System.StringComparison.InvariantCultureIgnoreCase)).ToArray(),
                    DeliveryCreation = day.Creation
                };

                list.Add(ob);
            }

            vm.Days = list.ToArray();

            var allDays = vm.Days.Select(t => t.DeliveryCreation).Distinct().ToArray();

            foreach (var currentDay in allDays)
            {
                var day = vm.Days.Where(t => t.DeliveryCreation == currentDay).FirstOrDefault();

                if (day == null)
                {
                    vm.PeterSmith.Add(currentDay, 0);
                    vm.SashaHerrman.Add(currentDay, 0);
                    vm.MonicaSnyder.Add(currentDay, 0);
                    vm.RobertPope.Add(currentDay, 0);
                    vm.BeatrixZimmermann.Add(currentDay, 0);
                }
                else
                {
                    vm.PeterSmith.Add(currentDay, day.PeterSmith.Length);
                    vm.SashaHerrman.Add(currentDay, day.SashaHerrman.Length);
                    vm.MonicaSnyder.Add(currentDay, day.MonicaSnyder.Length);
                    vm.RobertPope.Add(currentDay, day.RobertPope.Length);
                    vm.BeatrixZimmermann.Add(currentDay, day.BeatrixZimmermann.Length);
                }
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
