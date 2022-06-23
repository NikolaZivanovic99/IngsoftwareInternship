using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MovieLibrary.Data.Models
{
    public partial class MoviesDataBaseContext : DbContext
    {
        public MoviesDataBaseContext()
        {
        }

        public MoviesDataBaseContext(DbContextOptions<MoviesDataBaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Movie> Movies { get; set; } = null!;
        public virtual DbSet<Occupation> Occupations { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.Property(e => e.Caption).HasMaxLength(50);

                entity.Property(e => e.DeleteDate).HasColumnType("datetime");

                entity.Property(e => e.InsertDate).HasColumnType("datetime");

                entity.Property(e => e.SubmittedBy).HasMaxLength(100);
            });

            modelBuilder.Entity<Occupation>(entity =>
            {
                entity.Property(e => e.OccupationId).HasColumnName("OccupationID");

                entity.Property(e => e.Caption).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Address).HasMaxLength(512);

                entity.Property(e => e.DeleteDate).HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Idnumber)
                    .HasMaxLength(50)
                    .HasColumnName("IDNumber");

                entity.Property(e => e.InsertDate).HasColumnType("datetime");

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.OccupationId).HasColumnName("OccupationID");

                entity.HasOne(d => d.Occupation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.OccupationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
