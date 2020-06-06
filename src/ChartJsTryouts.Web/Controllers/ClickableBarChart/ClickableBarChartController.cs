using ChartJsTryouts.Lib;
using Microsoft.AspNetCore.Mvc;
using System;

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
            // get some statistic data for bar chart example
            var deliveriers = _deliveryManager.GetDeliveriesOfWeek();

            var weekViewModel = new WeekViewViewModel(deliveriers);         

            return View("DeliveryOverview", weekViewModel);
        }


        public IActionResult OfDay(string day)
        {
            var date = DateTime.Parse(day);

            var deliveries = _deliveryManager.GetDeliveriesOfDay(date);

            return View("Deliveries", deliveries);
        }
    }
}
