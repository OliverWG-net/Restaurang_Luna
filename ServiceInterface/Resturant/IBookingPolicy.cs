namespace Restaurang_luna.ServiceInterface.Resturant
{
    public interface IBookingPolicy
    {
        bool IsValidStart(DateTimeOffset start);
        int SlotMinutes { get; }
        IReadOnlyList<int> AllowedStartHours { get; }
        DateTimeOffset GetEnd(DateTimeOffset start);
    }
}
