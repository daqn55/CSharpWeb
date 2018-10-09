using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Exceptions
{
    public class BadRequestException : Exception
    {
        private const string defaultMessage = "The Request was malformed or contains unsupported elements.";

        public BadRequestException()
        {
        }

        public BadRequestException(string message)
            : base(defaultMessage)
        {
        }
    }
}
