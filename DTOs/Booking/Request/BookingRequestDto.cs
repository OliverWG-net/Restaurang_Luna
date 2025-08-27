namespace Restaurang_luna.DTOs.Booking.Request
{
    public class BookingRequestDto
    {
        public DateTimeOffset StartAt { get; init; }
        public int Duration { get; init; } = 120;
        public int GuestAmount { get; init; }

        public int TableId { get; init; }


        public Guid? CustomerId { get; init; }


        public string? SnapshotName { get; init; }
        public string? SnapshotPhone { get; init; }
        public string? SnapshotEmail { get; init; }
        public string? SnapshotNotes { get; init; }
    }
}
