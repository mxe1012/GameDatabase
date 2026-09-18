using Microsoft.AspNetCore.Mvc;

using GameDatabase.DTOs;
using GameDatabase.Services;

namespace GameDatabase.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var genres = await _genreService.GetGenreResponseDtosAsync();
            return Ok(genres);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {   
            try
            {
                var genre = await _genreService.GetGenreByIdAsync(id);
                return Ok(genre);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(GenreRequestDto dto)
        {
            var created = await _genreService.AddGenreAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.GenreId }, created);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, GenreRequestDto dto)
        {
            try
            {
                await _genreService.UpdateGenreAsync(id, dto);
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
                await _genreService.DeleteGenreAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

    }
}