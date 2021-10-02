namespace ArtmaisBackend.Core.Aws.Dto
{
    public class AwsDto
    {
        public string Content { get; set; }

        public AwsDto(string userPicture) 
        {
            Content = userPicture;
        }
    }
}
