using ChartJsTryouts.Lib;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ChartJsTryouts.Web.Controllers
{
    public class DeliveryController : Controller
    {
        DeliveryManager _deliveryManager;

        public DeliveryController()
        {
            _deliveryManager = new DeliveryManager();
        }

        public IActionResult OfDay(string day)
        {
            var date = DateTime.Parse(day);

            var deliveries = _deliveryManager.GetDeliveriesOfDay(date);

            return View("Deliveries", deliveries);
        }
    }
}
