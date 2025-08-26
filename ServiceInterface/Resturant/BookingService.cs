using Microsoft.EntityFrameworkCore;
using Restaurang_luna.Data;
using Restaurang_luna.DTOs.Booking.Other;
using Restaurang_luna.Extensions;
using Restaurang_luna.Models;

namespace Restaurang_luna.ServiceInterface.Resturant
{
    public class BookingService : IBookingService
    {
        private readonly LunaDbContext _context;

        public BookingService(LunaDbContext context)
        {
            _context = context;
        }
        
        public async Task<IReadOnlyList<BookingListDto>> GetListBucket(BookingBucket bucket, DateTimeOffset now, CancellationToken ct)
        {
            return await _context.Bookings
                .WhereBucket(bucket, now)
                .OrderBucket(bucket)
                .SelectListDto(bucket)
                .ToListAsync(ct);
        }
    }
}
