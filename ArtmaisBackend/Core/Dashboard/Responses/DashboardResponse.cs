using System.Collections.Generic;

namespace ArtmaisBackend.Core.Dashboard.Responses
{
    public class DashboardResponse
    {
        public IEnumerable<GrowthData> CommentsGrowth { get; set; }
        public IEnumerable<GrowthData> LikesGrowth { get; set; }
        public IEnumerable<GrowthData> VisitsGrowth { get; set; }
        public IEnumerable<PredictionData> CommentsPrediction { get; set; }
        public IEnumerable<PredictionData> LikesPrediction { get; set; }
        public IEnumerable<PredictionData> VisitsPrediction { get; set; }
        public int AverageUsersAge { get; set; }
        public int SumComments { get; set; }
        public int SumLikes { get; set; }
        public int SumVisits { get; set; }
        public bool IsPremium { get; set; }
        public IEnumerable<GrowthData> AudioCommentsGrowth { get; set; }
        public IEnumerable<GrowthData> AudioLikesGrowth { get; set; }
        public IEnumerable<GrowthData> PicturesCommentsGrowth { get; set; }
        public IEnumerable<GrowthData> PicturesLikesGrowth { get; set; }
        public IEnumerable<GrowthData> VideoCommentsGrowth { get; set; }
        public IEnumerable<GrowthData> VideoLikesGrowth { get; set; }
    }
}
