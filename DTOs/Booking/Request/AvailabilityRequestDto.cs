namespace Restaurang_luna.DTOs.Booking.Request
{
    public class AvailabilityRequestDto
    {
        public DateTimeOffset StartAt { get; set; }
        public int GuestAmount { get; set; }
        public int SearchWindowMinutes { get; set; } = 45;
        public int SlotsMinutes { get; set; } = 15;
        public int MaxSuggestions { get; set; } = 8;
    }
}
