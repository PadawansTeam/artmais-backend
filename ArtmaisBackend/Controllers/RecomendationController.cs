using ArtmaisBackend.Core.Profile;
using ArtmaisBackend.Core.Profile.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ArtmaisBackend.Controllers
{
    [ApiController]
    [Route("/v1/[controller]")]
    public class RecomendationController : ControllerBase
    {
        public RecomendationController(IRecomendationMediator recomendationMediator)
        {
            _recomendationMediator = recomendationMediator;
        }

        private readonly IRecomendationMediator _recomendationMediator;

        [HttpGet]
        public ActionResult<IEnumerable<RecomendationDto>> Index()
        {
            try
            {
                return Ok(_recomendationMediator.Index(User));
            }
            catch
            {
                return Forbid();
            }
        }
    }
}
