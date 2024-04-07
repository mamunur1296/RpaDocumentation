using AutoMapper;
using MediatR;
using Project.Application.DTOs;
using Project.Application.Interfaces;
using Project.Domain.Abstractions;

namespace Project.Application.Features.CustomerFeatures.Queries
{
    public class GetCustomerByEmailQuery : IRequest<CustomerDTO>
    {
        public GetCustomerByEmailQuery(string? email)
        {
            Email = email;
        }

        public string? Email { get; private set; }
    }
    public class GetCustomerByEmailHandler : IRequestHandler<GetCustomerByEmailQuery, CustomerDTO>
    {
        private readonly ICustomerService _customerService;
      

        public GetCustomerByEmailHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<CustomerDTO> Handle(GetCustomerByEmailQuery request, CancellationToken cancellationToken)
        {
            var Customer = await _customerService.GetCustomerByEmailAsync(request.Email);
            var customerDetails = new CustomerDTO()
            {
                Address = Customer.address,
                ContactNumber = Customer.contactNumber,
                Id = Customer.id,
                FirstName = Customer.firstName,
                LastName = Customer.lastName,
                Email = Customer.email,

            };
            return customerDetails;
        }
    }
}
