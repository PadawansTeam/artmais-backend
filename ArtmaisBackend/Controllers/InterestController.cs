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
        public InterestController(IInterest interest)
        {
            _interest = interest;
        }

        private readonly IInterest _interest;

        [HttpGet]
        public IEnumerable<SubcategoryDto> Index()
        {
            return _interest.Index();
        }
    }
}
