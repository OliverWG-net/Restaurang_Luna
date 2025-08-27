using Restaurang_luna.DTOs.Booking.Other;
using Restaurang_luna.Models;
using System.Runtime.CompilerServices;

namespace Restaurang_luna.Extensions
{
    public static class BookingQueryExtensions
    {
        public static IQueryable<Booking> WhereBucket(this IQueryable<Booking> source, BookingBucket bucket, DateTimeOffset now)
        {
            return bucket switch
            {
                //standard query depending on bucket
                BookingBucket.Previous => source.Where(b =>
                (b.StartAt.AddMinutes(b.Duration) <= now) ||
                b.Status == BookingStatus.Completed ||
                b.Status == BookingStatus.Cancelled),

                BookingBucket.Current => source.Where(b =>
                (b.Status == BookingStatus.Confirmed ||
                b.Status == BookingStatus.CheckedIn ||
                b.Status == BookingStatus.NoShow) &&
                b.StartAt <= now &&
                now < b.StartAt.AddMinutes(b.Duration)),

                BookingBucket.Future => source.Where(b =>
                b.StartAt > now &&
                b.Status != BookingStatus.Cancelled),

                BookingBucket.All => source,

                _ => source
            };
        }
        public static IOrderedQueryable<Booking> OrderBucket(this IQueryable<Booking> source, BookingBucket bucket)
        {
            return bucket switch
            {
                //standard ordering depending on bucket
                BookingBucket.Previous => source.OrderByDescending(b => b.StartAt),
                BookingBucket.Current => source.OrderBy(b => b.StartAt),
                BookingBucket.Future => source.OrderBy(b => b.StartAt),

                _ => source.OrderBy(b => b.StartAt)
            };
        }
        public static IQueryable<BookingListDto> SelectListDto(this IQueryable<Booking> source, BookingBucket bucket, DateTimeOffset now)
        {
            return source.Select(b => new BookingListDto
            {
                //List reuturn view
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
                    (b.EndAt <= now || b.Status == BookingStatus.Completed || b.Status == BookingStatus.Cancelled)
                        ? BookingBucket.Previous : ((b.Status == BookingStatus.Confirmed || b.Status == BookingStatus.CheckedIn || b.Status == BookingStatus.NoShow)
                       && b.StartAt <= now && now < b.EndAt) ? BookingBucket.Current : BookingBucket.Future
            });
        }
    }
}
