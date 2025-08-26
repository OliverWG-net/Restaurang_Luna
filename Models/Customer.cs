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
        [MaxLength(32)]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? Notes { get; set;  }

        public int NoShowCount { get; set; }

        public ICollection<Booking> Booking { get; set; } = new List<Booking>();

    }
}
