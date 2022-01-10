using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WindsorFlightTracker.Models
{

    public class RawData
    {
        public int time { get; set; }
        public object[][] states { get; set; }
    }

    public class Traffic
    {
        public int time { get; set; }
        public List<Flight> Flights { get; set; } = new List<Flight>();
        public string MapAPIKey { get; set; } = Secret.MapBoxAPI;
    }        

    public class Flight
    {
        public string Icao24 { get; set; }
        public string CallSign { get; set; }
        public int Time_Position { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public decimal Baro_Altitude { get; set; }
        public string Baro_Alt { get; set; }
        public bool On_Ground { get; set; }
        public decimal Velocity { get; set; }
        public decimal True_Track { get; set; }
        public string Heading { get; set; }
        public decimal Vertical_Rate { get; set; }
        public string VrString { get; set; }
        public string Squawk { get; set; }
        
    }



}
