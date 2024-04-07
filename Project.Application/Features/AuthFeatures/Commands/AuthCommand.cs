using MediatR;
using Project.Application.DTOs;
using Project.Application.Exceptions;
using Project.Application.Interfaces;


namespace Project.Application.Features.AuthFeatures.Commands
{
    public class AuthCommand : IRequest<AuthDTO>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class AuthCommandHandler : IRequestHandler<AuthCommand, AuthDTO>
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IIdentityService _identityService;

        public AuthCommandHandler(IIdentityService identityService, ITokenGenerator tokenGenerator)
        {
            _identityService = identityService;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<AuthDTO> Handle(AuthCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.SigninUserAsync(request.UserName, request.Password);

            if (!result)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var (userId, fullName, userName, email, roles) = await _identityService.GetUserDetailsAsync(await _identityService.GetUserIdAsync(request.UserName));

            string token = _tokenGenerator.GenerateJWTToken((userId, userName, roles));

            return new AuthDTO()
            {
                UserId = userId,
                Name = userName,
                Token = token
            };
        }
    }
}
