using ArtmaisBackend.Core.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Users.Interface
{
    public interface IUserService
    {
        Task<ShareLinkDto> GetShareLinkAsync(int userId);
    }
}
