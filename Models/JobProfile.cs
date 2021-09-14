using Microsoft.AspNetCore.Identity;

namespace JobPortal.Models
{

    public enum Roles
    {
        Admin,
        Applicant,
        Employer
    }

    public class JobProfile : IdentityUser
    {
        public Roles Role { get; set; }

        [PersonalData]
        public string FullName { get; set; }
        [PersonalData]
        public string ProfilePicture { get; set; }
        [PersonalData]
        public string Bio { get; set; }
        [PersonalData]
        public string Sector { get; set; }
        [PersonalData]
        public string EmployeesCount { get; set; }
        [PersonalData]
        public string Organisation { get; set; }
        [PersonalData]
        public bool OrganisationVerified { get; set; }
        [PersonalData]
        public JobResume Resume { get; set; }
    }

}
