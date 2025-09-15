using Microsoft.EntityFrameworkCore;
using Restaurang_luna.Data;
using Restaurang_luna.DTOs.Booking.AvailabilityDto;
using Restaurang_luna.DTOs.Booking.Request;
using Restaurang_luna.DTOs.Booking.Response;
using Restaurang_luna.DTOs.Table;
using Restaurang_luna.Models;
using System.Diagnostics;

namespace Restaurang_luna.ServiceInterface.Resturant
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly LunaDbContext _context;
        public AvailabilityService(LunaDbContext context)
        {
            _context = context;
        }
        public async Task<DayAvailabilityDto> GetAvailability(DateOnly date, int guests, TimeSpan offset, CancellationToken ct)
        {
            // array with allowed start times
            var starts = new[]
            {
                new DateTimeOffset(date.Year, date.Month, date.Day, 16,0,0, offset),
                new DateTimeOffset(date.Year, date.Month, date.Day, 18,0,0, offset),
                new DateTimeOffset(date.Year, date.Month, date.Day, 20,0,0, offset),
                new DateTimeOffset(date.Year, date.Month, date.Day, 22,0,0, offset),
            };

            //list of tables matching guest cpacity
            var tables = await _context.Tables
                .Where(t => t.Capacity >= guests)
                .Select(t => new { t.TableId, t.TableNr, t.Capacity })
                .ToListAsync(ct);

            //list of busy tables
            var busy = await _context.Bookings
                .Where(b => starts.Contains(b.StartAt) && b.Status != BookingStatus.Cancelled && b.Status != BookingStatus.Completed)
                .Select(b => new { b.StartAt, b.TableId_FK })
                .ToListAsync(ct);

            var result = new DayAvailabilityDto
            {
                Date = date,
                Guests = guests
            };

            //loops over all start times
            foreach (var start in starts)
            {
                var end = start.AddMinutes(120);

                //find all tables that are busy
                var isBusy = busy
                    .Where(b => b.StartAt == start)
                    .Select(b => b.TableId_FK)
                    .ToHashSet();

                //find free tables by excluding busy tables
                var free = tables
                    .Where(t => !isBusy.Contains(t.TableId))
                    .Select(t => new TableOptionDto { TableId = t.TableId, TableNr = t.TableNr, Capacity = t.Capacity })
                    .ToList();

                result.Slots.Add(new SlotAvailabilityDto
                {
                    StartAt = start,
                    EndAt = end,
                    FreeTables = free
                });
                   
            }
            return result;
        }
        //old function
        public static DateTimeOffset TruncateMinutes(DateTimeOffset dto, int roundedMinutes)
        {
            var minutes = (dto.Minute / roundedMinutes) * roundedMinutes;
            return new DateTimeOffset(dto.Year, dto.Month, dto.Day, dto.Hour, minutes, 0, dto.Offset);
        }
    }
}
