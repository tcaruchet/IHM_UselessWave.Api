using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace IHM_UselessWave.Api
{
    public partial class Ride
    {
        public int Id { get; set; }
        public string Depart { get; set; }
        public string Destination { get; set; }
        public DateTime Date { get; set; }
        public decimal LongitudeDepart { get; set; }
        public decimal LatitudeDepart { get; set; }
        public decimal LongitudeDestination { get; set; }
        public decimal LatitudeDestination { get; set; }
        public string EstimatedTime { get; set; }
        public double Distance { get; set; }
        public Guid? UserUid { get; set; }

        public virtual User UserU { get; set; }
    }
}
