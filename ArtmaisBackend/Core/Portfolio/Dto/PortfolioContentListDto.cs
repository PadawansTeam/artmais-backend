using System.Collections.Generic;

namespace ArtmaisBackend.Core.Portfolio.Dto
{
    public class PortfolioContentListDto
    {
        public List<PortfolioContentDto> Image { get; set; }
        public List<PortfolioContentDto> Video { get; set; }
        public List<PortfolioContentDto> Audio { get; set; }
    }
}
