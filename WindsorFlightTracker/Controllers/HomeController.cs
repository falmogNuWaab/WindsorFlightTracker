using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WindsorFlightTracker.Models;

namespace WindsorFlightTracker.Controllers
{
    public class HomeController : Controller
    {
        OpenSkyDAL osd = new OpenSkyDAL();

        public IActionResult Index()
        {
            Traffic f = osd.CreateFlightStates();
            return View(f);
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
