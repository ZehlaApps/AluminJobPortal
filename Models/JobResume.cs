using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.Models
{
    public class Experience
    {
        public string Id { get; set; }
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }

    public enum AcademicDetailType
    {
        COLLEGE,
        INTERMEDIATE,
        HIGHSCHOOL
    }

    public enum AcademicDetailMarkType
    {
        CGPA,
        PERCENTAGE
    }

    public class AcademicDetail
    {
        public string Id { get; set; }
        public AcademicDetailType Type { get; set; }
        public string Institute { get; set; }
        public string University { get; set; }
        public string Specialization { get; set; }
        public float Marks { get; set; }
        public AcademicDetailMarkType MarksType { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }

    public class Project
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Contribution { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }

    public class Skill
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class JobResume
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public JobProfile Profile { get; set; }

        [DataType(DataType.Url)]
        public string Linkedin { get; set; }

        public ICollection<Skill> Skills { get; set; }
        public ICollection<AcademicDetail> AcademicDetails { get; set; }
        public ICollection<Experience> Expereinces { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}
