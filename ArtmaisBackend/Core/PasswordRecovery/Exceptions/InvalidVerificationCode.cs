using System;

namespace ArtmaisBackend.Core.PasswordRecovery.Exceptions
{
    public class InvalidVerificationCode : Exception
    {
        public InvalidVerificationCode() { }

        public InvalidVerificationCode(string message)
            : base(message) { }

        public InvalidVerificationCode(string message, Exception inner)
            : base(message, inner) { }
    }
}
