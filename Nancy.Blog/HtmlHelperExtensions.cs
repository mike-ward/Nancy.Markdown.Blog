using System.Collections.Concurrent;
using System.IO;
using Nancy.ViewEngines.Razor;

namespace Nancy.Markdown.Blog
{
    public static class HtmlHelperExtensions
    {
        private static readonly ConcurrentDictionary<int, IHtmlString> Cache = new ConcurrentDictionary<int, IHtmlString>();

        private static IHtmlString Html(string text, string baseUri)
        {
            var md = new MarkdownDeep.Markdown {ExtraMode = true, UrlBaseLocation = baseUri};
            var html = md.Transform(text);
            return new NonEncodedHtmlString(html);
        }

        public static IHtmlString Markdown<TModel>(this HtmlHelpers<TModel> helpers, string text, string baseUri = null)
        {
            var hash = (text + (baseUri ?? string.Empty)).GetHashCode();
            return Cache.GetOrAdd(hash, h => Html(text, baseUri));
        }

        public static IHtmlString MarkdownLoad<TModel>(this HtmlHelpers<TModel> helpers, string path, string baseUri = null)
        {
            return helpers.Markdown(File.ReadAllText(path), baseUri);
        }
    }
}