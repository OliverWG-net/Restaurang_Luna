using Restaurang_luna.DTOs.Table;

namespace Restaurang_luna.DTOs.Booking.AvailabilityDto
{
    public class SlotAvailabilityDto
    {
        public DateTimeOffset StartAt { get; set; }
        public DateTimeOffset EndAt { get; set; }
        public List<TableOptionDto> FreeTables { get; set; } = new();
    }
}
