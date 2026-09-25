using Microsoft.AspNetCore.Mvc;

using GameDatabase.DTOs;
using GameDatabase.Services;
using Microsoft.AspNetCore.Authorization;


namespace GameDatabase.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeveloperController : ControllerBase
    {
        private readonly IDeveloperService _developerService;

        public DeveloperController(IDeveloperService developerService)
        {
            _developerService = developerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var developers = await _developerService.GetDeveloperResponseDtosAsync(false);
            return Ok(developers);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllIncludingDeleted()
        {
            var developers = await _developerService.GetDeveloperResponseDtosAsync(true);
            return Ok(developers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var developer = await _developerService.GetDeveloperByIdAsync(id, false);
                return Ok(developer);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all/{id}")]
        public async Task<IActionResult> GetByIdIncludingDeleted(int id)
        {
            try
            {
                var developer = await _developerService.GetDeveloperByIdAsync(id, true);
                return Ok(developer);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(DeveloperRequestDto dto)
        {
            var created = await _developerService.AddDeveloperAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.DeveloperId }, created);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DeveloperRequestDto dto)
        {
            try
            {
                await _developerService.UpdateDeveloperAsync(id, dto);
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
                await _developerService.DeleteDeveloperAsync(id, isHardDelete);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

    }

}