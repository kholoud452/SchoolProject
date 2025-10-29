using SchoolProject.Data.Commans;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public class StudentSubject : GeneralLocalizableEntities
    {

        public int StudID { get; set; }
        public int SubID { get; set; }
        public decimal? grade { get; set; }

        [ForeignKey("StudID")]
        [InverseProperty(nameof(Student.StudentSubjects))]
        public virtual Student? Student { get; set; }

        [ForeignKey("SubID")]
        [InverseProperty(nameof(Subject.StudentsSubjects))]
        public virtual Subject? Subject { get; set; }

    }

}
