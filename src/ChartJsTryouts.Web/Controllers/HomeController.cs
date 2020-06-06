using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ChartJsTryouts.Web.Models;
using ChartJsTryouts.Lib;
using System.Linq;
using System.Collections.Generic;

namespace ChartJsTryouts.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        DeliveryManager _deliveryManager;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

            _deliveryManager = new DeliveryManager();
        }

        public IActionResult Index()
        {
            return View();
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
