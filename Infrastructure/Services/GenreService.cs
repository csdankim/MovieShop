using ApplicationCore.Models.Response;
using ApplicationCore.ServiceInterfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;

namespace Infrastructure.Services
{
    public class GenreService : IGenreService
    {
        private readonly IAsyncRepository<Genre> _genreRepository;

        public GenreService(IAsyncRepository<Genre> genreRepository)
        {
            _genreRepository = genreRepository;
        }
        public async Task<IEnumerable<GenreModel>> GetAllGenres()
        {
            var genres = await _genreRepository.ListAllAsync();
            var genresModel = new List<GenreModel>();
            foreach (var genre in genres)
            {
                genresModel.Add(new GenreModel
                {
                    Id = genre.Id, Name = genre.Name
                });
            }

            return genresModel.OrderBy(g => g.Name);
        }
    }
}