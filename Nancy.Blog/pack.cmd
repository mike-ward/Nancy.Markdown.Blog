C:\Windows\Microsoft.NET\Framework\v4.0.30319\msbuild.exe Nancy.Blog.csproj /property:Configuration=Release
IF ERRORLEVEL 0 nuget pack Nancy.Blog.csproj -Prop Configuration=Release