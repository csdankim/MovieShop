using System.Collections.Generic;
using ApplicationCore.Models.Request;
using ApplicationCore.Models.Response;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IMovieService
    {
        List<MovieCardResponseModel> Get30HighestGrossing();
        void CreateMovie(MovieCreateRequestModel model);
    }
}