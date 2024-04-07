using AutoMapper;
using MediatR;
using Project.Application.Interfaces;
using Project.Domain.Abstractions;


namespace Project.Application.Features.CustomerFeatures.Commands
{
    public class UpdateCustomerCommand : IRequest<string>
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? ContactNumber { get; set; }
        public string? Address { get; set; }
    }
    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, string>
    {
        private readonly ICustomerService _customerService;

        public UpdateCustomerHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<string> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var result = await _customerService.UpdateCustomerAsync(request.FirstName, request.LastName, request.ContactNumber, request.Address, request.Id);

            return result.isSucceed ? "customer Update Successfully  " : "Failed to update customer.";
        }

    }
}
