## Nancy.Blog ##

**This is a work in progress**

A Blog engine that uses [Markdown](http://daringfireball.net/projects/markdown/syntax) and [NancyFx](http://NancyFx.org)

Nancy.Blog can integrate with exsiting sites. It's relatively unopinionated about views, paging, etc.

See the Example project for some ideas on how to use it.

**Features**

- Posts are composed in Markdown. The Markdown engine understands 
  [PHP Markdown Extras](https://michelf.ca/projects/php-markdown/extra/) (similar to GitHub extras)
- RSS syndication
- PermaLinks and Slugs supported

There is no API support. Author your posts in whatever editor you like and store the files however you wish.


**Install**

    PM> Install-Package Nancy.Blog

**Example**

See the example project.

**Release Notes**

- 0.2.0, 9/2/2014
  + Fix how permalinks work
  + Update example

- 0.1.0, 9/1/2014
  + Initial release
