﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccessLayer.EFCore
{

    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Address Address { get; set; }

    }

    public class Address
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
    }


    public class ApplicationDbContext : DbContext
    {
        public static readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
            optionsBuilder.EnableSensitiveDataLogging();
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=EFCoreOptimization;Trusted_Connection=True;");
            }
        }

        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        //: base(options)
        //{
        //}


        public virtual DbSet<Posts> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                modelBuilder.Entity<Posts>()
                    .IsMemoryOptimized();

            modelBuilder.Entity<Person>().OwnsOne(typeof(Address), "Address");


            base.OnModelCreating(modelBuilder);
        }

    }
}
