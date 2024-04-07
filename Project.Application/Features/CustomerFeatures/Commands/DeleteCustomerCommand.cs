using MediatR;
using Project.Application.Interfaces;

namespace Project.Application.Features.CustomerFeatures.Commands
{
    public class DeleteCustomerCommand : IRequest<string>
    {
        public DeleteCustomerCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; private set; }
    }
    public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, string>
    {
        private readonly ICustomerService _customerService;

        public DeleteCustomerHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<string> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var result = await _customerService.DeleteCustomerAsync(request.Id);
            return result ? "Customer Deleted Successfully !" : "Failed to delete customer.";
        }
    }
}
