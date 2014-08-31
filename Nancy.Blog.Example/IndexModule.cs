using System.Globalization;
using System.Linq;
using Nancy.Blog.Example.Models;

namespace Nancy.Blog.Example
{
    public class IndexModule : NancyModule
    {
        public IndexModule(IndexModel model)
        {
            Get["/"] = p => View[model];
            Get["/rss"] = p => model.Blog.Rss();

            Get["/archive"] = p => View["archive", model.Blog.Posts
                .GroupBy(post => post.Created.Year)
                .ToDictionary(yg => yg.Key, yg => yg
                    .GroupBy(mg => mg.Created.Month)
                    .OrderBy(mg => mg.Key)
                    .ToDictionary(
                        mg => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(mg.Key),
                        mg => mg.OrderBy(d => d.Created)))
                ];
        }
    }
}