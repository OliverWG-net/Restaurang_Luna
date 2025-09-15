using Restaurang_luna.DTOs.Table;

namespace Restaurang_luna.ServiceInterface.Resturant
{
    public interface ITableService
    {
        Task<List<TableDto>> GetTables(CancellationToken ct);
        Task<TableDto> GetTable(int Id, CancellationToken ct);
        Task<TableDto> CreateTable(CreateTableDto dto, CancellationToken ct);
        Task<bool> PatchTable(int id, TablePatchDto dto, CancellationToken ct);
        Task<bool> DeleteTable(int id, CancellationToken ct);
    }
}
