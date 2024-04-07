using AutoMapper;
using MediatR;
using Project.Application.DTOs;
using Project.Application.Interfaces;
using Project.Domain.Abstractions;
using Project.Domain.Entities;

namespace Project.Application.Features.CustomerFeatures.Queries
{
    public class GetCustomerByIdQuery : IRequest<CustomerDTO>
    {
        public GetCustomerByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
    public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDTO>
    {
        private readonly ICustomerService _customerService;


        public GetCustomerByIdHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public async Task<CustomerDTO> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var Customer = await _customerService.GetCustomerByIdAsync(request.Id);
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
