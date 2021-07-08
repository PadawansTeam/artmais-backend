namespace ArtmaisBackend.Core.SignIn.Interface
{
    public interface ISignIn
    {
        public string Authenticate(SigInRequest sigInRequest);
    }
}
