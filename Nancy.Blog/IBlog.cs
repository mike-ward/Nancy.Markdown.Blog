using System;
using System.Collections.Generic;

namespace Nancy.Markdown.Blog
{
    public interface IBlog
    {
        string Title { get; set; }
        string Author { get; set; }
        string Description { get; set; }
        Uri BaseUri { get; set; }
        string Copyright { get; set; }
        string Langauge { get; set; }
        IEnumerable<Post> Posts { get; set; }
        IEnumerable<Post> AllPosts { get; }
        Post PreviousPost(Post post);
        Post NextPost(Post post);
        RssResponse Rss();
        Func<Post, string> PermaLink { get; set; } 
    }
}