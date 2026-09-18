using GameDatabase.DTOs;
using GameDatabase.Entites;
using GameDatabase.Repositories;

namespace GameDatabase.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<IEnumerable<GenreResponseDto>> GetGenreResponseDtosAsync()
        {
            var genres = await _genreRepository.GetGenresAsync();

            return genres.Select(g => new GenreResponseDto
                {
                    GenreId = g.GenreId,
                    GenreName = g.GenreName,
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
            };
        }

        public async Task<GenreResponseDto> AddGenreAsync(GenreRequestDto dto)
        {
            var genre = new Genre
            {
              GenreName = dto.GenreName,  
            };
            await _genreRepository.AddAsync(genre);

            return new GenreResponseDto
            {
                GenreId = genre.GenreId,
                GenreName = genre.GenreName
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
            await _genreRepository.DeleteAsync(id);
        }

    }
}