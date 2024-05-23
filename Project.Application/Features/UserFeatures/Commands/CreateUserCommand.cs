using MediatR;
using Project.Application.Interfaces;
using Project.Application.Response;
using System.Net;

namespace Project.Application.Features.UserFeatures.Commands
{
    public class CreateUserCommand : IRequest<Response<string>>
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmationPassword { get; set; }
        public List<string> Roles { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response<string>>
    {
        private readonly IIdentityService _identityService;

        public CreateUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Response<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();
            var result = await _identityService.CreateUserAsync(request.UserName, request.Password, request.Email, request.FullName, request.Roles);

            if (result.isSucceed)
            {
                response.Success = true;
                response.Status = HttpStatusCode.OK;
                response.Data = result.userId; // Assuming `userId` is of type string
            }
            else
            {
                // Handle the case where user creation fails
                response.Success = false;
                response.Status = HttpStatusCode.BadRequest; // or any other appropriate status code
                response.ErrorMessage = "User creation failed."; // Add an appropriate error message
            }

            return response;
        }
    }
}


