namespace DataAccess.Models;

public class File
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Size { get; set; }
    public string Type { get; set; }
    public string Url { get; set; }
    public string UrlPath { get; set; }
}