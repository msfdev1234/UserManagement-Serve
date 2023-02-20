﻿using Microsoft.EntityFrameworkCore;
using UserManagement_Serv.Models;

namespace UserManagement_Serv.Context
{
    public class AppDbContext: DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");

        }
    }
}
