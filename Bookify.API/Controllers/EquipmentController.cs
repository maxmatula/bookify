using System.Threading.Tasks;
using Bookify.API.DTO.Equipment.Return;
using Bookify.API.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentService _equipmentService;
        public EquipmentController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        // GET api/equipment/
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var equipment = await _equipmentService.GetAll();

            return Ok(equipment);
        }

        // GET api/equipment/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            var equipment = await _equipmentService.GetById(id);

            return Ok(equipment);
        }

        // POST api/equipment/
        [HttpPost]
        public async Task<IActionResult> Add(EquipmentForCreateDto equipment)
        {
            if (equipment == null)
            {
                return BadRequest("Add object is null");
            }

            var result = await _equipmentService.Create(equipment);

            if (result)
            {
                return Ok("Created successfully");
            }
            else 
            {
                return BadRequest("Failed to Create");
            }
        }

        // POST api/equipment/edit
        [HttpPost("edit")]
        public async Task<IActionResult> Edit(EquipmentForEditDto equipment)
        {
            var result = await _equipmentService.Edit(equipment);

            if (result)
            {
                return Ok("Edit successful");
            }
            else 
            {
                return BadRequest("Failed to Edit");
            }
        }

        // DELETE api/equipment/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _equipmentService.Delete(id);

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