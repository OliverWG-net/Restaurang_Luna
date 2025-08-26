using Restaurang_luna.DTOs.Table;

namespace Restaurang_luna.ServiceInterface.Resturant
{
    public interface ITableService
    {
        Task<List<TableDto>> GetTables(CancellationToken ct);
        Task<TableDto> GetTable(int Id, CancellationToken ct);
        Task<TableDto> CreateTable(TableDto dto, CancellationToken ct);
    }
}
