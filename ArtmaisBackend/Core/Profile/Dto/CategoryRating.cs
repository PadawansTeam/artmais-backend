namespace ArtmaisBackend.Core.Profile.Dto
{
    public class CategoryRating
    {
        public long VisitorUserId { get; set; }
        public int VisitedSubcategoryId { get; set; }
        public float VisitNumber { get; set; }
    }
}
