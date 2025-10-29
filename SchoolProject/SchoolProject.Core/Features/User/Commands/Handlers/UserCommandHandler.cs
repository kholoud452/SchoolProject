using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.User.Commands.Models;
using SchoolProject.Core.SharedResource;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Services.Abstract;


namespace SchoolProject.Core.Features.User.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler,
        IRequestHandler<AddUserCommand, Response<string>>,
        IRequestHandler<UpdateUserCommand, Response<string>>,
        IRequestHandler<DeleteUserCommand, Response<string>>,
        IRequestHandler<ChangePasswordCommand, Response<string>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;

        public UserCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor contextAccessor,
            IEmailService emailService,
            IUserService userService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _emailService = emailService;
            _userService = userService;
        }
        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var userMapper = _mapper.Map<ApplicationUser>(request);
            var addedUser = await _userService.AddUserAsync(userMapper, request.Password);
            switch (addedUser)
            {
                case "EmailIsExist":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.EmailIsAlreadyExist]);
                case "UserNameIsExist":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UserNameIsAlreadyExist]);
                case "ErrorInCreate":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FaildToAddUser]);
                case "Failed":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.TryToRegisterAgain]);
                case "Success":
                    return Created("");
                default:
                    return BadRequest<string>(addedUser);
            }

        }

        public async Task<Response<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userById = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == request.Id);
            if (userById == null)
                return NotFound<string>();

            var userMapper = _mapper.Map(request, userById);

            if (await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName && u.Id != request.Id) != null)
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UserNameIsAlreadyExist]);

            var updatedUser = await _userManager.UpdateAsync(userMapper);
            if (updatedUser.Succeeded) return Success((string)_stringLocalizer[SharedResourcesKeys.Updated]);
            else return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FaildToUpdateUser]);
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userFromDB = await _userManager.FindByIdAsync(request.Id.ToString());
            if (userFromDB == null)
                return NotFound<string>();
            var deletedUser = await _userManager.DeleteAsync(userFromDB);
            if (deletedUser.Succeeded)
                return Deleted<string>();
            return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FaildToDeleteUser]);
        }

        public async Task<Response<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var userFromDB = await _userManager.FindByIdAsync(request.Id.ToString());
            if (userFromDB == null)
                return NotFound<string>();

            var changedPasswordUser = await _userManager.ChangePasswordAsync(userFromDB, request.CurrentPassword, request.NewPassword);
            if (changedPasswordUser.Succeeded)
                return Success((string)_stringLocalizer[SharedResourcesKeys.PassChangedSuccessfuly]);
            return BadRequest<string>(_stringLocalizer[changedPasswordUser.Errors.FirstOrDefault().Description]);
        }
    }
}
