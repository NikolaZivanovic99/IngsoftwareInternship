using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MovieLibrary.Data.Models
{
    public partial class MoviesDataBaseContext : IdentityDbContext<ApplicationUser>
    {
        public MoviesDataBaseContext()
        {
        }

        public MoviesDataBaseContext(DbContextOptions<MoviesDataBaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Director> Directors { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<Movie> Movies { get; set; } = null!;
        public virtual DbSet<Occupation> Occupations { get; set; } = null!;
        public virtual DbSet<Rate> Rate { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Director>(entity =>
            {
                entity.ToTable("Director");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.DeleteDate).HasColumnType("date");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.ImagePath)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.InsertDate).HasColumnType("date");

                entity.Property(e => e.LastName).HasMaxLength(50);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.Caption).HasMaxLength(50);

                entity.Property(e => e.DeleteDate).HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.InsertDate).HasColumnType("date");
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.Property(e => e.Caption).HasMaxLength(50);

                entity.Property(e => e.DeleteDate).HasColumnType("datetime");
                entity.Property(e => e.AvgRate).HasPrecision(18, 2);

                entity.Property(e => e.ImagePath)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.InsertDate).HasColumnType("datetime");

                entity.Property(e => e.SubmittedBy).HasMaxLength(100);

                entity.HasMany(d => d.Directors)
                    .WithMany(p => p.Movies)
                    .UsingEntity<Dictionary<string, object>>(
                        "MovieDirector",
                        l => l.HasOne<Director>().WithMany().HasForeignKey("DirectorId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_MovieDirector_Director"),
                        r => r.HasOne<Movie>().WithMany().HasForeignKey("MovieId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_MovieDirector_Movies"),
                        j =>
                        {
                            j.HasKey("MovieId", "DirectorId").HasName("PK_MovieDirector_1");

                            j.ToTable("MovieDirector");
                        });

                entity.HasMany(d => d.Genres)
                    .WithMany(p => p.Movies)
                    .UsingEntity<Dictionary<string, object>>(
                        "MovieGenre",
                        l => l.HasOne<Genre>().WithMany().HasForeignKey("GenreId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_MovieGenre_Genres"),
                        r => r.HasOne<Movie>().WithMany().HasForeignKey("MovieId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_MovieGenre_Movies"),
                        j =>
                        {
                            j.HasKey("MovieId", "GenreId");

                            j.ToTable("MovieGenre");
                        });
                entity.HasMany(d => d.Users)
                    .WithMany(p => p.Movies)
                    .UsingEntity<Dictionary<string, object>>(
                        "MovieUser",
                        l => l.HasOne<ApplicationUser>().WithMany().HasForeignKey("Id").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_MovieUsers_Users"),
                        r => r.HasOne<Movie>().WithMany().HasForeignKey("MovieId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_MovieUsers_Movies"),
                        j =>
                        {
                            j.HasKey("MovieId", "Id");

                            j.ToTable("MovieUser");
                        });
            });

            modelBuilder.Entity<Occupation>(entity =>
            {
                entity.Property(e => e.OccupationId).HasColumnName("OccupationID");

                entity.Property(e => e.Caption).HasMaxLength(50);
            });
            modelBuilder.Entity<ApplicationUser>(entity=>
            {
                entity.Property(e => e.FirstName).HasMaxLength(50).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Address).HasMaxLength(512).IsRequired();
                entity.Property(e=>e.IdNumber).HasMaxLength(50).IsRequired();
                entity.Property(e => e.InsertDate).HasColumnType("datetime").IsRequired();
                entity.Property(e => e.OccupationId).HasColumnName("OccupationId");

                entity.HasOne(d => d.Occupation)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.OccupationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Users");
            });

            modelBuilder.Entity<Rate>(entity =>
            {
                entity.HasKey(e => e.RateId);
                entity.Property(e => e.Comment).HasMaxLength(300);
                entity.Property(e => e.Rates);
                entity.Property(e => e.UserId).HasColumnName("UserId");
                entity.Property(e => e.InsertDate).HasColumnType("datetime").IsRequired();

                entity.HasOne(d => d.User)
                .WithMany(p => p.Rates)
                .HasForeignKey(d=>d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Rate");

                entity.HasOne(d => d.Movie)
                       .WithMany(p => p.Rates)
                       .HasForeignKey(d => d.MovieId)
                       .OnDelete(DeleteBehavior.ClientSetNull)
                       .HasConstraintName("FK_Movie_Rate"); 

            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
