using Microsoft.AspNetCore.Http;
using System;

namespace ArtmaisBackend.Core.Aws
{
    public static class UploadObjectCommandFactory
    {        
        public static UploadObjectCommand Create(long userId, IFormFile? file, Channel channel)
        {
            return new UploadObjectCommand
            {
                UserId = userId,
                File = file,
                Channel = $"{channel}",
                BucketName = "bucket-artmais",
                ObjectKey = $"{DateTime.Today:yyyyMMdd}{new Random((int)DateTime.Now.Ticks).Next():D14}"
            };
        }
    }
}
