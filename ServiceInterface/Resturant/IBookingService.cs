using Restaurang_luna.DTOs.Booking.Other;
using Restaurang_luna.DTOs.Booking.Request;
using Restaurang_luna.Models;
using System.Threading.Tasks;

namespace Restaurang_luna.ServiceInterface.Resturant
{
    public interface IBookingService
    {
        Task<IReadOnlyList<BookingListDto>> GetListBucket(BookingBucket bucket, DateTimeOffset now, CancellationToken ct);
        Task<BookingListDto> GetBookingById(Guid id, DateTimeOffset now, CancellationToken ct);
        Task<BookingListDto?> CreateBooking(BookingRequestDto dto, CancellationToken ct);
        Task<bool> PatchBooking(Guid id, PatchBookingDto dto, CancellationToken ct);
        Task<bool> DeleteBooking(Guid id, CancellationToken ct);
    }
}
