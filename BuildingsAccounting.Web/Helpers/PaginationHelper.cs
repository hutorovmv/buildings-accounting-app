using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuildingsAccounting.Web.Pagination;

namespace BuildingsAccounting.Web.Helpers
{
    public static class PaginationHelper
    {
        public static MvcHtmlString Pagination(this HtmlHelper html, IPagedList list)
        {
            var pagination = new TagBuilder("ul");
            pagination.AddCssClass("pagination");

            if (list.HavePreviousPage)
            {
                var prev = new TagBuilder("li");
                prev.AddCssClass("page-item");
                prev.AddCssClass("page-item-prev");

                var prevInner = new TagBuilder("span");
                prevInner.AddCssClass("page-link");
                prevInner.InnerHtml = "<i class='fas fa-chevron-left'></i>";

                prev.InnerHtml = prevInner.ToString();
                pagination.InnerHtml += prev.ToString();
            }

            for (int i = 1; i < list.TotalPages; i++)
            {
                var item = new TagBuilder("li");
                item.AddCssClass("page-item");            
                if (i == list.PageIndex)
                    item.AddCssClass("active");

                var itemInner = new TagBuilder("span");
                itemInner.AddCssClass("page-link");
                itemInner.InnerHtml = i.ToString();

                item.InnerHtml = itemInner.ToString();
                pagination.InnerHtml += item.ToString();
            }

            if (list.HaveNextPage)
            {
                var next = new TagBuilder("li");
                next.AddCssClass("page-item");
                next.AddCssClass("page-item-next");

                var nextInner = new TagBuilder("span");
                nextInner.AddCssClass("page-link");
                nextInner.InnerHtml = "<i class='fas fa-chevron-right'></i>";

                next.InnerHtml = nextInner.ToString();
                pagination.InnerHtml += next.ToString();
            }

            return MvcHtmlString.Create(pagination.ToString());
        }
    }
}