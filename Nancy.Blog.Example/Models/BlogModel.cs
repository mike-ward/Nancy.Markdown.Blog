using System;
using System.IO;
using System.Linq;

namespace Nancy.Blog.Example.Models
{
    public interface IBlogModel
    {
        IBlog Blog { get; } 
    }

    public class BlogModel : IBlogModel
    {
        public IBlog Blog { get; private set; }

        public BlogModel(IBlog blog, IRootPathProvider rootPathProvider)
        {
            Blog = blog;
            blog.Title = "Sample Blog";
            blog.Author = "Sample Blog Generator";
            blog.Description = "Sample Blog Description";
            blog.BaseUri = new Uri("http://localhost:12116/blog");
            blog.Copyright = "Copyright 2014";
            blog.Langauge = "en-US";
            blog.Posts = Directory.GetFiles(Path.Combine(rootPathProvider.GetRootPath(), "App_Data/Blog/"), "*.md")
                .Select(file => Post.Read(File.OpenRead(file)));
        }
    }
}