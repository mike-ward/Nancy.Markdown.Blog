# Some Code Examples
2014-08-18

Here's some code:

    using System.Collections.Generic;
    
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
        }
    }

A bit of text to explain.

And then some more code.

~~~
using System.Linq;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace Nancy.Blog.Example
{
    using Nancy;

    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            var blog = new Blog();
            var rootPath = container.Resolve<IRootPathProvider>().GetRootPath();

            blog.Posts = Directory.GetFiles(Path.Combine(rootPath, "App_Data/Blog/"), "*.md")
                .Select(file =>
                {
                    using (var f = File.OpenRead(file))
                    {
                        return Post.Read(f);
                    }
                })
                .ToArray();

            container.Register<IBlog>(blog);
        }
    }
}
~~~

The End