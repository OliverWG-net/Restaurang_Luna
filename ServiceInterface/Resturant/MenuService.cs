using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Restaurang_luna.Data;
using Restaurang_luna.DTOs.Booking.Other;
using Restaurang_luna.DTOs.Customer;
using Restaurang_luna.DTOs.Menu;
using Restaurang_luna.Extensions;
using Restaurang_luna.Models;

namespace Restaurang_luna.ServiceInterface.Resturant
{
    public class MenuService : IMenuService
    {
        private readonly LunaDbContext _context;
        public MenuService(LunaDbContext context)
        {
            _context = context;
        }

        public async Task<MenuDto> GetById(int id, CancellationToken ct)
        {
            var menuItem = await _context.Menus
                .FirstOrDefaultAsync(m => m.MenuId == id);

            if (menuItem == null)
                return null;

            var menuDto = new MenuDto
            {
                MenuId = menuItem.MenuId,
                MenuItem = menuItem.MenuItem,
                Price = menuItem.Price,
                Description = menuItem.Description,
                IsPopular = menuItem.IsPopular,
                PictureUrl = menuItem.PictureUrl
            };

            return menuDto;
        }

        public async Task<List<MenuDto>> GetMenu(CancellationToken ct)
        {
            var menuList = await _context.Menus
                .ToListAsync();

            if (menuList.Count == 0)
                throw new NullReferenceException("No items were found");

            var menuDto = menuList.Select(m => new MenuDto
            {
                MenuId = m.MenuId,
                MenuItem = m.MenuItem,
                Price = m.Price,
                Description = m.Description,
                IsPopular = m.IsPopular,
                PictureUrl = m.PictureUrl
            }).ToList();

            return (menuDto);
        }
        public async Task<List<MenuDto>> GetPopularMenu(CancellationToken ct)
        {
            var menuList = await _context.Menus
                .Where(m => m.IsPopular == true)
                .ToListAsync();

            if (menuList.Count == 0)
                throw new NullReferenceException("No items were found");

            var menuDto = menuList.Select(m => new MenuDto
            {
                MenuItem = m.MenuItem,
                Price = m.Price,
                Description = m.Description,
                IsPopular = m.IsPopular,
                PictureUrl = m.PictureUrl
            }).ToList();

            return (menuDto);
        }
        public async Task<MenuDto> CreateMenuItem(MenuDto dto, CancellationToken ct)
        {
            var item = await _context.Menus
                .AnyAsync(m => m.MenuItem == dto.MenuItem);

            if (item)
                return null;

            var newItem = new Menu
            {
                MenuItem = dto.MenuItem,
                Price = dto.Price,
                Description = dto.Description,
                IsPopular = dto.IsPopular,
                PictureUrl = dto.PictureUrl
            };
            _context.Menus.Add(newItem);
            await _context.SaveChangesAsync(ct);

            return new MenuDto
            {
                MenuItem = dto.MenuItem,
                Price = dto.Price,
                Description = dto.Description,
                IsPopular = dto.IsPopular,
                PictureUrl = dto.PictureUrl
            };
        }
        public async Task<bool> PatchMenu(int id, PatchMenuDto dto, CancellationToken ct)
        {
            var menu = await _context.Menus
                .FirstOrDefaultAsync(m => m.MenuId == id);
            if (menu == null)
                return false;

            var changedFields = menu.PatchFrom(dto);

            if (changedFields.Count > 0)
                await _context.SaveChangesAsync(ct);

            return true;
        }
        public async Task<bool> DeleteMenuItem(int id, CancellationToken ct)
        {
            var menu = await _context.Menus
                .FirstOrDefaultAsync(m => m.MenuId == id);

            if (menu == null)
                return false;

            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync(ct);

            return true;
        }
    }
}
