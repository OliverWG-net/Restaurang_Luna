using Microsoft.EntityFrameworkCore;
using Restaurang_luna.DTOs.Customer;
using Restaurang_luna.Data;
using Restaurang_luna.Models;
using Restaurang_luna.Extensions;


namespace Restaurang_luna.ServiceInterface.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly LunaDbContext _context;

        public CustomerService(LunaDbContext context)
        {
            _context = context;
        }
        public async Task<List<CustomerDto>> GetCustomers(CancellationToken ct)
        {
            var customers = await _context.Customers
                .AsNoTracking()
                .ToListAsync(ct);

            if (customers == null)
                return null;

            var customersDto = customers.Select(c => new CustomerDto
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                PhoneNumber = c.PhoneNumber,
                Email = c.Email,
                Notes = c.Notes,
                NoShowCount = c.NoShowCount
            }).ToList();

            return (customersDto);
        }
        public async Task<CustomerDto> GetCusstomer(Guid Id, CancellationToken ct)
        {
            var customer = await _context.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CustomerId == Id, ct);
            if (customer == null)
                return null;

            var customerDto = new CustomerDto
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,
                Notes = customer.Notes,
                NoShowCount = customer.NoShowCount
            };

            return (customerDto);
        }
        public async Task<CustomerDto> CreateCustomer(CustomerDto dto, CancellationToken ct)
        {
            var existingCustomer = await _context.Customers
                .AnyAsync(c => c.FirstName == dto.FirstName && c.PhoneNumber == dto.PhoneNumber, ct);

            if (existingCustomer)
                return null;

            var customer = new Customer
            {
                CustomerId = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                Notes = dto.Notes,
                NoShowCount = dto.NoShowCount
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync(ct);

            return new CustomerDto
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                Notes = dto.Notes,
                NoShowCount = dto.NoShowCount
            };
        }
        public async Task<Dictionary<string, object>> PatchCustomer(Guid id, PatchCustomerDto dto, CancellationToken ct)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == id, ct);

            if (customer is null)
                return null;

            var changedFields = customer.PatchFrom(dto);

            if (changedFields.Count > 0)
                await _context.SaveChangesAsync(ct);

            return changedFields;
        }
        public async Task<bool> DeleteCustomer(Guid id, CancellationToken ct)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(t => t.CustomerId == id, ct);

            if (customer == null)
                return false;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync(ct);

            return true;
        }
    }
}
