using Project.Application.Exceptions;
using Project.Application.Interfaces;
using Project.Domain.Abstractions;
using Project.Domain.Entities;


namespace Project.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;

        public CustomerService(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;
        }

        public async Task<(bool isSucceed, Guid Id)> CreateCustomerAsync(string? firstName, string? lastName, string? email, string? contactNumber, string? address)
        {
            try
            {
                var customer = new Customer()
                {
                    Address = address,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    ContactNumber = contactNumber,
                    CreatedDate = DateTime.Now,
                    Id = Guid.NewGuid() 
                };

                await _unitOfWorkDb.customerCommandRepository.AddAsync(customer);
                await _unitOfWorkDb.SaveAsync(); 

                return (true, customer.Id); 
            }
            catch (Exception ex)
            {
                // Log the exception
                return (false, Guid.Empty);

            }
        }


        public async Task<bool> DeleteCustomerAsync(Guid id)
        {
            var customer = await _unitOfWorkDb.customerQueryRepository.GetByIdAsync(id);
            if (customer == null)
            {
                throw new NotFoundException("User not found");
                //throw new Exception("User not found");
            }
            var result =  _unitOfWorkDb.customerCommandRepository.DeleteAsync(customer);
            await _unitOfWorkDb.SaveAsync();
            return result.IsCompletedSuccessfully;
        }

        public async Task<IEnumerable<(Guid id, string fullName, string contactNumber, string email, string address)>> GetAllCustomerAsync()
        {
            var customers = await _unitOfWorkDb.customerQueryRepository.GetAllAsync();

            return customers.Select(c => (c.Id, $"{c.FirstName} {c.LastName}", c.ContactNumber, c.Email, c.Address)).ToList();
        }

        public async Task<(string? firstName, string? lastName, string? contactNumber, string email, string? address, Guid id)> GetCustomerByEmailAsync(string email)
        {
            var customer = await _unitOfWorkDb.customerQueryRepository.GetCustomerByEmail(email);

            if (customer == null)
            {
                throw new NotFoundException("Customer not found");
            }

            return (customer.FirstName, customer.LastName, customer.ContactNumber, customer.Email, customer.Address, customer.Id);
        }

        public async Task<(string? firstName, string? lastName, string? contactNumber, string email, string? address, Guid id)> GetCustomerByIdAsync(Guid id)
        {
            var customer = await _unitOfWorkDb.customerQueryRepository.GetByIdAsync(id);

            if (customer == null)
            {
                throw new NotFoundException("Customer not found");
            }

            return (customer.FirstName, customer.LastName, customer.ContactNumber,customer.Email, customer.Address, customer.Id);
        }



        public async Task<(bool isSucceed, Guid id)> UpdateCustomerAsync(string? firstName, string? lastName, string? contactNumber, string? address, Guid id)
        {
            var customer = await _unitOfWorkDb.customerQueryRepository.GetByIdAsync(id);
            if (customer == null)
            {
                throw new NotFoundException($"Customer Not Found {id}");
            }
            customer.FirstName = firstName;
            customer.LastName = lastName;
            customer.ContactNumber = contactNumber;
            customer.Address = address;
            try
            {
                await _unitOfWorkDb.customerCommandRepository.UpdateAsync(customer);
                await _unitOfWorkDb.SaveAsync();
                return (true, customer.Id);
            }
            catch (Exception ex)
            {
                return (false, Guid.Empty);
            }
        }
    }
}
