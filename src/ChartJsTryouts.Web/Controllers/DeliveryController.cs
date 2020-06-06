using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ChartJsTryouts.Web.Controllers
{
    public class DeliveryController : Controller
    {
        public IActionResult OfDay(string day)
        {
            return View();
        }
    }
}
