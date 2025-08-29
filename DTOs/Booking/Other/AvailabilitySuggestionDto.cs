using Restaurang_luna.DTOs.Table;

namespace Restaurang_luna.DTOs.Booking.Other
{
    public class AvailabilitySuggestionDto
    {
        public DateTimeOffset StartAt { get; set; }
        public DateTimeOffset EndAt { get; set; }
        public List<TableOptionDto> Tables { get; set; } = new();
    }
}
