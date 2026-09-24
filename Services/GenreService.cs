using GameDatabase.DTOs;
using GameDatabase.Entites;
using GameDatabase.Repositories;

namespace GameDatabase.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GenreService(IGenreRepository genreRepository, IHttpContextAccessor httpContextAccessor)
        {
            _genreRepository = genreRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<GenreResponseDto>> GetGenreResponseDtosAsync()
        {
            var genres = await _genreRepository.GetGenresAsync();

            return genres.Select(g => new GenreResponseDto
                {
                    GenreId = g.GenreId,
                    GenreName = g.GenreName,
                    CreatedBy = g.CreatedBy,
                    IsDeleted = g.IsDeleted,
                    DeletedBy = g.DeletedBy,
                    DeletedAt = g.DeletedAt
                }
            );
        }

        public async Task<GenreResponseDto> GetGenreByIdAsync(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);

            if(genre == null)
            {
                throw new KeyNotFoundException("Genre not found");
            }

            return new GenreResponseDto
            {
                GenreId = genre.GenreId,
                GenreName = genre.GenreName,
                CreatedBy = genre.CreatedBy,
                IsDeleted = genre.IsDeleted,
                DeletedBy = genre.DeletedBy,
                DeletedAt = genre.DeletedAt
            };
        }

        public async Task<GenreResponseDto> AddGenreAsync(GenreRequestDto dto)
        {
            var genre = new Genre
            {
              GenreName = dto.GenreName,
              CreatedBy = _httpContextAccessor.HttpContext?.User?.Identity?.Name  
            };
            await _genreRepository.AddAsync(genre);

            return new GenreResponseDto
            {
                GenreId = genre.GenreId,
                GenreName = genre.GenreName,
                CreatedBy = genre.CreatedBy,
                IsDeleted = genre.IsDeleted,
                DeletedBy = genre.DeletedBy,
                DeletedAt = genre.DeletedAt
            };
        }

        public async Task UpdateGenreAsync(int id, GenreRequestDto dto)
        {
            var genre = await _genreRepository.GetByIdAsync(id);

            if(genre == null)
            {
                throw new KeyNotFoundException("Genre not found");
            }
            genre.GenreName = dto.GenreName;
            await _genreRepository.UpdateAsync(genre);
        }

        public async Task DeleteGenreAsync(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);

            if(genre == null)
            {
                throw new KeyNotFoundException("Genre not found");
            }

            var deletedBy = _httpContextAccessor.HttpContext?.User?.Identity?.Name;

            await _genreRepository.DeleteAsync(id, deletedBy);
        }
        
    }
}