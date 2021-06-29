using System;

namespace oauth_poc.Exceptions
{
    public class EmailAlreadyInUse : Exception
    {
        public EmailAlreadyInUse() { }

        public EmailAlreadyInUse(string message)
            : base(message) { }

        public EmailAlreadyInUse(string message, Exception inner)
            : base(message, inner) { }
    }
}
