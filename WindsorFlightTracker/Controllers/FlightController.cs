using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WindsorFlightTracker.Models;

namespace WindsorFlightTracker.Controllers
{
    public class FlightController : Controller
    {
        OpenSkyDAL osd = new OpenSkyDAL();
        public IActionResult Index()
        {
            Traffic f = osd.CreateFlightStates();
            return View(f);
        }
        public Traffic GetFlights()
        {
            Traffic f = osd.CreateFlightStates();
            return f;
        }
    }
}
