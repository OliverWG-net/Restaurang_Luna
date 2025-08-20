using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Resturang_luna.Models
{
    [Index(nameof(PhoneNumber))]
    public class Customer
    {
        [Key]
        public Guid CustomerId { get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }
        [MaxLength(20)]
        public string? LastName { get; set; }

        [Phone]
        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        public ICollection<Booking>? Booking { get; set; }

    }
}
