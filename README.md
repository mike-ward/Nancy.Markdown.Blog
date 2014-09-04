## Nancy.Markdown.Blog ##

**This is a work in progress**

A blog engine that uses [Markdown](http://daringfireball.net/projects/markdown/syntax) and [NancyFx](http://NancyFx.org)

Nancy.Markdown.Blog can integrate with exsiting sites. It's relatively unopinionated about views, paging, etc.

See the Example project for some ideas on how to use it.

**Features**

- Posts are composed in Markdown. The Markdown engine understands 
  [PHP Markdown Extras](https://michelf.ca/projects/php-markdown/extra/) (similar to GitHub extras)
- RSS syndication
- PermaLinks and Slugs supported
- Html extensions to inject Markdown

There is no API support. Author your posts in whatever editor you like and store the files however you wish.

**Install**

    PM> Install-Package Nancy.Markdown.Blog

**Example**

See the example project.

**Release Notes**

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
