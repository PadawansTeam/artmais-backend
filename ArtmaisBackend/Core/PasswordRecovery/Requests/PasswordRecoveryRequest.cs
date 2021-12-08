namespace ArtmaisBackend.Core.PasswordRecovery.Requests
{
    public class PasswordRecoveryRequest
    {
        public long UserId { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordConfirmation { get; set; }
    }
}
