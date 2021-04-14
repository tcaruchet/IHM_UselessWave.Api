using IHM_UselessWave.Api.Helpers;
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
        [Required]
        public Enums.EventType Type { get; set; }
        [Required]
        public GPSPoint Coordinates { get; set; }
        [Required]
        public DateTime DateTimeSent { get; set; }
        public string? Comment { get; set; }
        public Guid? UserUid { get; set; }
        public User? User { get; set; }
    }
}
