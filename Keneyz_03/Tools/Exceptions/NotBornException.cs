using System;

namespace Keneyz_03.Tools.Exceptions
{
    class NotBornException : Exception
    {
        public NotBornException() : base("Person is not born yet")
        {
        }
    }
}