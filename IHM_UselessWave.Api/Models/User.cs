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
        public Guid UserUid { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string UrlAvatar { get; set; }
        public int Points { get; set; }
        [Required]
        public bool Enabled { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}