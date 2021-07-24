using ArtmaisBackend.Core.Profile.Dto;
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
            this._recomendationMediator = recomendationMediator;
        }

        private readonly IRecomendationMediator _recomendationMediator;

        [HttpGet]
        public ActionResult<IEnumerable<RecomendationDto>> Index()
        {
            try
            {
                return this.Ok(this._recomendationMediator.Index(this.User));
            }
            catch
            {
                return this.Forbid();
            }
        }
    }
}
