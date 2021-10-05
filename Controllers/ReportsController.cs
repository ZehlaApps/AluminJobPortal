using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using JobPortal.Models;
using JobPortal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace JobPortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportsController : ControllerBase
    {

        private readonly ILogger<ReportsController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<JobProfile> _userManager;
        private readonly JobListingService _jobListingService;
        private readonly JobApplicationService _jobApplicationService;

        public ReportsController(
            ILogger<ReportsController> logger,
            UserManager<JobProfile> userManager,
            RoleManager<IdentityRole> roleManager,
            JobListingService jobListingService,
            JobApplicationService jobApplicationService)
        {
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
            _jobListingService = jobListingService;
            _jobApplicationService = jobApplicationService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("jobs")]
        public async Task<ActionResult> GetJobs()
        {
            var jobs = from job in await _jobListingService.GetJobListingsAsync()
                       select $"{job.Id}," +
                       $"{job.EmployerId}," +
                       $"{job.JobTitle}," +
                       $"{job.JobSector}," +
                       $"{job.ListingDate}," +
                       $"{job.JobLocation},";

            if (jobs == null)
                return NoContent();

            jobs = jobs.Prepend("Id, Employer Id, Job Title, Job Sector, Listing Date, Job Location");
            return Ok(String.Join("\n", jobs));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("applications")]
        public async Task<ActionResult> GetApplications()
        {
            var applications = from application in await _jobApplicationService.GetJobApplicationsAsync()
                               select $"{application.Id}," +
                                      $"{application.ApplicantId}," +
                                      $"{application.ApplicantName}," +
                                      $"{application.ApplicationDate}," +
                                      $"{application.ApplicationApproved}," +
                                      $"{application.ApplicantCollege}," +
                                      $"{application.JobListing.JobTitle}," +
                                      $"{application.JobListing.ListingDate}," +
                                      $"{application.JobListing.EmployerId}";

            if (applications == null)
                return NoContent();

            applications = applications.Prepend("Id, Applicant Id, Name, Date, Approval, College, Job, Listing date, EmployerId");
            return Ok(String.Join("\n", applications));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("applicants")]
        public ActionResult GetApplicants()
        {
            var users = from user in _userManager.Users.Include(x => x.Resume)
                        where user.Role == Roles.Applicant
                        select
                            $"{user.Id}," +
                            $"{user.FullName}," +
                            $"{user.Email}," +
                            $"{user.EmailConfirmed}," +
                            $"{user.PhoneNumber}," +
                            $"{user.PhoneNumberConfirmed}," +
                            // user.Resume.HighSchool,
                            // user.Resume.Intermediate,
                            // user.Resume.College,
                            // user.Resume.GraduationDate,
                            $"{user.Resume.Linkedin}"
                        ;

            if (users == null)
                return NoContent();

            users = users.Prepend("Id, Name, Email, Email Confirmed, Phone Number, Phone Number Confirmed, Linkedin");
            return Ok(String.Join("\n", users));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("employers")]
        public ActionResult GetEmployers()
        {
            var users = from user in _userManager.Users.Include(x => x.Resume)
                        where user.Role == Roles.Employer
                        select
                            $"{user.Id}," +
                            $"{user.FullName}," +
                            $"{user.Email}," +
                            $"{user.EmailConfirmed}," +
                            $"{user.PhoneNumber}," +
                            $"{user.PhoneNumberConfirmed}," +
                            $"{user.Organisation}," +
                            $"{user.OrganisationVerified}," +
                            $"{user.Resume.Linkedin}";

            if (users == null)
                return NoContent();

            users = users.Prepend("Id, Name, Email, Email Confirmed, Phone Number, Phone Number Confirmed, Organisation, Organisation Confirmed, Linkedin");
            return Ok(String.Join("\n", users));
        }
    }
}