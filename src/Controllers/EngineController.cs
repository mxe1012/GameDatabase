using Microsoft.AspNetCore.Mvc;

using GameDatabase.DTOs;
using GameDatabase.Services;

namespace GameDatabase.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EngineController : ControllerBase
    {
        private readonly IEngineService _engineService;
        public EngineController(IEngineService engineService)
        {
            _engineService = engineService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var engines = await _engineService.GetEngineResponseDtosAsync();
            return Ok(engines);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var engine = await _engineService.GetEngineByIdAsync(id);
                return Ok(engine);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add(EngineRequestDto dto)
        {
            var created = await _engineService.AddEngineAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.EngineId }, created);
        }
        [HttpPut]
        public async Task<IActionResult> Update(int id, EngineRequestDto dto)
        {
            try
            {
                await _engineService.UpdateEngineAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _engineService.DeleteEngineAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

    }
}