using Microsoft.EntityFrameworkCore;
using OLA.Data.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLA.Business.Service.Account
{
    public interface IAddressService
    {
        Task<Address> GetByIdAsync(Guid Id);
        Task Add(Address address);
        Task Update(Address address);
        Task<DbSet<Address>> GetDbSet();
        Task<bool> SaveChangesAsync(Guid Id);
    }
}
