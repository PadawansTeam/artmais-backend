using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ArtmaisBackend.Infrastructure.Repository
{
    public class LikeRepository : ILikeRepository
    {
        public LikeRepository(ArtplusContext context)
        {
            _context = context;
        }

        private readonly ArtplusContext _context;

        public async Task Create(int? publicationId, long userId)
        {
            var likeContent = new Like
            {
                UserID = userId,
                PublicationID = publicationId,
                LikeDate = DateTime.Now
            };

            await _context.Like.AddAsync(likeContent);
            _context.SaveChanges();
        }

        public void Delete(Like like)
        {
            _context.Like.Remove(like);
            _context.SaveChanges();
        }

        public Like? GetLikeByPublicationIdAndUserId(int? publicationId, long userId)
        {
            return this._context.Like.FirstOrDefault(like => like.UserID == userId && like.PublicationID == publicationId);
        }
    }
}
