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
            Get["/"] = p => Response.AsRedirect("~/blog");
            Get["blog/"] = p => ShowBlog(model, 0);
            Get["blog/{index:int}"] = p => ShowBlog(model, p.index);
            Get["blog/{slug}"] = p => ShowArticle(model, p.slug);
            Get["blog/archive"] = p => ShowArchive(model);
            Get["blog/rss"] = p => model.Blog.Rss();
        }

        private Negotiator ShowBlog(IndexModel model, int index)
        {
            const int pageLength = 3;
            Context.ViewBag.Index = index;
            Context.ViewBag.PageLength = pageLength;
            Context.ViewBag.Prev = Math.Max(index - pageLength, 0);
            Context.ViewBag.Next = Math.Min(model.Blog.Posts.Count() - 1, index + pageLength);
            return View[model];
        }

        private Negotiator ShowArticle(IndexModel model, string slug)
        {
            const int pageLength = 1;
            Context.ViewBag.PageLength = pageLength;
            var index = model.Blog.IndexFromSlug(slug);
            Context.ViewBag.Index = index;
            Context.ViewBag.Prev = model.Blog.Posts.ElementAt(Math.Max(0, index - 1)).Slug;
            Context.ViewBag.Next = model.Blog.Posts.ElementAt(Math.Min(model.Blog.Posts.Count() - 1, index + 1)).Slug;
            return View[model];
        }

        private Negotiator ShowArchive(IndexModel model)
        {
            return View["archive", model.Blog.Posts
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