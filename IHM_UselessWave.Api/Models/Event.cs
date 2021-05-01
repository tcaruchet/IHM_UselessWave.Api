using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace IHM_UselessWave.Api
{
    public partial class Event
    {
        public Guid Uid { get; set; }
        public byte Type { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public DateTime DateTimeSent { get; set; }
        public string Comment { get; set; }
        public Guid? UserUid { get; set; }

        public virtual User UserU { get; set; }
    }
}
