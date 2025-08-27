using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurang_luna.Models
{
    public enum BookingStatus { Confirmed, CheckedIn, Completed, NoShow, Cancelled }
    public enum BookingBucket { Previous, Current, Future, All}

    [Index(nameof(StartAt))]
    [Index(nameof(Status), nameof(StartAt))]
    [Index(nameof(TableId_FK), nameof(StartAt))]
    public class Booking
    {
        [Key]
        public Guid BookingId { get; set; }

        public DateTimeOffset StartAt { get; set; }
        public int Duration { get; set; } = 120;
        public DateTimeOffset EndAt => StartAt.AddMinutes(Duration);

        public int GuestAmount { get; set; }

        [ForeignKey("Table")]
        public int TableId_FK { get; set; }
        public Table? Table { get; set; }

        [ForeignKey("Customer")]
        public Guid? CustomerId_FK { get; set; }
        public Customer? Customer { get; set; }

        //Snapshot of Customer
        public string SnapshotName { get; private set; } = "";
        public string? SnapshotPhone { get; private set; }
        public string? SnapshotEmail { get; private set; }
        public string? SnapshotNotes { get; private set; }
        public bool IsSnapshotLocked { get; private set; } = false;

        public BookingStatus Status { get; private set; } = BookingStatus.Confirmed;

        //attendance sets time when corresponding method is called
        public DateTimeOffset? CheckedInAt { get; private set; }
        public DateTimeOffset? CompletedAt { get; private set; }
        public DateTimeOffset? CancelledAt { get; private set; }

        //History
        public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();

        //call before save async to lock snapshot
        public void LockSnapshot() => IsSnapshotLocked = true;

        //call when creating booking for exsisting customer
        public void CaptureSnapshotFrom(Customer customer)
        {
            if (IsSnapshotLocked)
                return;
            SnapshotName = string.IsNullOrWhiteSpace(customer.LastName) ? customer.FirstName : $"{customer.FirstName} {customer.LastName}";
            SnapshotPhone = customer.PhoneNumber;
            SnapshotEmail = customer.Email;
            SnapshotNotes = customer.Notes;
        }
        //when creating new customer
        public void CaptureSnapshot(string name, string? phone, string? email, string notes)
        {
            if (IsSnapshotLocked)
                return;
            SnapshotName = name;
            SnapshotPhone = phone;
            SnapshotEmail = email;
            SnapshotNotes = notes;
        }
        //checks in customer
        public void CheckIn(DateTimeOffset now)
        {
            if (Status is BookingStatus.Confirmed or BookingStatus.NoShow)
            {
                Status = BookingStatus.CheckedIn;
                CheckedInAt = now;
            }
        }
        //set customer to no show
        public void NoShow()
        {
            if (Status == BookingStatus.Confirmed)
                Status = BookingStatus.NoShow;
        }
        //Set booking to completed
        public void Complete(DateTimeOffset now)
        {
            if (Status == BookingStatus.CheckedIn)
            {
                Status = BookingStatus.Completed;
                CompletedAt = now;
            }
        }
        //cancel booking
        public void Cancel(DateTimeOffset now)
        {
            if (Status == BookingStatus.Confirmed)
            {
                Status = BookingStatus.Cancelled;
                CancelledAt = now;

            }
        }

    }
}
