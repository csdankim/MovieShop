using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Models.Response;

namespace ApplicationCore.ServiceInterfaces
{
    public interface ICastService
    {
        Task<CastDetailResponseModel> GetCastDetailsWithMovies(int id);
    }
}