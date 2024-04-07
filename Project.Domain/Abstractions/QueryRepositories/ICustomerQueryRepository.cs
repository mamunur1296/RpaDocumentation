using Project.Domain.Abstractions.QueryRepositories.Base;
using Project.Domain.Entities;


namespace Project.Domain.Abstractions.QueryRepositories
{
    public interface ICustomerQueryRepository : IQueryRepository<Customer>
    {
        Task<Customer> GetCustomerByEmail(string email);
    }
}
