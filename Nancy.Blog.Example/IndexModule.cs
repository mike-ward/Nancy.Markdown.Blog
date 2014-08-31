using System;
using System.Globalization;
using System.Linq;
using Nancy.Blog.Example.Models;
using Nancy.Responses.Negotiation;

namespace Nancy.Blog.Example
{
    public class IndexModule : NancyModule
    {
        public IndexModule(IndexModel model)
        {
            Get["/"] = p => ShowBlog(model, 0);
            Get["/{page:int}"] = p => ShowBlog(model, p.page);
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

        private Negotiator ShowBlog(IndexModel model, int page)
        {
            const int pageLength = 3;
            Context.ViewBag.PageLength = pageLength;
            Context.ViewBag.Page = page;
            Context.ViewBag.Prev = Math.Max(page - pageLength, 0);
            Context.ViewBag.Next = Math.Min(model.Blog.Posts.Count() - 1, page + pageLength);
            return View[model];
        }
    }
}