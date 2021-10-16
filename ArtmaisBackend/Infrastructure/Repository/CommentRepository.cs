using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Portfolio.Dto;
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

        public async Task<List<CommentDto?>> GetAllCommentsByPublicationId(int? publicationId)
        {
            var listComments = await (from comments in this._context.Comment
                                      join user in this._context.User on comments.UserID equals user.UserID
                                      where comments.PublicationID.Equals(publicationId)
                                      select new CommentDto
                                      {
                                          Name = user.Name,
                                          Username = user.Username,
                                          Description = comments.Description,
                                          CommentDate = comments.CommentDate
                                      }).ToListAsync();

            return listComments;
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
