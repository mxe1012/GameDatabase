using Microsoft.AspNetCore.Mvc;

using GameDatabase.DTOs;
using GameDatabase.Services;

using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(EngineRequestDto dto)
        {
            var created = await _engineService.AddEngineAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.EngineId }, created);
        }
        [Authorize]
        [HttpPut("{id}")]
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
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, bool isHardDelete=false)
        {
            if(isHardDelete && !User.IsInRole("Admin"))
            {
                return Forbid();
            }
            try
            {
                await _engineService.DeleteEngineAsync(id, isHardDelete);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

    }
}