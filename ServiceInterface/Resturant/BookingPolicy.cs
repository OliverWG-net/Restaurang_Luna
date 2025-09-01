namespace Restaurang_luna.ServiceInterface.Resturant
{
    public class BookingPolicy : IBookingPolicy
    {
        public int SlotMinutes => 120;
        public IReadOnlyList<int> AllowedStartHours { get; } = new[] { 16, 18, 20 };

        public bool IsValidStart(DateTimeOffset start) =>
            AllowedStartHours.Contains(start.Hour) &&
            start.Minute == 0 && start.Second == 0;

        public DateTimeOffset GetEnd(DateTimeOffset start) => start.AddMinutes(SlotMinutes);
    }
}
