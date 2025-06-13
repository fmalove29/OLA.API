using System;
using Microsoft.EntityFrameworkCore;
using OLA.Data.Models.Administrator;
using System.Linq.Expressions;

namespace OLA.Business.Service.Administrator
{
	public interface IAccessService
	{
        Task<DbSet<Access>> GetDbSet();
        Task<IEnumerable<Access>> GetAll();
        Task Add(Access access);
        Task Update(Access access);
        Task<Access> GetById(Guid Id);
        Task Delete(Access access);
        Task SaveChangesAsync(Guid Id);
        Task<IEnumerable<Access>> GetByCondition(Expression<Func<Access, bool>> whereExp);
    }
}

