using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Restaurang_luna.Models
{
    [Index(nameof(MenuItem))]
    public class Menu
    {
        [Key]
        public int MenuId { get; set; }
        [MaxLength(30)]
        public string MenuItem { get; set; }

        [Required]
        [Range(15, 500)]
        public Double Price { get; set; }

        [Required]
        [MaxLength(150)]
        public string Description { get; set; }

        [Required]
        public bool IsPopular { get; set; }

        [Url]
        public string? PictureUrl { get; set; }
    }
}
