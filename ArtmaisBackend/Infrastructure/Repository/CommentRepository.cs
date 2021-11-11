using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Publications.Dto;
using ArtmaisBackend.Core.Publications.Request;
using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ArtmaisBackend.Infrastructure.Repository
{
    [ExcludeFromCodeCoverage]
    public class CommentRepository : ICommentRepository
    {
        public CommentRepository(ArtplusContext context)
        {
            _context = context;
        }

        private readonly ArtplusContext _context;

        public async Task<List<CommentDto?>> GetAllCommentsDtoByPublicationId(int? publicationId)
        {
            var listComments = await (from comments in _context.Comment
                                      join user in _context.User on comments.UserID equals user.UserID
                                      where comments.PublicationID.Equals(publicationId)
                                      select new CommentDto
                                      {
                                          Name = user.Name,
                                          Username = user.Username,
                                          UserPicture = user.UserPicture,
                                          Description = comments.Description,
                                          CommentDate = comments.CommentDate
                                      }).ToListAsync();

            return listComments;
        }

        public async Task<List<Comment>> GetAllCommentsByPublicationId(int? publicationId)
        {
            return await _context.Comment.Where(comment => comment.PublicationID == publicationId).ToListAsync();
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

            _context.Comment.Add(commentContent);
            _context.SaveChanges();
        }

        public void Delete(Comment comment)
        {
            _context.Comment.Remove(comment);
            _context.SaveChanges();
        }
    }
}
