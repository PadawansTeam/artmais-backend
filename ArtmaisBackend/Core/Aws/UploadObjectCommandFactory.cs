using Microsoft.AspNetCore.Http;
using System;

namespace ArtmaisBackend.Core.Aws
{
    public class UploadObjectCommandFactory
    {
        public long UserId { get; private set; }
        public IFormFile? File { get; private set; }
        public Channel Channel { get; private set; }
        
        public UploadObjectCommandFactory(long userId, IFormFile? file, Channel channel)
        {
            UserId = userId;
            File = file;
            Channel = channel;
        }

        public UploadObjectCommand Create()
        {
            return new UploadObjectCommand
            {
                UserId = UserId,
                File = File,
                Channel = $"{Channel}",
                BucketName = "bucket-artmais",
                ObjectKey = $"{DateTime.Today:yyyyMMdd}{new Random((int)DateTime.Now.Ticks).Next():D14}"
            };
        }
    }
}
