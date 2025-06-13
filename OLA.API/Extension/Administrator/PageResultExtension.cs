using System;
using System.Linq;
using System.Collections.Generic;
using OLA.API.Models.Common;

namespace OLA.API.Extension.Administrator
{
	public static class PageResultExtension
	{
        public static PagedResult<T> ToPagedList<T>(this IEnumerable<T> query, int page, int limit)
        {
            var result = new PagedResult<T>(page, limit, query.Count());

            result.Data = query.Skip((page - 1) * limit).Take(limit).ToList();

            return result;
        }
    }
}

