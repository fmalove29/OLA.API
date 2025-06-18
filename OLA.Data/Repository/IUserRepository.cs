using OLA.Data.Models.User;
using OLA.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace OLA.Data.Repository;

public interface IUserRepository
{
    public Task<AppUser> GetByIdAsync(string id); 
    public Task<IEnumerable<AppUser>> GetAllAsync();
    public Task<CreateUserResult> CreateAsync(AppUser user, string password);

    public Task<IEnumerable<string>> GetRolesAsync(AppUser user);
    public Task UpdateAsync(AppUser user);
    public Task<AppUser> Login(AppUser appUser, string Password);

    public Task<AppUser> FindUserByEmail(string Email);

    public Task<string> GetUserIdByEmail(string Email);

    public Task DeleteAsync(AppUser user);
    public Task AddRoles(AppUser appUser,string role);

    public Task<bool> CheckRoleExist(string role);
    public Task<bool> IsAdmin(AppUser appUser);
    public Task<IdentityResult> RemoveRole(AppUser appUser, string role);
    public Task<bool> IsInRole(AppUser appUser, string role);
    public DbSet<AppUser> GetDbSet();
}
