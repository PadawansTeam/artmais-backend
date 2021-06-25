using oauth_poc.Core.Entities;
using oauth_poc.Infrastructure.Data;
using oauth_poc.Infrastructure.Repository.Interface;
using System.Linq;

namespace oauth_poc.Infrastructure.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public UsuarioRepository(AuthContext context)
        {
            _context = context;
        }

        private readonly AuthContext _context;

        public Usuario GetUsuarioByEmailAndSenha(string email, string senha)
        {
            var query = from usuario in _context.Usuario
                        where usuario.Email.Equals(email)
                        && usuario.Senha.Equals(senha)
                        select new Usuario { ID = usuario.ID, Nome = usuario.Nome, Sobrenome = usuario.Sobrenome,
                            NomeSocial = usuario.NomeSocial, Email = usuario.Email, Senha = usuario.Senha,
                            DataCadastro = usuario.DataCadastro };

            return query.FirstOrDefault();
        }
    }
}
