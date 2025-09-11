using Microsoft.IdentityModel.Tokens;
using Restaurang_luna.DTOs.Booking.Other;
using Restaurang_luna.Models;
using System.Linq.Expressions;

namespace Restaurang_luna.Extensions.Mappers
{
    public class BookingMapper : IMapper<Booking, BookingListDto>
    {


        public BookingMapper()
        {
        }
        private const int SlotMinutes = 120;

        public Expression<Func<Booking, BookingListDto>> Projection(DateTimeOffset now) => b => new BookingListDto
        {
            
            //maps properites
            BookingId = b.BookingId,
            StartAt = b.StartAt,
            EndAt = b.StartAt.AddMinutes(SlotMinutes),
            Duration = SlotMinutes,
            GuestAmount = b.GuestAmount,
            TableId = b.TableId_FK,
            TableNr = b.Table.TableNr,
            Status = b.Status,
            SnapshotName = b.SnapshotName,
            SnapshotPhone = b.SnapshotPhone,
            SnapshotEmail = b.SnapshotEmail,
            Bucket =
                      (b.StartAt.AddMinutes(SlotMinutes) <= now || b.Status == BookingStatus.Completed ||
                        b.Status == BookingStatus.Cancelled) ? BookingBucket.Previous : BookingBucket.Future
        };
    }
}
