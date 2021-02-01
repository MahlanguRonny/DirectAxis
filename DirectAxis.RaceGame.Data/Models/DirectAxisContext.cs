using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DirectAxis.RaceGame.Data.Models
{
    public partial class DirectAxisContext : DbContext
    {
        public DirectAxisContext()
        {
        }

        public DirectAxisContext(DbContextOptions<DirectAxisContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CarType> CarTypes { get; set; }
        public virtual DbSet<RaceStatistic> RaceStatistics { get; set; }
        public virtual DbSet<Track> Tracks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=NOVA-NB06\\SQLEXPRESS;Database=DirectAxis;User Id=DirectAxis;password=DirectAxis@12;Trusted_Connection=False;MultipleActiveResultSets=true;integrated security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<CarType>(entity =>
            {
                entity.ToTable("CarType");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<RaceStatistic>(entity =>
            {
                entity.Property(e => e.DateRaced).HasColumnType("datetime");
            });

            modelBuilder.Entity<Track>(entity =>
            {
                entity.Property(e => e.Complexity)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
