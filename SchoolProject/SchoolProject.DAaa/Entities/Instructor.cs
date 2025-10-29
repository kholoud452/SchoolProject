using SchoolProject.Data.Commans;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public class Instructor : GeneralLocalizableEntities
    {

        public Instructor()
        {
            Instructors = new HashSet<Instructor>();
            Ins_Subjects = new HashSet<Ins_Subject>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InsId { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? Address { get; set; }
        public string? Position { get; set; }
        public decimal Salary { get; set; }
        public int? SupervisorId { get; set; }
        [ForeignKey(nameof(SupervisorId))]
        [InverseProperty(nameof(Instructor.Instructors))]
        public virtual Instructor? Supervisor { get; set; }
        [InverseProperty(nameof(Instructor.Supervisor))]
        public virtual ICollection<Instructor> Instructors { get; set; }


        public int? DID { get; set; }
        [ForeignKey("DID")]
        [InverseProperty(nameof(Department.Instructors))]
        public virtual Department? Department { get; set; }
        [InverseProperty(nameof(Department.instructor))]
        public virtual Department? DepartmentManager { get; set; }

        [InverseProperty(nameof(Ins_Subject.Instructor))]
        public virtual ICollection<Ins_Subject> Ins_Subjects { get; set; }


    }
}
