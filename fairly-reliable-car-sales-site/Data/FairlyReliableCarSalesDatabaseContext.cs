using System;
using System.Collections.Generic;
using FairlyReliableCarSalesSite.Models;
using Microsoft.EntityFrameworkCore;

namespace FairlyReliableCarSalesSite.Data
{
    public partial class FairlyReliableCarSalesDatabaseContext : DbContext
    {
        public FairlyReliableCarSalesDatabaseContext()
        {
        }

        public FairlyReliableCarSalesDatabaseContext(DbContextOptions<FairlyReliableCarSalesDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Enquiry> Enquiries { get; set; }
        public virtual DbSet<News> NewsItems { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureCarEntity(modelBuilder);
            ConfigureEnquiryEntity(modelBuilder);
            ConfigureNewsEntity(modelBuilder);
            ConfigureEventEntity(modelBuilder);
            ConfigureJobEntity(modelBuilder);

            OnModelCreatingPartial(modelBuilder);
        }

        private static void ConfigureCarEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasKey(e => e.CarId).HasName("PK__Cars__68A0342ED7A057D0");

                entity.Property(e => e.Details).HasMaxLength(250);
                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(255)
                    .HasColumnName("ImageURL");
                entity.Property(e => e.Make).HasMaxLength(50);
                entity.Property(e => e.Model).HasMaxLength(50);
                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            });
        }

        private static void ConfigureEnquiryEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enquiry>(entity =>
            {
                entity.HasKey(e => e.EnquiryId).HasName("PK__Enquiries__0A019B7D652D9B04");

                entity.Property(e => e.CarId).HasColumnName("CarID");
                entity.Property(e => e.Email).HasMaxLength(50);
                entity.Property(e => e.Message).HasMaxLength(250);
                entity.Property(e => e.Name).HasMaxLength(50);
                entity.Property(e => e.PhoneNumber).HasMaxLength(20);

                entity.HasOne(d => d.Car).WithMany(p => p.Enquiries)
                    .HasForeignKey(d => d.CarId)
                    .HasConstraintName("FK__Enquiries__CarID__267ABA7A");
            });
        }

        private static void ConfigureNewsEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<News>(entity =>
            {
                entity.HasKey(n => n.Id);

                entity.Property(n => n.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(n => n.Summary)
                    .HasMaxLength(250);

                entity.Property(n => n.Details)
                    .HasMaxLength(1000);

                entity.Property(n => n.ImageUrl)
                    .HasMaxLength(255)
                    .HasColumnName("ImageURL");
            });
        }

        private static void ConfigureEventEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Date)
                    .IsRequired();

                entity.Property(e => e.Details)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(255)
                    .HasColumnName("ImageURL");
            });
        }

        private static void ConfigureJobEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>(entity =>
            {
                entity.HasKey(j => j.Id);

                entity.Property(j => j.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(j => j.Description)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(j => j.ImageUrl)
                    .HasMaxLength(255)
                    .HasColumnName("ImageURL");
            });
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
