C:\Windows\Microsoft.NET\Framework\v4.0.30319\msbuild.exe Nancy.Markdown.Blog.csproj /property:Configuration=Release
IF ERRORLEVEL 0 nuget pack Nancy.Markdown.Blog.csproj -Prop Configuration=Release