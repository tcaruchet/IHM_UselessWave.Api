using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IHM_UselessWave.Api.Models
{
    public class User
    {
        [Key]
        public Guid Uid { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UrlAvatar { get; set; }
        public bool Enabled { get; set; }
    }
}