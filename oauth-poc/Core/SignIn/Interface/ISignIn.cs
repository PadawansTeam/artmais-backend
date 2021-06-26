namespace oauth_poc.Core.SignIn.Interface
{
    public interface ISignIn
    {
        public string Authenticate(SigInRequest sigInRequest);
    }
}
