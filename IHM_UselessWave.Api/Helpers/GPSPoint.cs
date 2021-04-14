using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IHM_UselessWave.Api.Helpers
{
    public struct GPSPoint
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public GPSPoint(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }
    }
}
