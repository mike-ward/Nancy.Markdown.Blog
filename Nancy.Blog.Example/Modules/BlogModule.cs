using System;
using System.Globalization;
using System.Linq;
using Nancy.Markdown.Blog.Example.Models;
using Nancy.Responses.Negotiation;

namespace Nancy.Markdown.Blog.Example
{
    public class BlogModule : NancyModule
    {
        public BlogModule(IBlogModel model)
        {
            Get["blog/"] = p => ShowBlog(model, 0);
            Get["blog/posts/{index:int}"] = p => ShowBlog(model, p.index) ?? 404;
            Get["blog/post/{year:int}/{month:int}/{day:int}/{slug}"] = p => ShowArticle(model, p.year, p.month, p.day, p.slug) ?? 404;
            Get["blog/archive"] = p => ShowArchive(model);
            Get["blog/rss"] = p => model.Blog.Rss();
        }

        private Negotiator ShowBlog(IBlogModel model, int index)
        {
            const int numberOfPosts = 3;
            var posts = model.Blog.Posts.Skip(index).Take(numberOfPosts);
            if (!posts.Any()) return null;
            Func<int, string> link = p => string.Format("{0}/posts/{1}", model.Blog.BaseUri, p);
            ViewBag.Title = model.Blog.Title;
            Context.ViewBag.Posts = posts;
            var prev = index - numberOfPosts;
            var next = index + numberOfPosts;
            Context.ViewBag.Prev = prev >= 0 ? link(prev) : string.Empty;
            Context.ViewBag.Next = next < model.Blog.Posts.Count() ? link(next) : string.Empty;
            Context.ViewBag.PrevVisible = string.IsNullOrEmpty(Context.ViewBag.Prev) ? "hidden" : "visible";
            Context.ViewBag.NextVisible = string.IsNullOrEmpty(Context.ViewBag.Next) ? "hidden" : "visible";
            return View[model.Blog];
        }

        private Negotiator ShowArticle(IBlogModel model, int year, int month, int day, string slug)
        {
            var post = model.Blog.Posts
                .FirstOrDefault(p =>
                    year == p.Created.Year &&
                    month == p.Created.Month &&
                    day == p.Created.Day &&
                    slug.Equals(p.Slug, StringComparison.InvariantCultureIgnoreCase));

            if (post == null) return null;
            var prev = model.Blog.PreviousPost(post);
            var next = model.Blog.NextPost(post);
            ViewBag.Title = post.Title;
            Context.ViewBag.Posts = new[] {post};
            Context.ViewBag.Prev = prev != null ? prev.PermaLink : string.Empty;
            Context.ViewBag.Next = next != null ? next.PermaLink : string.Empty;
            Context.ViewBag.PrevVisible = string.IsNullOrEmpty(Context.ViewBag.Prev) ? "hidden" : "visible";
            Context.ViewBag.NextVisible = string.IsNullOrEmpty(Context.ViewBag.Next) ? "hidden" : "visible";
            return View[model.Blog];
        }

        private Negotiator ShowArchive(IBlogModel model)
        {
            return View["archive", model.Blog.Posts
                .GroupBy(post => post.Created.Year)
                .ToDictionary(yg => yg.Key, yg => yg
                    .GroupBy(mg => mg.Created.Month)
                    .OrderByDescending(mg => mg.Key)
                    .ToDictionary(
                        mg => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(mg.Key),
                        mg => mg.OrderByDescending(d => d.Created)))
                ];
        }
    }
}