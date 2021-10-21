using ArtmaisBackend.Core.Profile;
using ArtmaisBackend.Core.Profile.Dto;
using ArtmaisBackend.Core.Profile.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ArtmaisBackend.Controllers
{
    [ApiController]
    [Route("/v1/[controller]")]
    public class InterestController : ControllerBase
    {
        public InterestController(IInterestMediator interestMediator)
        {
            _interestMediator = interestMediator;
        }

        private readonly IInterestMediator _interestMediator;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<InterestDto> Index()
        {
            try
            {
                return Ok(_interestMediator.Index(User));
            }
            catch
            {
                return Forbid();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<dynamic>> Create(InterestRequest interestRequest)
        {
            try
            {
                return Ok(await _interestMediator.Create(interestRequest, User));
            }
            catch
            {
                return Forbid();
            }
        }
    }
}
