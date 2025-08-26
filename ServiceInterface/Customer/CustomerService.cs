using Microsoft.EntityFrameworkCore;
using Restaurang_luna.DTOs.Customer;
using Resturang_luna.Data;
using Resturang_luna.Models;
using System.Reflection.Metadata.Ecma335;


namespace Restaurang_luna.ServiceInterface.Customer
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

            var customer = new Resturang_luna.Models.Customer
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

            var changedFields = new Dictionary<string, object>();

            var dtoProp = typeof(PatchCustomerDto).GetProperties();
            var entityProp = typeof(Resturang_luna.Models.Customer).GetProperties();

            foreach (var prop in dtoProp)
            {
                var value = prop.GetValue(dto);

                if (value != null)
                {
                    var matchingProp = entityProp.FirstOrDefault(p => p.Name == prop.Name);

                    if (matchingProp != null && matchingProp.CanWrite)
                    {
                        matchingProp.SetValue(customer, value);
                        changedFields[prop.Name] = value;
                    }
                }
            }

            await _context.SaveChangesAsync(ct);

            return changedFields;
        }
    }
}
