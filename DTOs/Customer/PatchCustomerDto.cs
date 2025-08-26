namespace Restaurang_luna.DTOs.Customer
{
    public class PatchCustomerDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Notes { get; set; }
        public int? NoShowCount { get; set; }
    }
}
