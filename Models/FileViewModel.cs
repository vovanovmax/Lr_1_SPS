namespace WebApplication2.Models
{
    public class FileViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string Extension { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public DateTime LastModified { get; set; }
        public string FileCategory { get; set; } = "other"; // image, pdf, text, other
        public string? TextContent { get; set; }
    }
}