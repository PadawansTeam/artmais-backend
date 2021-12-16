using ArtmaisBackend.Core.Answer.Requests;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Answer.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;

        public AnswerService(IAnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
        }

        public async Task<Entities.Answer> CreateAsync(AnswerRequest answerRequest, long userId)
        {
            return await _answerRepository.CreateAsync(answerRequest.Description, answerRequest.CommentId, userId);
        }

        public async Task<Entities.Answer> DeleteAsync(long answerId)
        {
            var answer = await _answerRepository.GetAnswerByAnswerId(answerId);

            await _answerRepository.Delete(answer);

            return answer;
        }
    }
}
