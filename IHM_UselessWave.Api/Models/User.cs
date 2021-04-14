using System;
using System.Collections.Generic;

#nullable disable

namespace IHM_UselessWave.Api
{
    public partial class User
    {
        public Guid Uid { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UrlAvatar { get; set; }
        public int? Points { get; set; }
        public bool Enabled { get; set; }
    }
}
