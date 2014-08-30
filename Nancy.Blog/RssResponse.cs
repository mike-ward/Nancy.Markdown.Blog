using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;

namespace Nancy.Blog
{
    public class RssResponse : Response
    {
        public RssResponse(SyndicationFeed feed)
        {
            StatusCode = HttpStatusCode.OK;
            ContentType = "application/rss+xml";
            Contents = stream =>
            {
                var settings = new XmlWriterSettings
                {
                    Encoding = Encoding.UTF8,
                    NewLineHandling = NewLineHandling.Entitize,
                    NewLineOnAttributes = true,
                    Indent = true
                };
                var writer = XmlWriter.Create(stream, settings);
                feed.SaveAsRss20(writer);
                writer.Flush();
            };
        }
    }
}