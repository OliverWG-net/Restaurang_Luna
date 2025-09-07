using System.ComponentModel.DataAnnotations;

namespace Restaurang_luna.DTOs.Menu
{
    public class MenuDto
    {
        public int MenuId { get; set; }
        public string MenuItem { get; set; }
        public Double Price { get; set; }
        public string Description { get; set; }
        public bool IsPopular { get; set; }
        public string? PictureUrl { get; set; }
    }
}
