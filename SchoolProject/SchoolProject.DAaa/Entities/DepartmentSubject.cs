using SchoolProject.Data.Commans;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public class DepartmentSubject : GeneralLocalizableEntities
    {

        public int DID { get; set; }
        public int SubID { get; set; }

        [ForeignKey("DID")]
        [InverseProperty(nameof(Department.DepartmentSubjects))]
        public virtual Department? Department { get; set; }

        [ForeignKey("SubID")]
        [InverseProperty(nameof(Subject.DepartmetsSubjects))]
        public virtual Subject? Subject { get; set; }
    }
}
