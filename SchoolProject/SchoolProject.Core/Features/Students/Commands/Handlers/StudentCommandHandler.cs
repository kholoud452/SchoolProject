using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.SharedResource;
using SchoolProject.Data.Entities;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Core.Features.Students.Commands.Handlers
{
    public class StudentCommandHandler : ResponseHandler,
        IRequestHandler<AddStudentCommand, Response<string>>,
        IRequestHandler<EditStudentCommand, Response<string>>,
        IRequestHandler<DeleteStudentCommand, Response<string>>,
        IRequestHandler<AddStudentSubjectsCommand, Response<string>>
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public StudentCommandHandler(IStudentService studentService, IMapper mapper,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _studentService = studentService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var studentMapping = _mapper.Map<Student>(request);
            var addedStudent = await _studentService.AddStudent(studentMapping);
            if (addedStudent == "Success") return Created("");
            else return BadRequest<string>();

        }

        public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            var studnet = await _studentService.GetStudentByIDAsync(request.StudID);
            if (studnet == null)
                return NotFound<string>("Student Not Exist");
            var studentMapping = _mapper.Map(request, studnet);
            var updatedStudent = await _studentService.EditAsync(studentMapping);
            if (updatedStudent == "success") return Success($"");
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentByIDAsync(request.StudID);
            if (student == null)
                return NotFound<string>("Student Not Exist");
            var deletedStudent = await _studentService.DeleteAsync(student);
            if (deletedStudent == $"{student.NameEn} deleted")
                return Deleted<string>("");
            else if (deletedStudent == "StudentNotFoundOrDeleted")
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.StudentNotFoundOrDeleted]);
            else return BadRequest<string>();

        }

        public async Task<Response<string>> Handle(AddStudentSubjectsCommand request, CancellationToken cancellationToken)
        {
            var studentSubject = await _studentService.AddStudentSubject(request.StudentId, request.SubjectsId);
            switch (studentSubject)
            {
                case "StudentNotExist": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.StudentNotFoundOrDeleted]);
                case "SubjectNotFound": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.SubjectNotFoundOrDeleted]);
                case "SubjectsAddedSuccessFully": return Success<string>(_stringLocalizer[SharedResourcesKeys.Success]);
                default: return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.TryAgain]);
            }
        }
    }
}
