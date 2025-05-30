
using OLA.Data.Models.User;
using OLA.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;


namespace OLA.Data.Repository;

public class UserRepository : IUserRepository
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    public UserRepository(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public async Task<CreateUserResult> CreateAsync(AppUser user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);

        return new CreateUserResult
        {
            Succeeded = result.Succeeded,
            Errors = result.Errors.Select(e => e.Description).ToList()
        };
        
    }

    public async Task DeleteAsync(AppUser user)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<AppUser>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<AppUser> GetByIdAsync(string id)
    {
        var appUser = await _userManager.FindByIdAsync(id);
        return appUser;
    }

    public async Task<bool> SaveChangesAsync()
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(AppUser user)
    {
        throw new NotImplementedException();
    }
    public async Task<IEnumerable<string>> GetRolesAsync(AppUser appUser)
    {
        return await _userManager.GetRolesAsync(appUser);
    }
    public async Task<AppUser?> Login(AppUser appUser, string password)
    {
        var user = await _userManager.FindByEmailAsync(appUser.Email);

        if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            return null; // Or throw new UnauthorizedAccessException();

        return user;
    }

    public async Task<AppUser> FindUserByEmail(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<string> GetUserIdByEmail(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user?.Id;
    }

    public async Task AddRoles(AppUser appUser, string inputRole)
    {
        await _userManager.AddToRoleAsync(appUser, inputRole);
    }

    public async Task<bool> CheckRoleExist(string inputRole)
    {
        var roleExists = await _roleManager.RoleExistsAsync(inputRole);

        return roleExists;
    }
}
