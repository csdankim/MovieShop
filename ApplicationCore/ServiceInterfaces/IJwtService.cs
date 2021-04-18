using ApplicationCore.Models.Response;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IJwtService
    {
        string GenerateToken(LoginResponseModel model); 
    }
}