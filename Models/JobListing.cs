using System;
using System.Collections.Generic;

namespace JobPortal.Models
{
    public class JobListing
    {
        public string Id { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string JobLocation { get; set; }
        public string JobSector { get; set; }
        public string EmployerId { get; set; }
        public DateTime ListingDate { get; set; } = DateTime.UtcNow;
        
        public ICollection<JobApplication> JobApplications { get; set; }
    }
}
