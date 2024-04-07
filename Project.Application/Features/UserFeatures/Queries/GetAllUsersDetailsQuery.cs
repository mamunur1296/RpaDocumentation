using MediatR;
using Project.Application.DTOs;
using Project.Application.Interfaces;

namespace Project.Application.Features.UserFeatures.Queries
{
    public class GetAllUsersDetailsQuery : IRequest<List<UserDetailsDTO>>
    {
        //public string UserId { get; set; }
    }

    public class GetAllUsersDetailsQueryHandler : IRequestHandler<GetAllUsersDetailsQuery, List<UserDetailsDTO>>
    {
        private readonly IIdentityService _identityService;

        public GetAllUsersDetailsQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<List<UserDetailsDTO>> Handle(GetAllUsersDetailsQuery request, CancellationToken cancellationToken)
        {
            var users = await _identityService.GetAllUsersAsync();
            var userDetails = users.Select(x => new UserDetailsDTO()
            {
                Id = x.id,
                Email = x.email,
                UserName = x.userName
                //Roles = (IList<string>)_identityService.GetUserRolesAsync(x.id) // Converstion problem
            }).ToList();

            foreach (var user in userDetails)
            {
                user.Roles = await _identityService.GetUserRolesAsync(user.Id);
            }
            return userDetails;
        }
    }
}
