using ArtmaisBackend.Core.Profile;
using ArtmaisBackend.Core.Profile.Dto;
using ArtmaisBackend.Core.Profile.Interface;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<dynamic> Create(InterestRequest interestRequest)
        {
            try
            {
                return Ok(_interestMediator.Create(interestRequest, User));
            }
            catch
            {
                return Forbid();
            }
        }
    }
}
