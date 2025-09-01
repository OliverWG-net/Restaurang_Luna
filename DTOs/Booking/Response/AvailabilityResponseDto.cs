namespace Restaurang_luna.DTOs.Booking.Response
{
    public class AvailabilityResponseDto
    {
        public DateTimeOffset RequestedStart { get; set; }
        public int DurationMinutes { get; set; } = 120;
        public int Guests { get; set; }
        public List<AvailabilityResponseDto> Suggestions = new();
    }
}
