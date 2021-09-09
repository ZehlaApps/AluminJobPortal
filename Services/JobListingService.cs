using JobPortal.Data;
using JobPortal.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace JobPortal.Services
{
    public class JobListingService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public JobListingService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<JobListing>> GetJobListingsAsync()
        {
            return await _applicationDbContext.JobListings.Include(p => p.JobApplications).ToListAsync();
        }


        public async Task<IEnumerable<JobListing>> GetTopListings(int count)
        {
            return await _applicationDbContext.JobListings
                .Include(p => p.JobApplications)
                .OrderByDescending(p => p.JobApplications.Count)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<JobListing>> GetJobListingsAsync(string employerId)
        {
            return await _applicationDbContext.JobListings.Where(e => e.EmployerId == employerId).Include(p => p.JobApplications).ToListAsync();
        }

        public async Task<IList<JobListing>> SearchJobListingsAsync(string query)
        {
            // Regex regex = new Regex(query);
            
            List<JobListing> jobs = new List<JobListing>();

            if (!String.IsNullOrWhiteSpace(query))
            {
                jobs = (await GetJobListingsAsync()).Where(a =>
                        !String.IsNullOrEmpty(a.JobTitle) && a.JobTitle.ToLower().Contains(query.ToLower()) ||
                        !String.IsNullOrEmpty(a.JobSector) && a.JobSector.ToLower().Contains(query.ToLower()) ||
                        !String.IsNullOrEmpty(a.JobLocation) && a.JobLocation.ToLower().Contains(query.ToLower()) ||
                        !String.IsNullOrEmpty(a.JobDescription) && a.JobDescription.ToLower().Contains(query.ToLower())
                    ).ToList();
            }
 
            return jobs;
        }

        public async Task<JobListing> GetJobListingAsync(string id)
        {
            return await _applicationDbContext.JobListings.FindAsync(id);
        }

        public async Task CreateJobListingAsync(JobListing jobListing)
        {
            await _applicationDbContext.JobListings.AddAsync(jobListing);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task UpdateJobListingAsync(JobListing jobListing)
        {
            _applicationDbContext.JobListings.Update(jobListing);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task DeleteJobListingAsync(string id)
        {
            var jobListing = await GetJobListingAsync(id);
            _applicationDbContext.JobListings.Remove(jobListing);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
