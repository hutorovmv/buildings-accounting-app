using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuildingsAccounting.Web.Pagination
{
    public class PagedList<T> : IPagedList
    {
        public int TotalPages { get; }
        public int TotalCount { get; }
        public int PageIndex { get; }
        public int PageSize { get; }
        public bool HavePreviousPage { get; }
        public bool HaveNextPage { get; }
        public IEnumerable<T> Items { get; }

        public PagedList(IEnumerable<T> items, int pageSize, int pageIndex)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;

            TotalCount = items.Count();
            TotalPages = TotalCount / PageSize;

            HavePreviousPage = PageIndex > 1;
            HaveNextPage = PageIndex <= TotalPages - 1;

            Items = items.Skip(PageSize * PageIndex - 1).Take(PageSize);
        }
    }
}