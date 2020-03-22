using System;

namespace Keneyz_03.Tools.Exceptions
{
    internal class InvalidEmailExceptions : Exception
    {
        public InvalidEmailExceptions(string email) : base($"Invalid email address: {email}")

        {
        }
    }
}