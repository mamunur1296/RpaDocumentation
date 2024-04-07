
using MediatR;
using Project.Application.Interfaces;


namespace Project.Application.Features.CustomerFeatures.Commands
{
    public class CreateCustomerCommand : IRequest<string>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? ContactNumber { get; set; }
        public string? Address { get; set; }
    }
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, string>
    {
        private readonly ICustomerService _customerService;

        public CreateCustomerHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<string> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var result = await _customerService.CreateCustomerAsync(request.FirstName, request.LastName, request.Email, request.ContactNumber, request.Address);
            return result.isSucceed ? result.Id.ToString() : "Failed to create customer.";
        }

    }
}
