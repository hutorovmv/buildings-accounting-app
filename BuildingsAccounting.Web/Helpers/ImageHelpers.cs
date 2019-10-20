using System.IO;
using System.Web;
using System.Web.Mvc;

namespace BuildingsAccounting.Web.Helpers
{
    public static class ImageHelpers
    {
        public static MvcHtmlString Image(this HtmlHelper html, string name, string classes = null)
        {
            string path = (string)HttpContext.Current.Application["ImagesPath"];
            string defaultImg = (string)HttpContext.Current.Application["DefaultImage"];

            TagBuilder img = new TagBuilder("img");

            if (File.Exists(HttpContext.Current.Server.MapPath(path + name)))
                img.Attributes.Add("src", path + name);
            else
                img.Attributes.Add("src", path + defaultImg);

            if (!string.IsNullOrWhiteSpace(classes))
                foreach (var i in classes.Split(' ')) 
                    img.AddCssClass(i);

            return MvcHtmlString.Create(img.ToString(TagRenderMode.SelfClosing));
        }
    }
}