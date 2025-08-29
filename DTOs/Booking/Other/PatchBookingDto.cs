using Restaurang_luna.Models;

namespace Restaurang_luna.DTOs.Booking.Other
{
    public class PatchBookingDto
    {
        public DateTimeOffset? StartAt { get; set; }
        public int? Duration { get; set; }
        public int? GuestAmount { get; set; }
        public int? TableId_FK { get; set; }
        public Guid? CustomerId_FK { get; set; }

        public BookingStatus? Status { get; set; }
    }
}
