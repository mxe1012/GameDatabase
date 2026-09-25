using GameDatabase.DTOs;
using GameDatabase.Entites;
using GameDatabase.Repositories;

namespace GameDatabase.Services
{
    public class DeveloperService: IDeveloperService
    {
        private readonly IDeveloperRepository _developerRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeveloperService(IDeveloperRepository developerRepository, IHttpContextAccessor httpContextAccessor)
        {
            _developerRepository = developerRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<DeveloperResponseDto>> GetDeveloperResponseDtosAsync(bool includeDeleted)
        {
            var developers = await _developerRepository.GetDevelopersAsync(includeDeleted);
            
            return developers.Select(p => new DeveloperResponseDto
                {
                    DeveloperId = p.DeveloperId,
                    DeveloperName = p.DeveloperName,
                    City = p.City,
                    State = p.State,
                    CountryCode = p.CountryCode,
                    YearFounded = p.YearFounded,
                    IsActive = p.IsActive,
                    CreatedBy = p.CreatedBy,
                    IsDeleted = p.IsDeleted,
                    DeletedBy= p.DeletedBy,
                    DeletedAt = p.DeletedAt
                }
            );
        }

        public async Task<DeveloperResponseDto> GetDeveloperByIdAsync(int id, bool isAdmin)
        {
            var developer = await _developerRepository.GetByIdAsync(id, isAdmin);

            if(developer == null)
            {
                throw new KeyNotFoundException("Developer not found");
            }

            return new DeveloperResponseDto
            {
                DeveloperId = developer.DeveloperId,
                DeveloperName = developer.DeveloperName,
                City = developer.City,
                State = developer.State,
                CountryCode = developer.CountryCode,
                YearFounded = developer.YearFounded,
                IsActive = developer.IsActive,
                CreatedBy = developer.CreatedBy,
                IsDeleted = developer.IsDeleted,
                DeletedBy = developer.DeletedBy,
                DeletedAt = developer.DeletedAt
            };
        }

        public async Task<DeveloperResponseDto> AddDeveloperAsync(DeveloperRequestDto dto)
        {   
            var developer = new Developer
            {
                DeveloperName = dto.DeveloperName,
                City = dto.City,
                State = dto.State,
                CountryCode = dto.CountryCode,
                YearFounded = dto.YearFounded,
                IsActive = dto.IsActive,
                CreatedBy = _httpContextAccessor.HttpContext?.User?.Identity?.Name,
            };

            await _developerRepository.AddAsync(developer);

            return new DeveloperResponseDto
            {
                DeveloperId = developer.DeveloperId,
                DeveloperName = developer.DeveloperName,
                City = developer.City,
                State = developer.State,
                CountryCode = developer.CountryCode,
                YearFounded = developer.YearFounded,
                IsActive = developer.IsActive,
                CreatedBy = developer.CreatedBy,
                IsDeleted = developer.IsDeleted,
                DeletedBy = developer.DeletedBy,
                DeletedAt = developer.DeletedAt
            };
        }

        public async Task UpdateDeveloperAsync(int id, DeveloperRequestDto dto)
        {
            var developer = await _developerRepository.GetByIdAsync(id, false);

            if(developer == null)
            {
                throw new KeyNotFoundException("Developer not found");
            }
            developer.DeveloperName = dto.DeveloperName;
            developer.City = dto.City;
            developer.State = dto.State;
            developer.CountryCode = dto.CountryCode;
            developer.YearFounded = dto.YearFounded;
            developer.IsActive = dto.IsActive;
            await _developerRepository.UpdateAsync(developer);
        }

        public async Task DeleteDeveloperAsync(int id, bool isHardDelete)
        {
            var developer = await _developerRepository.GetByIdAsync(id, false);

            if(developer == null)
            {
                throw new KeyNotFoundException("Developer not found");
            }

            var deletedBy = _httpContextAccessor.HttpContext?.User?.Identity?.Name;

            await _developerRepository.DeleteAsync(id, deletedBy, isHardDelete);
        }

    }
}