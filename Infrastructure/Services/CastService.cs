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
        public async Task<CastDetailResponseModel> GetCastDetailsWithMovies(int id)
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

            CastDetailResponseModel castDetailResponseModel = new CastDetailResponseModel();
            var response = castDetailResponseModel;
            response.Id = cast.Id;
            response.Name = cast.Name;
            response.Gender = cast.Gender;
            response.TmdbUrl = cast.TmdbUrl;
            response.ProfilePath = cast.ProfilePath;
            response.Movies = castMovies;

            return response;
        }
    }
}