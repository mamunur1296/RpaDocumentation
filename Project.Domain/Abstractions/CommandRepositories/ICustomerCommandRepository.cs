using Project.Domain.Abstractions.CommandRepositories.Base;
using Project.Domain.Entities;

namespace Project.Domain.Abstractions.CommandRepositories
{
    public interface ICustomerCommandRepository : ICommandRepository<Customer>
    {
        // Extand for all if any
    }
}
