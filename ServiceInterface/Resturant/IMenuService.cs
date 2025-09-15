using Restaurang_luna.DTOs.Menu;

namespace Restaurang_luna.ServiceInterface.Resturant
{
    public interface IMenuService
    {
        Task<MenuDto> GetById(int id, CancellationToken ct);
        Task<List<MenuDto>> GetMenu(CancellationToken ct);
        Task<List<MenuDto>> GetPopularMenu(CancellationToken ct);
        Task<CreateMenuDto> CreateMenuItem(CreateMenuDto dto, CancellationToken ct);
        Task<bool> PatchMenu(int id, PatchMenuDto dto, CancellationToken ct);
        Task<bool> DeleteMenuItem(int id, CancellationToken ct);
    }
}
