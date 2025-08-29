namespace Restaurang_luna.DTOs.Booking.Response
{
    public class AvailabilityResponseDto
    {
        public DateTimeOffset RequestStart { get; set; }
        public int DurationMinutes { get; set; }
        public int Guest { get; set; }
        public List<AvailabilityResponseDto> Suggestions = new();
    }
}
