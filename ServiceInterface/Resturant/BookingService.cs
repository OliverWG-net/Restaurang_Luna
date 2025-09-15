using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Restaurang_luna.Data;
using Restaurang_luna.DTOs.Booking.Other;
using Restaurang_luna.DTOs.Booking.Request;
using Restaurang_luna.DTOs.Customer;
using Restaurang_luna.Extensions;
using Restaurang_luna.Extensions.Mappers;
using Restaurang_luna.Models;
using System.Data.Common;

namespace Restaurang_luna.ServiceInterface.Resturant
{
    public class BookingService : IBookingService
    {
        private readonly LunaDbContext _context;
        private readonly IBookingPolicy _policy;
        private readonly IMapper<Booking, BookingListDto> _mapper;

        public BookingService(LunaDbContext context, IBookingPolicy policy, IMapper<Booking, BookingListDto> mapper)
        {
            _context = context;
            _policy = policy;
            _mapper = mapper;
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
            var mapper = new BookingMapper();

            var booking = await _context.Bookings
                .Where(b => b.BookingId == id)
                .AutoMap(mapper, now)
                .FirstOrDefaultAsync(ct);

            if (booking == null)
                return null;

            return (booking);


        }
        public async Task<BookingListDto?> CreateBooking(BookingRequestDto dto, CancellationToken ct)
        {

            if (!_policy.IsValidStart(dto.StartAt))
                throw new InvalidOperationException("Start time must be 16:00, 18:00 or 20:00");



            var booking = new Booking
            {
                BookingId = Guid.NewGuid(),
                StartAt = dto.StartAt,
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

            try
            { 
                await _context.SaveChangesAsync(ct);

                await _context.Entry(booking).ReloadAsync(ct);
            }
            catch (DbException ex)
            {
                return null;
            }

            var now = DateTimeOffset.UtcNow;
            var mapper = new BookingMapper();

            var result = await _context.Bookings
                .AsNoTracking()
                .Where(b => b.BookingId == booking.BookingId)
                .AutoMap(mapper, now)
                .FirstOrDefaultAsync(ct);

            return (result);
        }
        public async Task<bool> PatchBooking(Guid id, PatchBookingDto dto, CancellationToken ct)
        {
            var booking = await _context.Bookings
                .FirstOrDefaultAsync(b => b.BookingId == id);
            if (booking == null)
                return false;
            var table = await _context.Tables
          .FirstOrDefaultAsync(t => t.TableId == dto.TableId_FK, ct);

            if (table == null)
                throw new ArgumentException("Table not found");

            if (dto.GuestAmount > table.Capacity)
                throw new ArgumentException($"Table {table.TableNr} capacity ({table.Capacity}) exceeded by guest count ({dto.GuestAmount})");

            var changedFields = booking.PatchFrom(dto);

            if (changedFields.Count > 0)
                await _context.SaveChangesAsync(ct);

            return true;
        }
        public async Task<bool> DeleteBooking(Guid id, CancellationToken ct)
        {
            var booking = await _context.Bookings
                .FirstOrDefaultAsync(b => b.BookingId == id);

            if (booking == null)
                return false;

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync(ct);

            return true;
        }
        private const int SlotMinutes = 120;
        private async Task<bool> IsTableFree(int tableId, DateTimeOffset startAt, int duration, CancellationToken ct)
        {
            
            var endAt = startAt.AddMinutes(duration);

            return !await _context.Bookings
                .AnyAsync(b =>
                b.TableId_FK == tableId &&
                b.Status != BookingStatus.Cancelled &&
                b.StartAt < endAt &&
                startAt < b.StartAt.AddMinutes(SlotMinutes), ct);
        }
    }
}
