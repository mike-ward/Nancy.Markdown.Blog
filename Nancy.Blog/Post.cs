using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Nancy.Markdown.Blog
{
    public class Post : IEquatable<Post>, IComparable<Post>
    {
        private string _title;
        public DateTime Created { get; set; }
        public string Text { get; set; }
        public string Slug { get; set; }
        public string PermaLink { get; set; }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                Slug = value.ToSlug();
            }
        }

        public string Html()
        {
            var md = new MarkdownDeep.Markdown {ExtraMode = true};
            return md.Transform(Text);
        }

        public static Post Read(Stream stream)
        {
            return Read(stream, Encoding.UTF8);
        }

        public static Post Read(Stream stream, Encoding encoding)
        {
            using (var reader = new StreamReader(stream, encoding))
            {
                var title = Regex.Replace(reader.ReadLine().Trim(), @"^#*", "").Trim();
                var date = DateTime.Parse(reader.ReadLine().Trim());
                var text = reader.ReadToEnd().Trim();
                return new Post {Title = title, Created = date, Text = text};
            }
        }

        public bool Equals(Post other)
        {
            return (Title == other.Title && Created == other.Created);
        }

        public int CompareTo(Post other)
        {
            return Created.CompareTo(other.Created);
        }
    }
}