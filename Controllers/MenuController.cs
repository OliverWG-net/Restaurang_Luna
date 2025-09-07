using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurang_luna.DTOs.Menu;
using Restaurang_luna.ServiceInterface.Resturant;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Restaurang_luna.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }
        // GET: api/<MenuController>
        [HttpGet]
        public async Task<ActionResult<List<MenuDto>>> GetMenu(CancellationToken ct)
        {
            var menuList = await _menuService.GetMenu(ct);

            if (menuList.Count == 0 || menuList == null)
                return NotFound("No menu items were found");

            return Ok(menuList);
        }
        [HttpGet("Popular")]
        public async Task<ActionResult<List<MenuDto>>> GetPopularMenu(CancellationToken ct)
        {
            var menuList = await _menuService.GetPopularMenu(ct);

            if (menuList.Count == 0 || menuList == null)
                return NotFound("No menu items were found");

            return Ok(menuList);
        }

        // GET api/<MenuController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuDto>> GetById(int id, CancellationToken ct)
        {
            var menuItem = await _menuService.GetById(id, ct);

            if (menuItem == null)
                return NotFound("Item not found");

            return Ok(menuItem);
        }

        // POST api/<MenuController>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<MenuDto>> Post([FromBody] MenuDto dto, CancellationToken ct)
        {
            var newMenuItem = await _menuService.CreateMenuItem(dto, ct);

            if (newMenuItem == null)
                return BadRequest("Could not create new menu item");

            return Ok(newMenuItem);
        }

        // PUT api/<MenuController>/5

        [HttpPatch("{id}")]
        public async Task<ActionResult<MenuDto>> Patch(int id, [FromBody] PatchMenuDto dto, CancellationToken ct)
        {
            var success = await _menuService.PatchMenu(id, dto, ct);

            if (!success)
                return BadRequest("Could not update menu item");

            var updated = await GetById(id, ct);

            return Ok(updated);
        }

        // DELETE api/<MenuController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id, CancellationToken ct)  
        {
            var success = await _menuService.DeleteMenuItem(id, ct);

            if (!success)
                return BadRequest("Could not remove item");

            return Ok(success);
        }
    }
}
