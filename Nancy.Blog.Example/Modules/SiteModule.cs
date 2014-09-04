namespace Nancy.Markdown.Blog.Example.Modules
{
    public class SiteModule : NancyModule
    {
        public SiteModule(IRootPathProvider rootPathProvider)
        {
            Get["/"] = p => Response.AsRedirect("~/blog");
            Get["/about"] = p => View["about"];
            Get["/readme"] = p => View["readme", rootPathProvider.GetRootPath()];
        }
    }
}