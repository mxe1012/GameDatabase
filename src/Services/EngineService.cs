using GameDatabase.DTOs;
using GameDatabase.Entites;
using GameDatabase.Repositories;

namespace GameDatabase.Services
{
    public class EngineService : IEngineService
    {
        private readonly IEngineRepository _engineRepository;

        public EngineService(IEngineRepository EngineRepository)
        {
            _engineRepository = EngineRepository;
        }

        public async Task<IEnumerable<EngineResponseDto>> GetEngineResponseDtosAsync()
        {
            var Engines = await _engineRepository.GetEnginesAsync();

            return Engines.Select(g => new EngineResponseDto
                {
                    EngineId = g.EngineId,
                    EngineName = g.EngineName,
                    IsOpenSource = g.IsOpenSource
                }
            );
        }

        public async Task<EngineResponseDto> GetEngineByIdAsync(int id)
        {
            var engine = await _engineRepository.GetByIdAsync(id);

            if(engine == null)
            {
                throw new KeyNotFoundException("Engine not found");
            }

            return new EngineResponseDto
            {
                EngineId = engine.EngineId,
                EngineName = engine.EngineName,
                IsOpenSource = engine.IsOpenSource
            };
        }

        public async Task<EngineResponseDto> AddEngineAsync(EngineRequestDto dto)
        {
            var engine = new Engine
            {
              EngineName = dto.EngineName, 
              IsOpenSource = dto.IsOpenSource 
            };
            await _engineRepository.AddAsync(engine);

            return new EngineResponseDto
            {
                EngineId = engine.EngineId,
                EngineName = engine.EngineName,
                IsOpenSource = engine.IsOpenSource
            };
        }

        public async Task UpdateEngineAsync(int id, EngineRequestDto dto)
        {
            var engine = await _engineRepository.GetByIdAsync(id);

            if(engine == null)
            {
                throw new KeyNotFoundException("Engine not found");
            }
            engine.EngineName = dto.EngineName;
            engine.IsOpenSource = dto.IsOpenSource;
            await _engineRepository.UpdateAsync(engine);
        }

        public async Task DeleteEngineAsync(int id)
        {
            var engine = await _engineRepository.GetByIdAsync(id);

            if(engine == null)
            {
                throw new KeyNotFoundException("Engine not found");
            }
            await _engineRepository.DeleteAsync(id);
        }

    }
}