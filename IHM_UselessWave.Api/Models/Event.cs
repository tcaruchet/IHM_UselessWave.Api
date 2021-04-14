using System;
using System.Collections.Generic;

#nullable disable

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
