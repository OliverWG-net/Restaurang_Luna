using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurang_luna.DTOs.Table;
using Restaurang_luna.ServiceInterface.Resturant;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Restaurang_luna.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly ITableService _tableService;

        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }
        // GET: api/<TableController>
        [HttpGet]
        public async Task<ActionResult<List<TableDto>>> Get(CancellationToken ct)
        {
            var tables = await _tableService.GetTables(ct);

            if (tables == null || !tables.Any())
                return NotFound("No tables found");

            return Ok(tables);
        }

        // GET api/<TableController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TableDto>> Get(int id, CancellationToken ct)
        {
            var table = await _tableService.GetTable(id, ct);

            if (table == null)
                return NotFound("No table matching id was found");

            return Ok(table);
        }

        // POST api/<TableController>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<TableDto>> Post([FromBody] CreateTableDto dto, CancellationToken ct)
        {
            var newTable = await _tableService.CreateTable(dto, ct);

            if (newTable == null)
                return BadRequest("Could not create a new table, table already exist");

            return Ok($"A new table with table nr:{newTable.TableNr} with capcity of {newTable.Capacity}!");
        }
        [Authorize]
        [HttpPatch("{id}")]
        public async Task<ActionResult<TableDto>> Patch(int id, [FromBody] TablePatchDto dto, CancellationToken ct)
        {
            var succsess = await _tableService.PatchTable(id, dto, ct);
            if (!succsess)
                return BadRequest("Could not update menu item");

            var updated = await Get(id, ct);

            return Ok(updated.Value);
        }

        // DELETE api/<TableController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id, CancellationToken ct)
        {
            var success = await _tableService.DeleteTable(id, ct);
            if (success == false)
                return NotFound("Table not found");

            return Ok(success);
        }
    }
}
