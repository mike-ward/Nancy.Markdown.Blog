using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel.Syndication;

namespace Nancy.Blog
{
    public class Blog : IBlog
    {
        public string Title { get; set; }
        public IEnumerable<Post> Posts { get; set; }

        public Blog()
        {
            Title = "Sample Blog";
            Posts = new[] {new Post()};
        }

        public RssResponse Rss()
        {
            var feed = new SyndicationFeed
            {
                Title =  new TextSyndicationContent(Title),
                Items = Posts
                    .Take(10)
                    .Select((p, i) => new SyndicationItem(
                        p.Title,
                        p.Html(),
                        new Uri("http://localhost/Content/Two"),
                        i.ToString(CultureInfo.InvariantCulture),
                        DateTime.UtcNow
                        ))
            };
            return new RssResponse(feed);
        }
    }
}