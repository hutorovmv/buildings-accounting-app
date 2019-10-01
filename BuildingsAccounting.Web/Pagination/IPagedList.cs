using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingsAccounting.Web.Pagination
{
    public interface IPagedList
    {
        int TotalPages { get; }
        int TotalCount { get; }
        int PageIndex { get; }
        int PageSize { get; }
        bool HavePreviousPage { get; }
        bool HaveNextPage { get; }
    }
}
