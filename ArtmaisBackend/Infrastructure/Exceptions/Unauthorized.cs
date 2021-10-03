using System;

namespace ArtmaisBackend.Exceptions
{
    public class Unauthorized : Exception
    {
        public Unauthorized() { }

        public Unauthorized(string message)
            : base(message) { }

        public Unauthorized(string message, Exception inner)
            : base(message, inner) { }
    }
}
