using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resturang_luna.Models
{
    [Index(nameof(StartAt))]
    public class Booking
    {
        [Key]
        public Guid BookingId { get; set; }

        public DateTimeOffset StartAt { get; set; }

        public int GuestAmount { get; set; }

        [ForeignKey("Table")]
        public int TableId_FK { get; set; }

        public Table Table { get; set; }

        [ForeignKey("Customer")]
        public Guid CustomerId_FK { get; set; }

        public Customer Customer { get; set; }
    }
}
