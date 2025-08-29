using Microsoft.AspNetCore.Mvc;
using Restaurang_luna.DTOs.Customer;
using Restaurang_luna.ServiceInterface.Customers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Restaurang_luna.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<ActionResult<List<CustomerDto>>> Get(CancellationToken ct)
        {
            var customerList = await _customerService.GetCustomers(ct);

            if (customerList == null || !customerList.Any())
                return NotFound("No customers were found");

            return Ok(customerList);
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> Get(Guid id, CancellationToken ct)
        {
            var customer = await _customerService.GetCusstomer(id, ct);

            if (customer == null)
                return NotFound("No customer with matching Id was found");

            return Ok(customer);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public async Task<ActionResult<CustomerDto>> Post([FromBody] CustomerDto dto, CancellationToken ct)
        {
            var newCustomer = await _customerService.CreateCustomer(dto, ct);

            if (newCustomer == null)
                return BadRequest("No customer could be created");

            return Ok(newCustomer);
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult<Dictionary<object, string>>> Patch(Guid id, [FromBody] PatchCustomerDto dto, CancellationToken ct)
        {
            var updatedFields = await _customerService.PatchCustomer(id, dto, ct);

            if (updatedFields == null)
                return BadRequest("Could not update customer fields");

            return Ok(updatedFields);
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id, CancellationToken ct)
        {
            var success = await _customerService.DeleteCustomer(id, ct);
            if (!success)
                return NotFound("Table not found");

            return Ok(success);
        }
    }
}
