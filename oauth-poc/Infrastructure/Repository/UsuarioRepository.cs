using oauth_poc.Core.Entities;
using oauth_poc.Core.SignUp;
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

        public Usuario Create(SignUpRequest signUpRequest)
        {
            var usuario = new Usuario
            {
                Nome = signUpRequest.Nome,
                Sobrenome = signUpRequest.Sobrenome,
                NomeSocial = signUpRequest.NomeSocial,
                Email = signUpRequest.Email,
                Senha = signUpRequest.Senha
            };

            _context.Usuario.Add(usuario);
            _context.SaveChanges();

            return usuario;
        }

        public Usuario GetUsuarioByEmail(string email)
        {
            var query = from usuario in _context.Usuario
                        where usuario.Email.Equals(email)
                        select new Usuario
                        {
                            ID = usuario.ID,
                            Nome = usuario.Nome,
                            Sobrenome = usuario.Sobrenome,
                            NomeSocial = usuario.NomeSocial,
                            Email = usuario.Email,
                            Senha = usuario.Senha,
                            DataCadastro = usuario.DataCadastro
                        };

            return query.FirstOrDefault();
        }
    }
}
