using JobPortal.Data;
using JobPortal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.Services
{
    public class JobApplicationService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public JobApplicationService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<JobApplication>> GetJobApplicationsAsync()
        {
            return await _applicationDbContext.JobApplications.Include(p => p.JobListing).ToListAsync();
        }

        public IEnumerable<JobApplication> GetJobApplications()
        {
            return _applicationDbContext.JobApplications.Include(p => p.JobListing);
        }

        public async Task<IEnumerable<JobApplication>> FindJobApplicationsByUserIdAsync(string userid)
        {
            return (from application in await GetJobApplicationsAsync()
                   where application.ApplicantId == userid
                   select application);
        }

        public async Task<JobApplication> GetJobApplicationAsync(string id)
        {
            return await _applicationDbContext.JobApplications.FindAsync(id);
        }

        public async Task CreateJobApplicationAsync(JobApplication jobApplication)
        {
            await _applicationDbContext.JobApplications.AddAsync(jobApplication);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task UpdateJobApplicationAsync(JobApplication jobApplication)
        {
            _applicationDbContext.JobApplications.Update(jobApplication);
            await _applicationDbContext.SaveChangesAsync();
        }

        public bool CheckJobApplication(string userid, string jobid)
        {
            var jobApplications = from application in GetJobApplications()
                                  where application.JobListing.Id == jobid && application.ApplicantId == userid
                                  select application;

            return jobApplications.Any();
        }
    }
}
