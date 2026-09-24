using Microsoft.AspNetCore.Mvc;

using GameDatabase.DTOs;
using GameDatabase.Services;
using Microsoft.AspNetCore.Authorization;

namespace GameDatabase.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var games = await _gameService.GetGameResponseDtosAsync();
            return Ok(games);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var game = await _gameService.GetGameByIdAsync(id);
                return Ok(game);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(GameRequestDto dto)
        {
            var created = await _gameService.AddGameAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.GameId }, created);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, GameRequestDto dto)
        {
            try
            {
                await _gameService.UpdateGameAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, bool isHardDelete)
        {
            if(isHardDelete && !User.IsInRole("Admin"))
            {
                return Forbid();
            }
            try
            {
                await _gameService.DeleteGameAsync(id, isHardDelete);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        
    }
}