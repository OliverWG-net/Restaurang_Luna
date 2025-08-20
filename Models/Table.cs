using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace Resturang_luna.Models
{
    [Index(nameof(TableNr))]
    public class Table
    {
        [Key]
        public int TableId { get; set; }

        public int TableNr { get; set; }

        [Range(1, 8)]
        public int Capacity { get; set; }

        public ICollection<Booking>? Booking { get; set; }

    }
}
