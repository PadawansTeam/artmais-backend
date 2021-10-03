using System;

namespace ArtmaisBackend.Exceptions
{
    public class UsernameAlreadyInUse : Exception
    {
        public UsernameAlreadyInUse() { }

        public UsernameAlreadyInUse(string message)
            : base(message) { }

        public UsernameAlreadyInUse(string message, Exception inner)
            : base(message, inner) { }
    }
}
