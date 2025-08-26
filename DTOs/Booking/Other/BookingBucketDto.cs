namespace Restaurang_luna.DTOs.Booking.Other
{
    public class BookingBucketDto
    {
        public IReadOnlyList<BookingListDto> Previous { get; init; } = Array.Empty<BookingListDto>();
        public IReadOnlyList<BookingListDto> Current { get; init; } = Array.Empty<BookingListDto>();
        public IReadOnlyList<BookingListDto> Future { get; init; } = Array.Empty<BookingListDto>();
    }
}
