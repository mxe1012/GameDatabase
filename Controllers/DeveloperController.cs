using Microsoft.AspNetCore.Mvc;

using GameDatabase.DTOs;
using GameDatabase.Services;


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
            var developers = await _developerService.GetDeveloperResponseDtosAsync();
            return Ok(developers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var developer = await _developerService.GetDeveloperByIdAsync(id);
                return Ok(developer);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(DeveloperRequestDto dto)
        {
            var created = await _developerService.AddDeveloperAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.DeveloperId }, created);
        }

        [HttpPut]
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

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _developerService.DeleteDeveloperAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

    }

}