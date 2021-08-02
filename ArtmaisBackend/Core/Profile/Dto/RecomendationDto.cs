namespace ArtmaisBackend.Core.Profile.Dto
{
    public class RecomendationDto
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public string UserPicture { get; set; }
        public string BackgroundPicture { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }
    }
}
