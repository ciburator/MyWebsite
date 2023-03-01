namespace web_server.Common.Helpers;

using Microsoft.AspNetCore.StaticFiles;

internal static class FileHelper
{
    internal static string GetFileType(string path)
    {
        var provider = new FileExtensionContentTypeProvider();

        if (!provider.TryGetContentType(path, out var contentType))
        {
            contentType = "application/octet-stream";
        }

        return contentType;
    }
}