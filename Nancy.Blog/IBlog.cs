using System.Collections.Generic;

namespace Nancy.Blog
{
    public interface IBlog
    {
        string Title { get; set; }
        IEnumerable<Post> Posts { get; set; }
        RssResponse Rss();
    }
}