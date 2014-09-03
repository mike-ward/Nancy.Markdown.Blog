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
        public Func<Post, string> PermaLink { get; set; }

        public Blog()
        {
            Title = "Sample Blog";
            Author = "Sample Blog Generator";
            Description = "Sample Blog Description";
            BaseUri = new Uri("http://www.example.com");
            Copyright = "Copyright 2014";
            Langauge = "en-US";
            PermaLink = post => string.Format("{0}/post/{1}/{2}/{3}/{4}",
                BaseUri, post.Created.Year, post.Created.Month, post.Created.Day, post.Slug);
        }

        public IEnumerable<Post> Posts
        {
            get { return _posts; }
            set
            {
                if (BaseUri == null) throw new InvalidOperationException("BaseUri is null");
                if (PermaLink == null) throw new InvalidOperationException("PermaLink is null");
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
                    .Select(post => new SyndicationItem(
                        post.Title,
                        post.Html(),
                        new Uri(PermaLink(post)),
                        post.Slug,
                        DateTime.UtcNow
                        ))
            };
            feed.Authors.Add(new SyndicationPerson(Author));
            return new RssResponse(feed);
        }
    }
}