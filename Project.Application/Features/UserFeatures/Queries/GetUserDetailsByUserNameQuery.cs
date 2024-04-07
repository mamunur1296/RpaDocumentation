using MediatR;
using Project.Application.DTOs;
using Project.Application.Interfaces;

namespace Project.Application.Features.UserFeatures.Queries
{
    public class GetUserDetailsByUserNameQuery : IRequest<UserDetailsDTO>
    {
        public string UserName { get; set; }
    }

    public class GetUserDetailsByUserNameQueryHandler : IRequestHandler<GetUserDetailsByUserNameQuery, UserDetailsDTO>
    {
        private readonly IIdentityService _identityService;

        public GetUserDetailsByUserNameQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<UserDetailsDTO> Handle(GetUserDetailsByUserNameQuery request, CancellationToken cancellationToken)
        {
            var (userId, fullName, userName, email, roles) = await _identityService.GetUserDetailsByUserNameAsync(request.UserName);
            return new UserDetailsDTO() { Id = userId, FullName = fullName, UserName = userName, Email = email, Roles = roles };
        }
    }
}
