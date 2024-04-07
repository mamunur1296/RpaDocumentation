

namespace Project.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<(bool isSucceed, Guid Id)> CreateCustomerAsync(string? firstName, string? lastName, string? email, string? contactNumber, string? address);
        Task<bool> DeleteCustomerAsync(Guid id);
        Task<(bool isSucceed,Guid id)> UpdateCustomerAsync(string? firstName, string? lastName, string? contactNumber, string? address,Guid id);
        Task<IEnumerable<(Guid id, string fullName, string contactNumber, string email, string address)>> GetAllCustomerAsync();
        Task<(string? firstName, string? lastName, string? contactNumber, string email, string? address, Guid id)> GetCustomerByIdAsync(Guid id);
        Task<(string? firstName, string? lastName, string? contactNumber, string email, string? address, Guid id)> GetCustomerByEmailAsync(string Email);
    }
}
