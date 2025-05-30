using OLA.Data.Models.User;
using OLA.Business.Models.response;
namespace OLA.Business.Service.Auth;

public interface IAuthService
{
    Task<CreateUserResult> CreateUser(AppUser appUser, string Password);
    Task CreateAddress(Address address);
    Task AddFamilies(Family family);
    Task<AppUser> Login(AppUser appUser, string password);
    Task<AppUser> GetByIdAsync(string Id);
    Task<bool> SaveChangesAsync(Guid userId);
    Task<string> GetUserIdByEmail(string email);
    Task AssignRole(AppUser appUser, string role);
    Task<bool> CheckRoleExist(string role);
}
