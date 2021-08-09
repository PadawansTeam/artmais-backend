using ArtmaisBackend.Core.Profile.Dto;
using ArtmaisBackend.Core.Profile.Interface;
using Microsoft.AspNetCore.Http;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        [HttpGet]
        [Route("users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<RecomendationDto>> GetUsers()
        {
            return this.Ok(this._recomendationMediator.GetUsers());
        }
    }
}
