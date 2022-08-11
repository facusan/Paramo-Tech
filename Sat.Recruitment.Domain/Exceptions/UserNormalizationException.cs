using System;

namespace Sat.Recruitment.Domain.Exceptions
{
    public class UserNormalizationException : Exception
    {
        public UserNormalizationException(string message , Exception exception) : base(message,exception)
        {
        }
    }
}
