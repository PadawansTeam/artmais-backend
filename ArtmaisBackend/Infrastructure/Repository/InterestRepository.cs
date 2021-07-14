using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Profile;
using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using System.Linq;

namespace ArtmaisBackend.Infrastructure.Repository
{
    public class InterestRepository : IInterestRepository
    {
        public InterestRepository(ArtplusContext context)
        {
            _context = context;
        }

        private readonly ArtplusContext _context;

        public dynamic DeleteAllAndCreateAll(InterestRequest interestRequest, int userId)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    DeleteAll(userId);

                    foreach (var subcategoryId in interestRequest.SubcategoryID)
                    {
                        Create(subcategoryId, userId);
                    }

                    transaction.Commit();
                    _context.SaveChanges();
                    return new { message = "Os interesses foram salvos com sucesso." };
                }
                catch
                {
                    transaction.Rollback();
                    return new { message = "Erro ao salvar interesses." };
                }
            }
        }

        private void Create(int subcategoryId, int userId)
        {
            var interest = new Interest
            {
                UserID = userId,
                SubcategoryID = subcategoryId
            };
            _context.Interest.Add(interest);
        }

        private void DeleteAll(int userId)
        {
            _context.Interest.RemoveRange(_context.Interest
                .Where(i => i.UserID.Equals(userId)));
        }

    }
}
