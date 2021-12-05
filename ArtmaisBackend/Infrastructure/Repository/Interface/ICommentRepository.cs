using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Publications.Dto;
using ArtmaisBackend.Core.Publications.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface ICommentRepository
    {
        Task<List<CommentDto?>> GetAllCommentsDtoByPublicationId(int? publicationId);
     
        Task<List<Comment>> GetAllCommentsByPublicationId(int? publicationId);
       
        void Create(CommentRequest commentRequest, long userId);

        void Delete(Comment comment);

        Comment GetCommentById(int? commentId);
    }
}
