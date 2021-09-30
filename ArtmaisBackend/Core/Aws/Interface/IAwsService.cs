﻿using Amazon.S3.Model;
using ArtmaisBackend.Core.Aws.Dto;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Aws.Interface
{
    public interface IAwsService
    {
        Task<AwsDto?> UploadObjectAsync(IFormFile file, long userId);
        Task<string> WritingAnObjectAsync(IFormFile? file, long userId);
    }
}