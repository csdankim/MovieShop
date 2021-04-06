using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Models.Request;
using ApplicationCore.Models.Response;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IMovieService
    {
        Task<List<MovieCardResponseModel>> Get30HighestGrossing();
        void CreateMovie(MovieCreateRequestModel model);
        Task<MovieDetailResponseModel> GetMovieAsync(int id);
        Task<IEnumerable<MovieResponseModel>> GetMoviesByGenre(int Id, int pageSize = 10, int page = 1);
    }
}