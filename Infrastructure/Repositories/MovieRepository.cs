using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MovieRepository: EfRepository<Movie>, IMovieRepository 
    {
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Movie>> GetTop30HighestGrossingMovies()
        {
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
            // skip, take
            // pagesize = 20
            // 1-20, 21-40, 41-60
            // 1 skip(pagenumber-1).take(20)
            // 2 skip() 20 (2-1)*pagesize
            // 3 skip(3-1)*pagesize
            return movies;
        }

        public override async Task<Movie> GetByIdAsync(int id)
        {
            var movie = await _dbContext.Movies.Include(m => m.MovieCasts).ThenInclude(m => m.Cast).Include(m => m.Genres)
                .FirstOrDefaultAsync(m => m.Id == id);

            var movieRating = await _dbContext.Reviews.Where(r => r.MovieId == id).DefaultIfEmpty()
                .AverageAsync(r => r == null ? 0 : r.Rating);
            if (movieRating > 0)
            {
                movie.Rating = movieRating;
            }

            return movie;
        }

        public async Task<IEnumerable<Movie>> GetMoviesByGenre(int Id, int pageSize = 10, int page = 1)
        {
            var movies = await _dbContext.Genres.Include(g => g.Movies).Where(g => g.Id == Id)
                .SelectMany(g => g.Movies).OrderByDescending(m => m.Revenue).Skip((page - 1) * pageSize).Take(pageSize)
                .ToListAsync();

            return movies;
        }




        /*public override Task<IEnumerable<Movie>> ListAsync(Expression<Func<Movie, bool>> filter)
        {
            var movies = _dbContext.Movies.Where(m => m.Revenue > 100000).ToListAsync();  //30
            var movies2 = _dbContext.Movies.ToList().Where(m => m.Revenue > 100000).ToList();
        }*/
    }
}
