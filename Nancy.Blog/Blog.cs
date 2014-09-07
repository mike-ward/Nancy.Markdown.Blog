using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading;

namespace Nancy.Markdown.Blog
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
            Posts = new[] {new Post {Title = "Sample Post", Text = "Sample Content"}};
        }

        public IEnumerable<Post> Posts
        {
            get { return _posts.Where(p => p.Created <= DateTime.Now); }
            set
            {
                if (BaseUri == null) throw new InvalidOperationException("BaseUri is null");
                if (PermaLink == null) throw new InvalidOperationException("PermaLink is null");
                var posts = value.OrderByDescending(p => p.Created).ToArray();
                foreach (var post in posts) post.PermaLink = PermaLink(post);
                Interlocked.Exchange(ref _posts, posts);
            }
        }

        public IEnumerable<Post> AllPosts
        {
            get { return _posts; }
        }

        public Post PreviousPost(Post post)
        {
            if (post == null) return null;
            var posts = Posts;
            var index = posts.Select((p, i) => new {p, i}).FirstOrDefault(p => p.p.Equals(post)).i;
            return (index == 0) ? null : GetPost(posts, index -1);
        }

        public Post NextPost(Post post)
        {
            if (post == null) return null;
            var posts = Posts;
            var index = posts.Select((p, i) => new { p, i }).FirstOrDefault(p => p.p.Equals(post)).i;
            return GetPost(posts, index + 1);
        }

        private static Post GetPost(IEnumerable<Post> posts, int index)
        {
            try
            {
                return posts.ElementAt(index);
            }
            catch (ArgumentOutOfRangeException)
            {
                return null;
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