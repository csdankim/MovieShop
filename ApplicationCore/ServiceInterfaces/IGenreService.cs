using ApplicationCore.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreModel>> GetAllGenres();
    }
}