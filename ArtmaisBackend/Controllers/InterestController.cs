using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Profile;
using ArtmaisBackend.Core.Profile.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ArtmaisBackend.Controllers
{
    [ApiController]
    [Route("/v1/[controller]")]
    public class InterestController : ControllerBase
    {
        public InterestController(IInterestMediator interest)
        {
            _interest = interest;
        }

        private readonly IInterestMediator _interest;

        [HttpGet]
        public ActionResult<InterestDto> Index()
        {
            try
            {
                return Ok(_interest.Index(User));
            }
            catch
            {
                return Forbid();
            }
        }

        [HttpPost]
        public ActionResult<dynamic> Create([FromBody] InterestRequest interestRequest)
        {
            try
            {
                return Ok(_interest.Create(interestRequest, User));
            }
            catch
            {
                return Forbid();
            }
        }
    }
}
