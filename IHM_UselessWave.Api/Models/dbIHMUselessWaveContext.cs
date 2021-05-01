using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace IHM_UselessWave.Api
{
    public partial class dbIHMUselessWaveContext : DbContext
    {
        public dbIHMUselessWaveContext()
        {
        }

        public dbIHMUselessWaveContext(DbContextOptions<dbIHMUselessWaveContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Ride> Rides { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Startup.conString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.Uid)
                    .HasName("PK_dbo.Event");

                entity.ToTable("Event");

                entity.Property(e => e.Uid).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Comment).HasMaxLength(200);

                entity.Property(e => e.DateTimeSent).HasColumnType("datetime");

                entity.Property(e => e.Latitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(9, 6)");

                entity.HasOne(d => d.UserU)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.UserUid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_dbo.Event_dbo.User_UserUid");
            });

            modelBuilder.Entity<Ride>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.ToTable("Ride");

                entity.Property(e => e.Depart)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Destination)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EstimatedTime)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LatitudeDepart).HasColumnType("decimal(9, 5)");

                entity.Property(e => e.LatitudeDestination).HasColumnType("decimal(9, 5)");

                entity.Property(e => e.LongitudeDepart).HasColumnType("decimal(9, 5)");

                entity.Property(e => e.LongitudeDestination).HasColumnType("decimal(9, 5)");

                entity.HasOne(d => d.UserU)
                    .WithMany(p => p.Ride)
                    .HasForeignKey(d => d.UserUid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_dbo.Ride_dbo.User_UserUid");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Uid)
                    .HasName("PK_dbo.User");

                entity.ToTable("User");

                entity.Property(e => e.Uid).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UrlAvatar).HasMaxLength(200);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
