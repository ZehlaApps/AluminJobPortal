using JobPortal.Models;

using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Data
{
    public class ApplicationDbContext : IdentityDbContext<JobProfile>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {}

        public DbSet<JobListing> JobListings { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<JobListing>()
                        .HasMany(a => a.JobApplications)
                        .WithOne(b => b.JobListing)
                        .HasForeignKey(c => c.JobListingId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<JobProfile>()
                       .HasOne(c => c.Resume)
                       .WithOne(e => e.Profile)
                       .HasForeignKey<JobResume>(b => b.UserId)
                       .IsRequired();
        }
    }
}
