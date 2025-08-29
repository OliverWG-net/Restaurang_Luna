using Restaurang_luna.Data;
using Restaurang_luna.DTOs.Booking.Request;
using Restaurang_luna.DTOs.Booking.Response;

namespace Restaurang_luna.ServiceInterface.Resturant
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly LunaDbContext _context;
        public AvailabilityService(LunaDbContext context)
        {
            _context = context;
        }
        public async Task<AvailabilityResponseDto> GetAvailability(AvailabilityRequestDto request, CancellationToken ct)
        {
            var requestedStart = TruncateMinutes(request.StartAt, request.SlotsMinutes);
        }
        public static DateTimeOffset TruncateMinutes(DateTimeOffset dto, int roundedMinutes)
        {
            var minutes = (dto.Minute / roundedMinutes) * roundedMinutes;
            return new DateTimeOffset(dto.Year, dto.Month, dto.Day, dto.Hour, minutes, 0, dto.Offset);
        }
    }
}
