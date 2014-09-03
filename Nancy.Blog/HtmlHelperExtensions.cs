using System.IO;
using MarkdownDeep;
using Nancy.ViewEngines.Razor;

namespace Nancy.Blog
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlString Markdown<TModel>(this HtmlHelpers<TModel> helpers, string text)
        {
            var md = new Markdown {ExtraMode = true};
            var html = md.Transform(text);
            return new NonEncodedHtmlString(html);
        }

        public static IHtmlString MarkdownLoad<TModel>(this HtmlHelpers<TModel> helpers, string path)
        {
            return helpers.Markdown(File.ReadAllText(path));
        }
    }
}