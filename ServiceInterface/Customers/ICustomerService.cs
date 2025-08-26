using Restaurang_luna.DTOs.Customer;

namespace Restaurang_luna.ServiceInterface.Customers
{
    public interface ICustomerService
    {
        Task<List<CustomerDto>> GetCustomers(CancellationToken ct);
        Task<CustomerDto> GetCusstomer(Guid Id, CancellationToken ct);
        Task<CustomerDto> CreateCustomer(CustomerDto dto, CancellationToken ct);
        Task<Dictionary<string, object>> PatchCustomer(Guid id, PatchCustomerDto dto, CancellationToken ct);
        Task<bool> DeleteCustomer(Guid id, CancellationToken ct);
    }
}
