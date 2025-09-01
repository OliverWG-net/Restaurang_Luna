namespace Restaurang_luna.DTOs.Booking.AvailabilityDto
{
    public class DayAvailabilityDto
    {
        public DateOnly Date { get; set; }
        public int Guests { get; set; }
        public List<SlotAvailabilityDto> Slots { get; set; } = new();
    }
}
