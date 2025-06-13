using System;
using OLA.API.Models.response.user;
using OLA.Data.Models.User;

namespace OLA.API.Extension.Account
{
	public static class AccountExtension
	{
		public static AppUserResponse ToAccountResponse(this AppUser appUser)
		=> new AppUserResponse
		{
            Id = appUser.Id,
            FirstName = appUser.FirstName,
            LastName = appUser.LastName,
            MiddleName = appUser.MiddleName,
            UserName = appUser.UserName,
            PhoneNumber = appUser.PhoneNumber,
            Email = appUser.Email,
            Families = appUser.Families?.Select(f => new FamilyResponse
            {
                Id = f.Id,
                DateOfBrith = f.DateOfBrith,
                ContactNumber = f.ContactNumber,
                IsEmergencyContact = f.IsEmergencyContact,
                Name = f.Name,
                RelationshipType = (OLA.API.Models.RelationshipType)f.RelationType,
                RelationshipTypeName = f.RelationType.ToString(),
                Address = f.Address != null ? new AddressResponse
                {
                    AppUserId = f.Address.AppUserId,
                    Id = f.Address.Id,
                    Barangay = f.Address.Barangay,
                    City = f.Address.City,
                    Purok = f.Address.Purok
                } : null
            }).ToList(),

            Addresses = appUser.Addresses?.Select(a => new AddressResponse
            {
                Id = a.Id,
                AppUserId = a.AppUserId,
                Barangay = a.Barangay,
                City = a.City,
                Purok = a.Purok
            }).ToList()
        };
	}
}

