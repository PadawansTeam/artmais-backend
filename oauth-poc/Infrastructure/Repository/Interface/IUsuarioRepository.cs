using oauth_poc.Core.Entities;

namespace oauth_poc.Infrastructure.Repository.Interface
{
    public interface IUsuarioRepository
    {
        public Usuario GetUsuarioByEmailAndSenha(string email, string senha);
    }
}
