using System.Collections;
using System.Collections.Generic;
using ApplicationCore.Models.Response;
using ApplicationCore.ServiceInterfaces;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;

namespace Infrastructure.Services
{
    public class CastService : ICastService
    {
        private readonly IAsyncRepository<Cast> _castRepository;

        public CastService(IAsyncRepository<Cast> castRepository)
        {
            _castRepository = castRepository;
        }
        public async Task<List<CastDetailResponseModel>> GetCastDetailsWithMovies(int id)
        {
            var cast = await _castRepository.GetByIdAsync(id);
            var castMovies = new List<MovieResponseModel>();
            foreach (var movie in cast.MovieCasts)
            {
                castMovies.Add(new MovieResponseModel()
                {
                    Id = movie.MovieId,
                    PosterUrl = movie.Movie.PosterUrl,
                    Title = movie.Movie.Title
                });
            }

            var result = new List<CastDetailResponseModel>();
            result.Add(new CastDetailResponseModel
            {
                Id = cast.Id,
                Name = cast.Name,
                Gender = cast.Gender,
                TmdbUrl = cast.TmdbUrl,
                ProfilePath = cast.ProfilePath,
                Movies = castMovies
            });

            return result;
        }
    }
}