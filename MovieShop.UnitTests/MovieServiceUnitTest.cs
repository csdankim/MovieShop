using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models.Response;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MovieShop.UnitTests
{
    [TestClass]
    public class MovieServiceUnitTest
    {
        private MovieService _sut;
        private static List<Movie> _movies;
        private Mock<IMovieRepository> _mockMovieRepository;

        [TestInitialize]
        // [OneTimeSetup] in nUnit
        public void OneTimeSetup()
        {
            _mockMovieRepository = new Mock<IMovieRepository>();
            _mockMovieRepository.Setup(m => m.GetTop30HighestGrossingMovies()).ReturnsAsync(_movies);

            // SUT System under Test MovieService ==> Get30HighestGrossing
            _sut = new MovieService(_mockMovieRepository.Object);
        }

        [ClassInitialize]
        public static void SetUp(TestContext context)
        {
            _movies = new List<Movie>
            {
                new Movie {Id=1, Title="Avengers: Infinity War", Budget = 1200000},
                new Movie {Id=2, Title="Avatar", Budget = 1200000},
                new Movie {Id=3, Title="Star Wars: The Force Awakens", Budget = 1200000},
                new Movie {Id=4, Title="Titanic", Budget = 1200000},
                new Movie {Id=5, Title="Inception", Budget = 1200000},
                new Movie {Id=6, Title="Avengers: Age of Ultron", Budget = 1200000},
                new Movie {Id=7, Title="Interstellar", Budget = 1200000},
                new Movie {Id=8, Title="Fight Club", Budget = 1200000},
                new Movie {Id=9, Title="The Lord of the Rings: The Fellowship of the Ring", Budget = 1200000},
                new Movie {Id=10, Title="The Dark Knight", Budget = 1200000},
                new Movie {Id=11, Title="The Hunger Games", Budget = 1200000},
                new Movie {Id=12, Title="Django Unchained", Budget = 1200000},
                new Movie {Id=13, Title="The Lord of the Rings: The Return of the King", Budget = 1200000},
                new Movie {Id=14, Title="Harry Potter and the Philosopher's Stone", Budget = 1200000},
                new Movie {Id=15, Title="Iron Man", Budget = 1200000},
                new Movie {Id=16, Title="Furious 7", Budget = 1200000}
            };
        }

        [TestMethod]
        public async Task TestListOfGetTop30HighestGrossingMoviesFromFakeData()  // descriptive method name
        {
            // SUT System under Test MovieService ==> Get30HighestGrossing

            // Arrange: Initialize objects, creates mocks with arguments that are passed to the method under test and adds expectations.
            // mock objects, data, methods etc.
            //_sut = new MovieService(new MockMovieRepository());

            // Act: Invokes the method or property under test with the arranged parameters.
            var movies = await _sut.Get30HighestGrossing();

            // check the actual output with expected data.
            // AAA
            // Arrange, Act and Assert
            
            // Assert: Verifies that the action of the method under test behaves as expected.
            // don't do more than 2 or 3
            Assert.IsNotNull(movies);
            Assert.IsInstanceOfType(movies, typeof(IEnumerable<MovieCardResponseModel>));
            Assert.AreEqual(16, movies.Count());
        }
    }


    /*public class MockMovieRepository:IMovieRepository
    {
        public async Task<Movie> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Movie>> ListAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Movie>> ListAsync(Expression<Func<Movie, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetCountAsync(Expression<Func<Movie, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> GetExistAsync(Expression<Func<Movie, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public async Task<Movie> AddAsync(Movie entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Movie> UpdateAsync(Movie entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Movie entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Movie>> GetTop30HighestGrossingMovies()
        {
            // this method will get the fake data
            var _movies = new List<Movie>
            {
                new Movie {Id=1, Title="Avengers: Infinity War", Budget = 1200000},
                new Movie {Id=2, Title="Avatar", Budget = 1200000},
                new Movie {Id=3, Title="Star Wars: The Force Awakens", Budget = 1200000},
                new Movie {Id=4, Title="Titanic", Budget = 1200000},
                new Movie {Id=5, Title="Inception", Budget = 1200000},
                new Movie {Id=6, Title="Avengers: Age of Ultron", Budget = 1200000},
                new Movie {Id=7, Title="Interstellar", Budget = 1200000},
                new Movie {Id=8, Title="Fight Club", Budget = 1200000},
                new Movie {Id=9, Title="The Lord of the Rings: The Fellowship of the Ring", Budget = 1200000},
                new Movie {Id=10, Title="The Dark Knight", Budget = 1200000},
                new Movie {Id=11, Title="The Hunger Games", Budget = 1200000},
                new Movie {Id=12, Title="Django Unchained", Budget = 1200000},
                new Movie {Id=13, Title="The Lord of the Rings: The Return of the King", Budget = 1200000},
                new Movie {Id=14, Title="Harry Potter and the Philosopher's Stone", Budget = 1200000},
                new Movie {Id=15, Title="Iron Man", Budget = 1200000},
                new Movie {Id=16, Title="Furious 7", Budget = 1200000}
            };

            return _movies;
        }

        public async Task<IEnumerable<Movie>> GetMoviesByGenre(int id, int pageSize = 10, int page = 1)
        {
            throw new NotImplementedException();
        }
    }*/
}
