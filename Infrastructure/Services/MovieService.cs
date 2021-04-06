using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models.Request;
using ApplicationCore.Models.Response;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Infrastructure.Repositories;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        // no of methods : depends on business requirement
        public async Task<List<MovieCardResponseModel>> Get30HighestGrossing()
        {
            /*var movies = new List<MovieCardResponseModel>
            {
                new MovieCardResponseModel{Id = 1, Title = "Avengers: Infinity War"},
                new MovieCardResponseModel {Id = 2, Title = "Avatar"},
                new MovieCardResponseModel {Id = 3, Title = "Star Wars: The Force Awakens"},
                new MovieCardResponseModel {Id = 4, Title = "Titanic"},
                new MovieCardResponseModel {Id = 5, Title = "Inception"},
                new MovieCardResponseModel {Id = 6, Title = "Avengers: Age of Ultron"},
                new MovieCardResponseModel {Id = 7, Title = "Interstellar"},
                new MovieCardResponseModel {Id = 8, Title = "Fight Club"}
            };*/

            var movies = await _movieRepository.GetTop30HighestGrossingMovies();
            var result = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                result.Add(new MovieCardResponseModel
                {
                    Id = movie.Id, Title = movie.Title, PosterUrl = movie.PosterUrl
                });
            }

            return result;
        }

        public void CreateMovie(MovieCreateRequestModel model)
        {
            //take model and convert it to Movie Entity and send it to repository
            //if repository saves successfully return true/id:2

        }

        public async Task<MovieDetailResponseModel> GetMovieAsync(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            
            var favoriteCount = await _movieRepository.GetCountAsync(f=>f.Id == id);
            
            var castList = new List<MovieDetailResponseModel.CastResponseModel>();
            foreach (var cast in movie.MovieCasts)
            {
                castList.Add(new MovieDetailResponseModel.CastResponseModel
                {
                    Id = cast.CastId,
                    Gender = cast.Cast.Gender,
                    Name = cast.Cast.Name,
                    ProfilePath = cast.Cast.ProfilePath,
                    TmdbUrl = cast.Cast.TmdbUrl,
                    Character = cast.Character
                });
            }

            var genreList = new List<GenreModel>();
            foreach (var genre in movie.Genres)
            {
                genreList.Add(new GenreModel
                {
                    Id = genre.Id,
                    Name = genre.Name
                });
            }

            MovieDetailResponseModel movieDetailResponseModel = new MovieDetailResponseModel();
            var response = movieDetailResponseModel;
            response.Id = movie.Id;
            response.Title = movie.Title;
            response.PosterUrl = movie.PosterUrl;
            response.BackdropUrl = movie.BackdropUrl;
            response.Rating = movie.Rating;
            response.Overview = movie.Overview;
            response.Tagline = movie.Tagline;
            response.Budget = movie.Budget;
            response.Revenue = movie.Revenue;
            response.ImdbUrl = movie.ImdbUrl;
            response.TmdbUrl = movie.TmdbUrl;
            response.ReleaseDate = movie.ReleaseDate;
            response.RunTime = movie.RunTime;
            response.Price = movie.Price;
            response.FavoritesCount = favoriteCount;
            response.Casts = castList;
            response.Genres = genreList;

            return response;
        }

        public async Task<IEnumerable<MovieResponseModel>> GetMoviesByGenre(int genreId, int pageSize = 10, int page = 1)
        {
            var movies = await _movieRepository.GetMoviesByGenre(genreId, pageSize, page);
            var response = new List<MovieResponseModel>();
            foreach (var movie in movies)
            {
                response.Add(new MovieResponseModel
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    PosterUrl = movie.PosterUrl,
                    ReleaseDate = movie.ReleaseDate
                });
            }

            return response;
        }
    }

/*    public class MovieServiceTest : IMovieService
    {
        public List<MovieCardResponseModel> Get30HighestGrossing()
        {
            var movies = new List<MovieCardResponseModel>
            {
                new MovieCardResponseModel{Id = 1, Title = "Avengers: Infinity War"},
                new MovieCardResponseModel {Id = 2, Title = "Avatar"},
                new MovieCardResponseModel {Id = 3, Title = "Star Wars: The Force Awakens"},
                new MovieCardResponseModel {Id = 4, Title = "Titanic"},
                new MovieCardResponseModel {Id = 5, Title = "Inception"},
                new MovieCardResponseModel {Id = 6, Title = "Avengers: Age of Ultron"},
                new MovieCardResponseModel {Id = 7, Title = "Interstellar"},
                new MovieCardResponseModel {Id = 8, Title = "Fight Club"}
            };

            return movies;
        }

        public void CreateMovie(MovieCreateRequestModel model)
        {
            //take model and convert it to Movie Entity and send it to repository
            //if repository saves successfully return true/id:2

        }
    }*/
}
