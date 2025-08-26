using Microsoft.EntityFrameworkCore;
using Restaurang_luna.DTOs.Table;
using Restaurang_luna.Data;
using Restaurang_luna.Models;
using Restaurang_luna.Extensions;

namespace Restaurang_luna.ServiceInterface.Resturant
{
    public class TableService : ITableService
    {
        private readonly LunaDbContext _context;

        public TableService(LunaDbContext context)
        {
            _context = context;
        }
        public async Task<List<TableDto>> GetTables(CancellationToken ct)
        {
            var tables = await _context.Tables
                .ToListAsync(ct);
            if (tables == null)
                return null;

            var tablesDto = tables.Select(t => new TableDto
            {
                TableNr = t.TableNr,
                Capacity = t.Capacity
            }).ToList();

            return (tablesDto);
        }

        public async Task<TableDto> GetTable(int id, CancellationToken ct)
        {
            var table = await _context.Tables
                .FirstOrDefaultAsync(t => id == t.TableId, ct);
            if (table == null)
                return null;

            var tableDto = new TableDto
            {
                TableNr = table.TableNr,
                Capacity = table.Capacity
            };

            return tableDto;
        }

        public async Task<TableDto> CreateTable(TableDto dto, CancellationToken ct)
        {
            var existingTable = await _context.Tables
                .FirstOrDefaultAsync(t => t.TableNr == dto.TableNr, ct);

            if (existingTable != null)
                return null;

            var table = new Table
            {
                TableNr = dto.TableNr,
                Capacity = dto.Capacity
            };

            _context.Tables.Add(table);
            await _context.SaveChangesAsync(ct);

            return new TableDto
            {
                TableNr = table.TableNr,
                Capacity = table.Capacity
            };
        }
        public async Task<Dictionary<string, object>> PatchTable(int id, TablePatchDto dto, CancellationToken ct)
        {
            var table = await _context.Tables
                .FirstOrDefaultAsync(t => t.TableId == id, ct);

            if (table == null)
                return null;

            var changedFields = _context.PatchFrom(dto);

            if (changedFields.Count > 0)
                await _context.SaveChangesAsync(ct);

            return (changedFields);


        }
        public async Task<bool> DeleteTable(int id, CancellationToken ct)
        {
            var table = await _context.Tables
                .FirstOrDefaultAsync(t => t.TableId == id, ct);

            if (table == null)
            {
                return false;
            }

            _context.Tables.Remove(table);
            await _context.SaveChangesAsync(ct);
            return true;
        }
    }
}
