using Restaurang_luna.DTOs.Customer;
using Restaurang_luna.DTOs.Table;

namespace Restaurang_luna.ServiceInterface.Customer
{
    public interface ICustomerService
    {
        Task<List<CustomerDto>> GetCustomers(CancellationToken ct);
        Task<CustomerDto> GetCusstomer(Guid Id, CancellationToken ct);
        Task<CustomerDto> CreateCustomer(CustomerDto dto, CancellationToken ct);
        Task<Dictionary<string, object>> PatchCustomer(Guid id, PatchCustomerDto dto, CancellationToken ct);
        //Task<bool> DeleteCustomer(int id, CancellationToken ct);
    }
}
