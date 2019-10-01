using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuildingsAccounting.Web.Pagination
{
    public static class PagedListExtensions
    {
        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> items, int pageSize, int pageIndex)
        {
            return new PagedList<T>(items, pageSize, pageIndex);
        }
    }
}