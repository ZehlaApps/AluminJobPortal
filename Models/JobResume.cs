using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.Models
{
    public class JobExperience
    {
        public string Id { get; set; }
        public string JobName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class JobResume
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public JobProfile Profile { get; set; }

        public string HighSchool { get; set; }
        public string Intermediate { get; set; }
        public string College { get; set; }

        [DataType(DataType.Url)]
        public string Linkedin { get; set; }
        
        public DateTime GraduationDate { get; set; }
        public ICollection<JobExperience> JobExpereinces { get; set; }
    }
}
