using Restaurang_luna.Models;

namespace Restaurang_luna.DTOs.Booking.Other
{
    public class BookingListDto
    {
        public Guid BookingId { get; init; }

        public DateTimeOffset StartAt { get; init; }
        public DateTimeOffset EndAt { get; init; } 
        public int Duration { get; init; } 

        public int GuestAmount { get; init; }

        public int TableId { get; init; }
        public string TableNr { get; init; }


        public BookingStatus Status { get; init; }


        public string SnapshotName { get; init; } = "";
        public string? SnapshotPhone { get; init; }
        public string? SnapshotEmail { get; init; }


        public bool IsPrevious { get; init; }       
        public bool IsCurrent { get; init; }        
        public bool IsFuture { get; init; }

    }
}
