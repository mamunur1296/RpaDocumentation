using MediatR;
using Project.Application.DTOs;
using Project.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.UserFeatures.Queries
{
    public class GetUserDetailsQuery : IRequest<UserDetailsDTO>
    {
        public string UserId { get; set; }
    }

    public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, UserDetailsDTO>
    {
        private readonly IIdentityService _identityService;

        public GetUserDetailsQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<UserDetailsDTO> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
        {
            var (userId, fullName, userName, email, roles) = await _identityService.GetUserDetailsAsync(request.UserId);
            return new UserDetailsDTO() { Id = userId, FullName = fullName, UserName = userName, Email = email, Roles = roles };
        }
    }
}
