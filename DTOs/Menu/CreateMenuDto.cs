namespace Restaurang_luna.DTOs.Menu
{
    public class CreateMenuDto
    {
        public string MenuItem { get; set; }
        public Double Price { get; set; }
        public string Description { get; set; }
        public bool IsPopular { get; set; }
        public string? PictureUrl { get; set; }
    }
}
