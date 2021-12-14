using ArtmaisBackend.Core.Answer.Dtos;
using ArtmaisBackend.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface IAnswerRepository
    {
        Task<Answer> CreateAsync(string description, int commentId, long userId);
        Task<IEnumerable<Answer>> GetAnswerByCommentIdAsync(int commentId);
        public void Delete(Answer answer);
        Task<Answer> GetAnswerByAnswerId(long answerId);
        Task<List<AnswerDto?>> GetAnswerDtoByCommentId(int commentId);
    }
}
