namespace ArtmaisBackend.Core.Aws.Dto
{
    public class AwsDto
    {
        public string UserPicture { get; set; }

        public AwsDto(string userPicture) 
        {
            UserPicture = userPicture;
        }
    }
}
