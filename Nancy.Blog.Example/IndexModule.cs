using Nancy.Blog.Example.Models;

namespace Nancy.Blog.Example
{
    using Nancy;

    public class IndexModule : NancyModule
    {
        public IndexModule(IndexModel model)
        {
            Get["/"] = p => View[model];
            Get["/rss"] = p => model.Blog.Rss();
        }
    }
}