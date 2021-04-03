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
        Task<IEnumerable<ReviewMovieResponseModel>> GetReviewsForMovie(int id);
    }
}