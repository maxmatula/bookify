using System.Threading.Tasks;
using Bookify.API.DTO.House;
using Bookify.API.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseController : ControllerBase
    {
        private readonly IHouseService _houseService;
        public HouseController(IHouseService houseService)
        {
            _houseService = houseService;
        }

        // GET api/house/
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var houses = await _houseService.GetAll();

            return Ok(houses);
        }

        // GET api/house/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            var house = await _houseService.GetById(id);

            return Ok(house);
        }

        // POST api/house/
        [HttpPost]
        public async Task<IActionResult> Add(HouseForCreateDto house)
        {
            if (house == null)
            {
                return BadRequest("Add object is null");
            }

            var result = await _houseService.Create(house);

            if (result)
            {
                return Ok("Created successfully");
            }
            else 
            {
                return BadRequest("Failed to Create");
            }
        }

        // POST api/house/edit
        [HttpPost("edit")]
        public async Task<IActionResult> Edit(HouseForEditDto house)
        {
            var result = await _houseService.Edit(house);

            if (result)
            {
                return Ok("Edit successful");
            }
            else 
            {
                return BadRequest("Failed to Edit");
            }
        }

        // DELETE api/house/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _houseService.Delete(id);

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