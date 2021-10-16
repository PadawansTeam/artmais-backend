using ArtmaisBackend.Core.Portfolio.Dto;
using ArtmaisBackend.Core.Portfolio.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface ICommentRepository
    {
        Task<List<CommentDto?>> GetAllCommentsByPublicationId(int? publicationId);
        void Create(CommentRequest commentRequest, long userId);
    }
}
