using JobPortal.Data;
using JobPortal.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<JobListing>> SearchJobListingsAsync(string query)
        {
            return from job in await GetJobListingsAsync()
                   where job.JobTitle.ToLower().Contains(query)
                         || job.JobLocation.ToLower().Contains(query)
                         || job.JobSector.ToLower().Contains(query)
                         || job.JobDescription.ToLower().Contains(query)
                   select job;
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
