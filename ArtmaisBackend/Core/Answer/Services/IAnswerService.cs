using ArtmaisBackend.Core.Answer.Requests;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Answer.Services
{
    public interface IAnswerService
    {
        Task<Entities.Answer> CreateAsync(AnswerRequest answerRequest, long userId);
        Task<Entities.Answer> DeleteAsync(long answerId);
    }
}
