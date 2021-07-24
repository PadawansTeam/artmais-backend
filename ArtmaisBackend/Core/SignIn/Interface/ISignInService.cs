namespace ArtmaisBackend.Core.SignIn.Interface
{
    public interface ISignInService
    {
        string Authenticate(SigInRequest sigInRequest);
    }
}