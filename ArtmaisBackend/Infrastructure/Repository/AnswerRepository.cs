using ArtmaisBackend.Core.Answer.Dtos;
using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtmaisBackend.Infrastructure.Repository
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly ArtplusContext _context;

        public AnswerRepository(ArtplusContext context)
        {
            _context = context;
        }

        public async Task<Answer> CreateAsync(string description, int commentId, long userId)
        {
            var answer = new Answer
            {
                CommentID = commentId,
                Description = description,
                DateTime = DateTime.UtcNow,
                UserID = userId
            };

            await _context.Answer.AddAsync(answer);
            await _context.SaveChangesAsync();

            return answer;
        }

        public async Task<IEnumerable<Answer>> GetAnswerByCommentIdAsync(int commentId)
        {
            return await _context.Answer.Where(answer => answer.CommentID == commentId).ToListAsync();
        }

        public IEnumerable<Answer> GetAnswerByCommentId(int commentId)
        {
            return _context.Answer.Where(answer => answer.CommentID == commentId).ToList();
        }

        public async Task DeleteAsync(Answer answer)
        {
            _context.Answer.Remove(answer);
            await _context.SaveChangesAsync();
        }

        public void Delete(Answer answer)
        {
            _context.Answer.Remove(answer);
            _context.SaveChanges();
        }

        public async Task<Answer> GetAnswerByAnswerId(long answerId)
        {
            var answer = await _context.Answer.FirstOrDefaultAsync(answer => answer.AnswerID == answerId);

            _context.Entry(answer).Reference(a => a.Comment).Load();

            var comment = answer.Comment;

            _context.Entry(comment).Reference(c => c.Publication).Load();

            return answer;
        }

        public async Task<List<AnswerDto?>> GetAnswerDtoByCommentId(int commentId)
        {
            var listComments = await (from answer in _context.Answer
                                      join user in _context.User on answer.UserID equals user.UserID
                                      where answer.CommentID.Equals(commentId)
                                      select new AnswerDto
                                      {
                                          AnswerID = answer.AnswerID,
                                          Name = user.Name,
                                          Username = user.Username,
                                          UserPicture = user.UserPicture,
                                          Description = answer.Description,
                                          AnswerDate = answer.DateTime
                                      }).OrderByDescending(x => x.AnswerDate).ToListAsync();

            return listComments;
        }
    }
}
