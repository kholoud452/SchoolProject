using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.DepartmentFeature.Commands.Models;
using SchoolProject.Core.SharedResource;
using SchoolProject.Data.Entities;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Core.Features.DepartmentFeature.Commands.Handlers
{
    public class DepartmentCommandHandler : ResponseHandler,
        IRequestHandler<AddDepartmentCommand, Response<string>>,
        IRequestHandler<EditDepartmentCommand, Response<string>>,
        IRequestHandler<DeleteDepartmentCommand, Response<string>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly IDepartmentService _departmentService;

        public DepartmentCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
            IMapper mapper,
            IDepartmentService departmentService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _departmentService = departmentService;
        }

        public async Task<Response<string>> Handle(AddDepartmentCommand request, CancellationToken cancellationToken)
        {
            var departmentMapper = _mapper.Map<Department>(request);
            var addedDepartment = await _departmentService.AddDepartment(departmentMapper);
            if (addedDepartment != null)
                return Created("");
            return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.TryAgain]);
        }

        public async Task<Response<string>> Handle(EditDepartmentCommand request, CancellationToken cancellationToken)
        {
            var departmentFromDB = await _departmentService.GetDepartmentByIdAsync(request.DID);
            if (departmentFromDB == null)
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.DepartmentIsNotExist]);
            var departmentMapper = _mapper.Map(request, departmentFromDB);
            var updatedDepartment = await _departmentService.EditAsync(departmentMapper);
            if (updatedDepartment == "Success") return Success("");
            return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.TryAgain]);
        }

        public async Task<Response<string>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var departmentFromDB = await _departmentService.SoftDeleteAsync(request.Id);
            switch (departmentFromDB)
            {
                case "DepartmentNotFoundOrDeleted":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.DepartmentNotFoundOrDeleted]);
                case "Failed":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.TryAgain]);
                case "Department deleted successfully":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.Deleted]);
                default:
                    return BadRequest<string>(departmentFromDB);
            }
        }
    }
}
