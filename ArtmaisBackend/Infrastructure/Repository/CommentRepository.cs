using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Portfolio.Request;
using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtmaisBackend.Infrastructure.Repository
{
    public class CommentRepository : ICommentRepository
    {
        public CommentRepository(ArtplusContext context)
        {
            this._context = context;
        }

        private readonly ArtplusContext _context;

        public async Task<List<Comment>> GetAllCommentsByPublicationId(int? publicationId)
        {
            return await _context.Comment.Where(p => p.PublicationID == publicationId).ToListAsync();
        }

        public void Create(CommentRequest commentRequest, long userId)
        {
            var commentContent = new Comment
            {
                UserID = userId,
                PublicationID = commentRequest.PublicationID,
                Description = commentRequest.Description,
                CommentDate = DateTime.Now
            };

            this._context.Comment.Add(commentContent);
            this._context.SaveChanges();
        }
    }
}
