using System;
using OLA.Data.Repository;
using OLA.Data.Models.User;
using OLA.Business.Models.response;

namespace OLA.Business.Service.Auth
{
	public class AuthService : IAuthService
	{
		private readonly IUserRepository _userRepository;
		private readonly IRepository<Address> _repository;
		private readonly IRepository<Family> _repoFamily;
		public AuthService(IUserRepository userRepository, IRepository<Address> repository, IRepository<Family> repoFamily)
		{
			_userRepository = userRepository;
			_repository = repository;
			_repoFamily = repoFamily;
		}

		public async Task<CreateUserResult> CreateUser(AppUser appUser, string password)
		{
			var result = await _userRepository.CreateAsync(appUser, password);

			return new CreateUserResult
			{
				Succeeded = result.Succeeded,
				Errors = result.Errors.ToList()
			};
		}

		public async Task CreateAddress(Address address)
		{
			await _repository.Add(address);
		}
		public async Task<AppUser> GetByIdAsync(string Id)
		{
			var user = await _userRepository.GetByIdAsync(Id);
			return user;
		}
		public async Task<AppUser> Login(AppUser appUser , string password)
		{
			var user =  await _userRepository.Login(appUser, password);
			return user;
		}
		public async Task<bool> SaveChangesAsync(Guid userId)
		{
			return await _repository.SaveChangesAsync(userId);
		}
		public async Task<string> GetUserIdByEmail(string email)
		{
			return await _userRepository.GetUserIdByEmail(email);
		}
		public async Task AddFamilies(Family family)
		{
			await _repoFamily.Add(family);
		}

		public async Task AssignRole(AppUser appUser , string role)
		{
			await _userRepository.AddRoles(appUser, role);
		}
		public async Task<bool> CheckRoleExist(string role)
		{
			return await _userRepository.CheckRoleExist(role);
		} 
	}
}

