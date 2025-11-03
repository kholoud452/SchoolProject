using MediatR;
using SchoolProject.Core.Bases;


namespace SchoolProject.Core.Features.SubjectFeatures.Commands.Model
{
    public class AddSubjectCommand : IRequest<Response<string>>
    {
        public string SubjectNameAr { get; set; }
        public string SubjectNameEn { get; set; }
        public int? Period { get; set; }
    }
}
