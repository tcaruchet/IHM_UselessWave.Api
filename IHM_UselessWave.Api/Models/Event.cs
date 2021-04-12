﻿using IHM_UselessWave.Api.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IHM_UselessWave.Api.Models
{
    public class Event
    {
        [Key]
        public Guid Uid { get; set; }
        public Enums.EventType Type { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public DateTime DateTimeSent { get; set; }
        public User User { get; set; }
        public string Comment { get; set; }
    }
}
