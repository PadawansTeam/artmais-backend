using System;

namespace ArtmaisBackend.Core.Aws
{
    public static class AwsConfiguration
    {
        public const string BucketName = "bucket-artmais";
        public const string FilePath = "profile-pictures/";
        public static string ObjectKey { get; set; } = $"{DateTime.Today:yyyyMMdd}{new Random((int)DateTime.Now.Ticks).Next():D14}";
    }
}