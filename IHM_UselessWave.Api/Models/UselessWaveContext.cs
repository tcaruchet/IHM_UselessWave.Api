using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IHM_UselessWave.Api.Models
{
    public class UselessWaveContext : DbContext
    {
        public UselessWaveContext(DbContextOptions<UselessWaveContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }

    }
}
