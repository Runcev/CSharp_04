using System;

namespace Keneyz_03.Tools.Exceptions
{
    class TooOldException : Exception
    {
        public TooOldException() : base("Person can't be older than 135")
        {
        }
    }
}