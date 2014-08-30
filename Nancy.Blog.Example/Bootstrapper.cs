using System.Collections.Generic;
using System.IO;
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