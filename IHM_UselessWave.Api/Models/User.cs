using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace IHM_UselessWave.Api
{
    public partial class User
    {
        public User()
        {
            Event = new HashSet<Event>();
            Ride = new HashSet<Ride>();
        }

        public Guid Uid { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UrlAvatar { get; set; }
        public int? Points { get; set; }
        public bool Enabled { get; set; }

        [JsonIgnore]
        public virtual ICollection<Event> Event { get; set; }
        [JsonIgnore]
        public virtual ICollection<Ride> Ride { get; set; }
    }
}
