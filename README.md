## Nancy.Markdown.Blog ##

[Demo site](http://mike-ward.net)

[Documentation](https://github.com/mike-ward/Nancy.Markdown.Blog/wiki/About-Nancy.Markdown.Blog)

[![Build status](https://ci.appveyor.com/api/projects/status/cfd6x74actliiurs)](https://ci.appveyor.com/project/mike-ward/nancy-markdown-blog)

A blog engine that uses [Markdown](http://daringfireball.net/projects/markdown/syntax) and [NancyFx](http://NancyFx.org)

Nancy.Markdown.Blog can integrate with exsiting sites. It's relatively unopinionated about views, paging, etc.

See the example project for some ideas on how to use it.

**Features**

- Posts are composed in Markdown. The Markdown engine understands 
  [PHP Markdown Extras](https://michelf.ca/projects/php-markdown/extra/) (similar to GitHub extras)
- RSS syndication
- PermaLinks and Slugs
- Posts with dates in the future not shown
- Html extensions to inject Markdown

**Not Included**

- Publishing API: Author your posts in whatever editor you like and store the files however you wish.
- Commenting System: Instead use one of the many online services like [Disqus](https://disqus.com)


**Install**

    PM> Install-Package Nancy.Markdown.Blog

**Example**

See the example project.

**Release Notes**

- 0.4.0
  + Add better previous/next page handling

- 0.3.1, 9/6/2014
  + Hide posts with future dates
  + Add filewatcher to example site

- 0.3.0, 9/3/2014
  + Change name to Nancy.Blog.Markdown

- 0.2.1, 9/3/2014
  + Html helpers to read markdown
  + Update example

- 0.2.0, 9/2/2014
  + Fix how permalinks work
  + Update example

- 0.1.0, 9/1/2014
  + Initial release
