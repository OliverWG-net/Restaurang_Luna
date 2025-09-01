using Restaurang_luna.DTOs.Booking.AvailabilityDto;
using Restaurang_luna.DTOs.Booking.Request;
using Restaurang_luna.DTOs.Booking.Response;

namespace Restaurang_luna.ServiceInterface.Resturant
{
    public interface IAvailabilityService
    {
        Task<DayAvailabilityDto> GetAvailability(DateOnly date, int guests, TimeSpan offset, CancellationToken ct);
    }
}
