using System.Threading.Tasks;
using Bookify.API.DTO.Client;
using Bookify.API.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        // GET api/client/
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clients = await _clientService.GetAll();

            return Ok(clients);
        }

        // GET api/client/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            var client = await _clientService.GetById(id);

            return Ok(client);
        }

        // POST api/client/
        [HttpPost]
        public async Task<IActionResult> Add(ClientForCreateDto client)
        {
            if (client == null)
            {
                return BadRequest("Add object is null");
            }

            var result = await _clientService.Create(client);

            if (result != 0)
            {
                return Ok(result);
            }
            else 
            {
                return BadRequest("Failed to Create");
            }
        }

        // POST api/client/edit
        [HttpPost("edit")]
        public async Task<IActionResult> Edit(ClientForEditDto client)
        {
            var result = await _clientService.Edit(client);

            if (result)
            {
                return Ok("Edit successful");
            }
            else 
            {
                return BadRequest("Failed to Edit");
            }
        }

        // DELETE api/client/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _clientService.Delete(id);

            if (result)
            {
                return Ok("Deleted successfully");
            }
            else 
            {
                return BadRequest("Failed to Delete");
            }
        }
    }
}