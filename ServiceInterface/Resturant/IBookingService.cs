using Restaurang_luna.DTOs.Booking.Other;
using Restaurang_luna.Models;

namespace Restaurang_luna.ServiceInterface.Resturant
{
    public interface IBookingService
    {
        Task<IReadOnlyList<BookingListDto>> GetListBucket(BookingBucket bucket, DateTimeOffset now, CancellationToken ct);
    }
}
