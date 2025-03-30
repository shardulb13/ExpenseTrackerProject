using Microsoft.AspNetCore.Http;

namespace FinanceTrackingWebAPI.Model
{
    public class ImageUpload
    {
        public string FileName { get; set; }
        public IFormFile File { get; set; }
    }
}
