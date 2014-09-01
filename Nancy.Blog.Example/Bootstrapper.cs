using System;
using System.IO;
using System.Linq;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace Nancy.Blog.Example
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            var blog = new Blog
            {
                Title = "Sample Blog",
                Author = "Sample Blog Generator",
                Description = "Sample Blog Description",
                BaseUri = new Uri("http://localhost:12116/blog/"),
                Copyright = "Copyright 2014",
                Langauge = "en-US"
            };

            var rootPath = container.Resolve<IRootPathProvider>().GetRootPath();

            blog.Posts = Directory.GetFiles(Path.Combine(rootPath, "App_Data/Blog/"), "*.md")
                .Select(file => Post.Read(File.OpenRead(file)));

            container.Register<IBlog>(blog);
        }
    }
}