using Microsoft.AspNetCore.Identity;
using System;

namespace JobPortal.Models
{
    public class JobApplication
    {
        public string Id { get; set; }
        public string ApplicantId { get; set; }
        public string ApplicantName { get; set; }
        public string ApplicantCollege { get; set; }
        public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;
        public bool ApplicationApproved { get; set; } = false;

        public string JobListingId { get; set; }
        public JobListing JobListing { get; set; }
    }
}
