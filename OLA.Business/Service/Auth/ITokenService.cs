using OLA.Data.Models.User;
namespace OLA.Business.Service.Auth;


public interface ITokenService
{
    Task<string> CreateToken(AppUser appUser);
}
