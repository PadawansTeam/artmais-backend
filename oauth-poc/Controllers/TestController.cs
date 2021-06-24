using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using oauth_poc.Core.Entities;
using oauth_poc.Infrastructure.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oauth_poc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        public TestController(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        private readonly IUsuarioRepository _repository;

        [HttpGet]
        public ActionResult<Usuario> Get(string email, string senha)
        {
            var usuario = _repository.GetUsuarioByEmailAndSenha(email, senha);
            return Ok(usuario);
        }
    }
}
