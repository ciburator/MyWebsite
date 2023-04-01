using Microsoft.AspNetCore.Mvc;
using web_api.Common.Extensions;
using web_api.Common.Helpers;
using web_api.Models;

namespace web_api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class FileController : ControllerBase
{
    private readonly ILogger<FileController> _logger;
    private readonly IConfiguration _configuration;

    public FileController(
        ILogger<FileController> logger,
        IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    [HttpGet]
    [ActionName("getAll")]
    public List<DownloadFile> GetFiles(
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var folderPath = _configuration.GetValue<string>("DownloadsFolder");
        if (Directory.Exists(folderPath))
        {
            var fileList = new List<DownloadFile>();
            string[] files = Directory.GetFiles(folderPath);

            foreach (var file in files)
            {
                var path = file.Replace("D:\\Programming\\MyWebsite\\", "");
                var name = path.Split('\\')[1];

                var downloadFile = new DownloadFile
                {
                    Name = name,
                    Size = new FileInfo(file).Length.ToSizeDynamic(),
                    Url = path,
                    Extension = Path.GetExtension(file)
                };

                fileList.Add(downloadFile);
            }

            return fileList;
        }
        else
        {
            throw new Exception("Folder doesn't exist");
        }
    }

    [HttpGet]
    [ActionName("get")]
    public async Task<IResult> GetFile(
        [FromQuery]string fileUrl, 
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var filePath = Path.Combine("D:\\Programming\\MyWebsite\\", fileUrl);
        if (!System.IO.File.Exists(filePath))
            return Results.NotFound();

        var memory = new MemoryStream();
        await using (var stream = new FileStream(filePath, FileMode.Open))
        {
            await stream.CopyToAsync(memory, cancellationToken);
        }
        memory.Position = 0;

        return Results.File(memory, FileHelper.GetFileType(filePath), filePath);
    }

    [HttpPut]
    [ActionName("set")]
    public IResult SetFile() => Results.Ok();

    [HttpDelete]
    [ActionName("remove")]
    public IResult RemoveFile([FromQuery]string fileUrl) => Results.Ok();
}