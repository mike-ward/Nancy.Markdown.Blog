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
        public string Author { get; set; }
        public string Description { get; set; }
        public Uri BaseUri { get; set; }
        public string Copyright { get; set; }
        public string Langauge { get; set; }
        private IEnumerable<Post> _posts;

        public Blog()
        {
            Title = "Sample Blog";
            Author = "Sample Blog Generator";
            Description = "Sample Blog Description";
            BaseUri = new Uri("http://www.example.com");
            Copyright = "Copyright 2014";
            Langauge = "en-US";
            Posts = new[] {new Post()};
        }

        public IEnumerable<Post> Posts
        {
            get { return _posts; }
            set
            {
                _posts = value.OrderByDescending(p => p.Created).ToArray();
                foreach (var post in _posts) post.PermaLink = PermaLink(post);
            }
        }

        public RssResponse Rss()
        {
            var feed = new SyndicationFeed
            {
                Title = new TextSyndicationContent(Title),
                Description = new TextSyndicationContent(Description),
                BaseUri = BaseUri,
                Copyright = new TextSyndicationContent(Copyright),
                Language = Langauge,
                Items = Posts
                    .Take(10)
                    .Select((p, i) => new SyndicationItem(
                        p.Title,
                        p.Html(),
                        new Uri(PermaLink(p)),
                        i.ToString(CultureInfo.InvariantCulture),
                        DateTime.UtcNow
                        ))
            };
            feed.Authors.Add(new SyndicationPerson(Author));
            return new RssResponse(feed);
        }

        public string PermaLink(Post post)
        {
            return BaseUri + post.Slug;
        }

        public Post PostFromSlug(string slug)
        {
            return Posts.FirstOrDefault(p => slug.Equals(p.Slug, StringComparison.InvariantCultureIgnoreCase));
        }

        public int IndexFromSlug(string slug)
        {
            return Posts
                .Select((post, index) => new {post, index})
                .First(a => slug.Equals(a.post.Slug, StringComparison.InvariantCultureIgnoreCase)).index;
        }
    }
}