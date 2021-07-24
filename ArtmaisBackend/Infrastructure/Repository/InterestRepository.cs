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
            this._context = context;
        }

        private readonly ArtplusContext _context;

        public dynamic DeleteAllAndCreateAll(InterestRequest interestRequest, int userId)
        {
            using (var transaction = this._context.Database.BeginTransaction())
            {
                try
                {
                    this.DeleteAll(userId);

                    foreach (var subcategoryId in interestRequest.SubcategoryID)
                    {
                        this.Create(subcategoryId, userId);
                    }

                    transaction.Commit();
                    this._context.SaveChanges();
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
            this._context.Interest.Add(interest);
        }

        private void DeleteAll(int userId)
        {
            this._context.Interest.RemoveRange(this._context.Interest
                .Where(i => i.UserID.Equals(userId)));
        }

    }
}
