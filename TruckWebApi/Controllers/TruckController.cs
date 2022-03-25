using TruckWebApi.DTO;
using TruckWebApi.Models;
using TruckWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TruckWebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class TruckController : ControllerBase
    {
        private ITruckApplicationService _truckService;
        public TruckController(ITruckApplicationService userService)
        {
            _truckService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateForm model)
        {
            var result = await _truckService.Post(model);
            return CreatedAtAction(nameof(GetAllFiltered), new { id = result.Id }, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _truckService.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] CreateForm model)
        {
            var result = await _truckService.Put(id, model);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _truckService.GetAll();
            return Ok(result);
        }

        [HttpGet("query")]
        public async Task<IActionResult> GetAllFiltered([FromQuery] int id, string model, int modelYear, int manufacYear, string nickName)
        {
            var query = new TruckQueryModel()
            {
                Id = id,
                ManufacYear = manufacYear,
                Model = model,
                ModelYear = modelYear,
                NickName = nickName
            };

            var result = await _truckService.GetAll(query);
            return Ok(result);
        }
    }
}
