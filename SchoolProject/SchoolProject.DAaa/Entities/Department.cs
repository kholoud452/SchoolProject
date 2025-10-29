using SchoolProject.Data.Commans;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public partial class Department : GeneralLocalizableEntities
    {
        public Department()
        {
            Students = new HashSet<Student>();
            DepartmentSubjects = new HashSet<DepartmentSubject>();
            Instructors = new HashSet<Instructor>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DID { get; set; }
        [StringLength(500)]
        public string? DNameAr { get; set; }
        [StringLength(500)]
        public string? DNameEn { get; set; }
        public int? InsManager { get; set; }
        [ForeignKey(nameof(InsManager))]
        [InverseProperty(nameof(Instructor.DepartmentManager))]
        public virtual Instructor? instructor { get; set; }
        [InverseProperty(nameof(Student.Department))]
        public virtual ICollection<Student> Students { get; set; }


        [InverseProperty("Department")]
        public virtual ICollection<Instructor> Instructors { get; set; }


        [InverseProperty(nameof(DepartmentSubject.Department))]
        public virtual ICollection<DepartmentSubject> DepartmentSubjects { get; set; }
    }
}
