using Restaurang_luna.DTOs.Booking.Other;
using Restaurang_luna.Models;
using System.Linq.Expressions;

namespace Restaurang_luna.Extensions.Mappers
{
    public class BookingMapper : IMapper<Booking, BookingListDto>
    {
        private readonly DateTimeOffset _now;

        public BookingMapper(DateTimeOffset now)
        {
            _now = now;
        }

        public Expression<Func<Booking, BookingListDto>> Map => b => new BookingListDto
        {
            //maps properites
            BookingId = b.BookingId,
            StartAt = b.StartAt,
            EndAt = b.StartAt.AddMinutes(b.Duration),
            Duration = b.Duration,
            GuestAmount = b.GuestAmount,
            TableId = b.TableId_FK,
            TableNr = b.Table.TableNr,
            Status = b.Status,
            SnapshotName = b.SnapshotName,
            SnapshotPhone = b.SnapshotPhone,
            SnapshotEmail = b.SnapshotEmail,
            Bucket =
                    (b.EndAt <= _now || b.Status == BookingStatus.Completed || b.Status == BookingStatus.Cancelled)
                        ? BookingBucket.Previous : ((b.Status == BookingStatus.Confirmed || b.Status == BookingStatus.CheckedIn || b.Status == BookingStatus.NoShow)
                       && b.StartAt <= _now && _now < b.EndAt) ? BookingBucket.Current : BookingBucket.Future
        };
    }
}
