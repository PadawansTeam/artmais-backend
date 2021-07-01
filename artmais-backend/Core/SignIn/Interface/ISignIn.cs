namespace artmais_backend.Core.SignIn.Interface
{
    public interface ISignIn
    {
        public string Authenticate(SigInRequest sigInRequest);
    }
}
