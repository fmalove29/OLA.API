using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
namespace OLA.Data.DataContext
{
	public interface IApplicationDbContext
	{
		public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        public int SaveChanges();
    }
}

