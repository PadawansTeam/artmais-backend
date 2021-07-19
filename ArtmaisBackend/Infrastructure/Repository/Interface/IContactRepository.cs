using ArtmaisBackend.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface IContactRepository
    {
        Task<Contact?> GetContactByUserAsync(int userId);
    }
}
