using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Text;

namespace CavipetrolTestBack.Infrastructure.Utils
{
    public static class LinqExtensions
    {
        public static PagedResult<T> GetPaged<T>(this IEnumerable<T> query,
                                         int page, int pageSize) where T : class
        {
            var result = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = query.Count()
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            result.Results = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return result;
        }
    }
}
