using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.SubjectFeatures.Commands.Model
{
    public class EditSubjectCommand : IRequest<Response<string>>
    {
        public int SubID { get; set; }
        public string SubjectNameAr { get; set; }
        public string SubjectNameEn { get; set; }
        public int Period { get; set; }
    }
}
