using Nancy.Blog.Example.Models;

namespace Nancy.Blog.Example
{
    using Nancy;

    public class IndexModule : NancyModule
    {
        public IndexModule(IndexModel model)
        {
            Get["/"] = parameters => View[model];
        }
    }
}