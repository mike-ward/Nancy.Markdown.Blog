using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Nancy.Markdown.Blog.Example.Models
{
    public interface IBlogModel
    {
        IBlog Blog { get; } 
    }

    public class BlogModel : IBlogModel
    {
        public IBlog Blog { get; private set; }
        private readonly FileSystemWatcher _watchBlogPosts;
        private bool _disposed;

        public BlogModel(IBlog blog, IRootPathProvider rootPathProvider)
        {
            Blog = blog;
            blog.Title = "Sample Blog";
            blog.Author = "Sample Blog Generator";
            blog.Description = "Sample Blog Description";
#if DEBUG
            blog.BaseUri = new Uri("http://localhost:12116/blog");
#else
            blog.BaseUri = new Uri("http://localhost:12116/blog"); // release site goes here
#endif
            blog.Copyright = "Copyright 2014";
            blog.Langauge = "en-US";

            var path = Path.Combine(rootPathProvider.GetRootPath(), "App_Data/Blog/");
            blog.Posts = ReadPosts(path);

            _watchBlogPosts = new FileSystemWatcher(path) { NotifyFilter = NotifyFilters.LastWrite };
            _watchBlogPosts.Changed += (sender, args) => Blog.Posts = ReadPosts(path);
            _watchBlogPosts.EnableRaisingEvents = true;
        }
    
        private static IEnumerable<Post> ReadPosts(string path)
        {
            return Directory
                .GetFiles(path, "*.md")
                .Select(file => Post.Read(File.OpenRead(file)));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (_disposed) return;
            _disposed = true;
            _watchBlogPosts.Dispose();
        }
    }
}