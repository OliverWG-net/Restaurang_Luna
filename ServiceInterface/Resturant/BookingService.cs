using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurang_luna.Data;
using Restaurang_luna.DTOs.Booking.Other;
using Restaurang_luna.DTOs.Booking.Request;
using Restaurang_luna.Extensions;
using Restaurang_luna.Extensions.Mappers;
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
                .SelectListDto(bucket, now)
                .ToListAsync(ct);
        }

        public async Task<BookingListDto> GetBookingById(Guid id, DateTimeOffset now, CancellationToken ct)
        {
            var mapper = new BookingMapper(now);

            var booking = await _context.Bookings
                .Where(b => b.BookingId == id)
                .AutoMap(mapper)
                .FirstOrDefaultAsync(ct);

            if (booking == null)
                return null;

            return (booking);


        }
        public async Task<BookingListDto?> CreateBooking(BookingRequestDto dto, DateTimeOffset now, CancellationToken ct)
        {
            if (dto.Duration <= 0)
                throw new ArgumentOutOfRangeException(nameof(dto.Duration));

            if (dto.GuestAmount <= 0)
                throw new ArgumentOutOfRangeException(nameof(dto.Duration));

            var tableExist = await _context.Tables
                .AnyAsync(t => t.TableId == dto.TableId, ct);

            if (!tableExist)
                throw new InvalidOperationException("Table not found");

            var booking = new Booking
            {
                BookingId = Guid.NewGuid(),
                StartAt = dto.StartAt,
                Duration = dto.Duration,
                GuestAmount = dto.GuestAmount,
                TableId_FK = dto.TableId,
                CustomerId_FK = dto.CustomerId,

            };

            if (dto.CustomerId is Guid customerId)
            {
                var customer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.CustomerId == customerId, ct);

                if (customer is null)
                    throw new InvalidOperationException("Customer not found");

                booking.CaptureSnapshotFrom(customer);
            }
            else
            {
                var name = string.IsNullOrWhiteSpace(dto.SnapshotName) ? "Guest" : dto.SnapshotName!;
                booking.CaptureSnapshot(name, dto.SnapshotPhone, dto.SnapshotEmail, dto.SnapshotNotes ?? "");
            }

            booking.LockSnapshot();

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync(ct);

            var mapper = new BookingMapper(now);

            var result = await _context.Bookings
                .Where(b => b.BookingId == booking.BookingId)
                .Select(mapper.Map)
                .FirstOrDefaultAsync(ct);

            return result;
        }
        private async Task<bool> IsTableFree(int tableId, DateTimeOffset startAt, int duration, CancellationToken ct)
        {
            var endAt = startAt.AddMinutes(duration);

            return !await _context.Bookings
                .AnyAsync(b =>
                b.TableId_FK == tableId &&
                b.Status != BookingStatus.Cancelled &&
                b.StartAt < endAt &&
                startAt < b.StartAt.AddMinutes(b.Duration), ct);
        }
    }
}
