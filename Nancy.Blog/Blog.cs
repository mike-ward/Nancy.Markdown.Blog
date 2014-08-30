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