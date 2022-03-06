using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectMVC.Models;

namespace ProjectMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options){
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PlannedTask>()
                    .HasOne(p => p.project)
                    .WithMany(u => u.plannedTasks);
        }


        public DbSet<PlannedTask> PlannedTasks { get; set; }
        public DbSet<Project> Projects { get; set; }
    }

}
