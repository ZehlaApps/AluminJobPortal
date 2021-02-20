using Microsoft.AspNetCore.Identity;

namespace JobPortal.Models
{

    public enum Role
    {
        Applicant,
        Employer
    }

    public class JobProfile : IdentityUser
    {
        [PersonalData]
        public string FullName { get; set; }
        [PersonalData]
        public string ProfilePicture { get; set; }
        [PersonalData]
        public string Organisation { get; set; }
        [PersonalData]
        public bool OrganisationVerified { get; set; }
        [PersonalData]
        public JobResume Resume { get; set; }
    }

}
