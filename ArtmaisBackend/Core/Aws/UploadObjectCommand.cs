using Microsoft.AspNetCore.Http;

namespace ArtmaisBackend.Core.Aws
{
    public class UploadObjectCommand
    {
        public long UserId { get; set; }
        public IFormFile? File { get; set; }
        public string Channel { get; set; }
        public string BucketName { get; set; }
        public string ObjectKey { get; set; }
        public bool IsProfileContent => Channel == "PROFILE";
        public string FilePath => GetFilePath();

        private string GetFilePath()
        {
            if (IsProfileContent) return "profile-pictures";

            return "portfolio-contents";
        }
    }
}