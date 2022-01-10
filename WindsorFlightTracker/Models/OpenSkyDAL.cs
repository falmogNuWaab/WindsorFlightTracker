using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WindsorFlightTracker.Models
{
    public class OpenSkyDAL
    {
        public RawData GetAllFlightsAboveWindsor()
        {
            string url = "https://opensky-network.org/api/states/all?lamin=41.9967&lomin=-84.0014&lomax=-82.2024&lamax=42.7979";
            HttpWebRequest request = WebRequest.CreateHttp(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader rd = new StreamReader(response.GetResponseStream());
            string result = rd.ReadToEnd();
            rd.Close();

            RawData f = JsonConvert.DeserializeObject<RawData>(result);
            return f;
        }
        public Traffic CreateFlightStates()
        {
            RawData f = GetAllFlightsAboveWindsor();
            Traffic toReturn = new Traffic();
            toReturn.time = f.time;
            for(int i = 0; i < f.states.GetLength(0); i++)
            {
                Flight toAdd = new Flight();
                toAdd.Icao24 = (string)f.states[i][0];
                toAdd.CallSign = (string)f.states[i][1];
                toAdd.Time_Position = int.Parse(f.states[i][3].ToString());
                toAdd.Longitude = float.Parse(f.states[i][5].ToString());
                toAdd.Latitude = float.Parse(f.states[i][6].ToString());
                toAdd.Baro_Altitude = decimal.Parse(f.states[i][7].ToString()) * 3.28084m;
                toAdd.Baro_Altitude = Math.Round(toAdd.Baro_Altitude, 0);
                if(toAdd.Baro_Altitude >= 18000)
                {
                    string baro = toAdd.Baro_Altitude.ToString();
                    toAdd.Baro_Alt = "FL" + "" + baro.Substring(0, 3);
                } else if (toAdd.Baro_Altitude < 18000)
                {
                    string[] baro = toAdd.Baro_Altitude.ToString().Split(".");
                    toAdd.Baro_Alt = $"{baro[0]}ft";
                }
                toAdd.On_Ground = (bool)f.states[i][8];
                toAdd.Velocity = decimal.Parse(f.states[i][9].ToString()) * 1.944m;
                toAdd.Velocity = Math.Round(toAdd.Velocity, 0);
                toAdd.True_Track = decimal.Parse(f.states[i][10].ToString());
                toAdd.True_Track = Math.Round(toAdd.True_Track, 0);
                if (toAdd.True_Track < 100)
                {
                    toAdd.Heading = $"0{toAdd.True_Track}";
                }
                else
                {
                    toAdd.Heading = $"{toAdd.True_Track}";
                }
                toAdd.Vertical_Rate = decimal.Parse(f.states[i][11].ToString()) * 197;
                toAdd.Vertical_Rate = Math.Round(toAdd.Vertical_Rate,0);
                if (toAdd.Vertical_Rate > 0)
                {
                    //char c = (char)38;
                    toAdd.VrString = $" &uarr; {Math.Abs(toAdd.Vertical_Rate)}";
                } else if (toAdd.Vertical_Rate < 0)
                {
                    //char c = (char)40;
                    toAdd.VrString = $" &darr; {Math.Abs(toAdd.Vertical_Rate)}";
                }
                else
                {
                    toAdd.VrString = toAdd.Vertical_Rate.ToString();
                }
                toAdd.Squawk = (string)f.states[i][14];
                toReturn.Flights.Add(toAdd);
            }
            return toReturn;
        }        
    }
}
