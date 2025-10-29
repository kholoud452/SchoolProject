using SchoolProject.Data.Commans;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public class Ins_Subject : GeneralLocalizableEntities
    {
        [Key]
        public int SubId { get; set; }
        [ForeignKey("SubId")]
        [InverseProperty(nameof(Subject.Ins_Subjects))]
        public virtual Subject? Subject { get; set; }


        [Key]
        public int InsId { get; set; }
        [ForeignKey(nameof(InsId))]
        [InverseProperty(nameof(Instructor.Ins_Subjects))]
        public virtual Instructor? Instructor { get; set; }
    }
}
