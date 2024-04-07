using Project.Domain.Abstractions.CommandRepositories;
using Project.Domain.Entities;
using Project.Infrastructure.DataContext;
using Project.Infrastructure.Implementation.Command.Base;

namespace Project.Infrastructure.Implementation.Command
{
    public class CustomerCommandRepository : CommandRepository<Customer> , ICustomerCommandRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CustomerCommandRepository(ApplicationDbContext applicationDbContext):base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        // Extaind your code 
    }
}
