using GameDatabase.DTOs;
using GameDatabase.Entites;
using GameDatabase.Repositories;

namespace GameDatabase.Services
{
    public class EngineService : IEngineService
    {
        private readonly IEngineRepository _engineRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EngineService(IEngineRepository EngineRepository, IHttpContextAccessor httpContextAccessor)
        {
            _engineRepository = EngineRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<EngineResponseDto>> GetEngineResponseDtosAsync(bool includeDeleted)
        {
            var Engines = await _engineRepository.GetEnginesAsync(includeDeleted);

            return Engines.Select(g => new EngineResponseDto
                {
                    EngineId = g.EngineId,
                    EngineName = g.EngineName,
                    IsOpenSource = g.IsOpenSource,
                    CreatedBy = g.CreatedBy,
                    IsDeleted = g.IsDeleted,
                    DeletedBy = g.DeletedBy,
                    DeletedAt = g.DeletedAt
                }
            );
        }

        public async Task<EngineResponseDto> GetEngineByIdAsync(int id, bool isAdmin)
        {
            var engine = await _engineRepository.GetByIdAsync(id, isAdmin);

            if(engine == null)
            {
                throw new KeyNotFoundException("Engine not found");
            }

            return new EngineResponseDto
            {
                EngineId = engine.EngineId,
                EngineName = engine.EngineName,
                IsOpenSource = engine.IsOpenSource,
                CreatedBy = engine.CreatedBy,
                IsDeleted = engine.IsDeleted,
                DeletedBy = engine.DeletedBy,
                DeletedAt = engine.DeletedAt
            };
        }

        public async Task<EngineResponseDto> AddEngineAsync(EngineRequestDto dto)
        {
            var engine = new Engine
            {
              EngineName = dto.EngineName, 
              IsOpenSource = dto.IsOpenSource, 
              CreatedBy = _httpContextAccessor.HttpContext?.User?.Identity?.Name
            };
            await _engineRepository.AddAsync(engine);

            return new EngineResponseDto
            {
                EngineId = engine.EngineId,
                EngineName = engine.EngineName,
                IsOpenSource = engine.IsOpenSource,
                CreatedBy = engine.CreatedBy,
                IsDeleted = engine.IsDeleted,
                DeletedBy = engine.DeletedBy,
                DeletedAt = engine.DeletedAt
            };
        }

        public async Task UpdateEngineAsync(int id, EngineRequestDto dto)
        {
            var engine = await _engineRepository.GetByIdAsync(id, false);

            if(engine == null)
            {
                throw new KeyNotFoundException("Engine not found");
            }
            engine.EngineName = dto.EngineName;
            engine.IsOpenSource = dto.IsOpenSource;
            await _engineRepository.UpdateAsync(engine);
        }

        public async Task DeleteEngineAsync(int id, bool isHardDelete)
        {
            var engine = await _engineRepository.GetByIdAsync(id, false);

            if(engine == null)
            {
                throw new KeyNotFoundException("Engine not found");
            }

            var deletedBy = _httpContextAccessor.HttpContext?.User?.Identity?.Name;

            await _engineRepository.DeleteAsync(id, deletedBy, isHardDelete);
        }

    }
}