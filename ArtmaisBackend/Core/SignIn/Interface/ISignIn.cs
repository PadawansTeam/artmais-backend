namespace ArtmaisBackend.Core.SignIn.Interface
{
    public interface ISignIn
    {
        string Authenticate(SigInRequest sigInRequest);
    }
}
