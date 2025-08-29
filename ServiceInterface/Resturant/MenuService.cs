using Restaurang_luna.Data;
using Restaurang_luna.DTOs.Menu;

namespace Restaurang_luna.ServiceInterface.Resturant
{
    public class MenuService : IMenuService
    {
        private readonly LunaDbContext _context;
        public MenuService(LunaDbContext context)
        {
            _context = context;
        }

        public async Task<MenuDto> GetMenu(CancellationToken ct)
        {
            var menuList = await _context.Menus
                .ToListAsync();
        }
    }
}
