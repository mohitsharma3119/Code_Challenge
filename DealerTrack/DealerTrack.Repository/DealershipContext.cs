﻿using System;
using System.IO;
using DealerTrack.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace DealerTrack.Model
{
    public partial class DealershipContext : DbContext
    {
        public virtual DbSet<Dealerships> Dealerships { get; set; }
        public DealershipContext(DbContextOptions<DealershipContext> options)
            : base(options)
        {
        }

        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DealershipContext>
        {
            public DealershipContext CreateDbContext(string[] args)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(@Directory.GetCurrentDirectory() + "/../DealerTrack/appsettings.json")
                    .Build();
                var builder = new DbContextOptionsBuilder<DealershipContext>();
                var connectionString = configuration.GetConnectionString("DatabaseConnection");
                builder.UseSqlServer(connectionString);
                return new DealershipContext(builder.Options);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Dealerships>(entity =>
            {
                entity.Property(e => e.DealNumber)
                    .HasColumnName("dealnumber");

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasColumnName("customername")
                    .HasColumnType("nvarchar(4000)");

                entity.Property(e => e.DealershipName)
                    .IsRequired()
                    .HasColumnName("dealershipName")
                    .HasMaxLength(100);

                entity.Property(e => e.Vehicle)
                    .IsRequired()
                    .HasColumnName("vehicle")
                    .HasMaxLength(100);

                entity.Property(e => e.Price)
                    .IsRequired()
                    .HasColumnName("price")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");
            });

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
