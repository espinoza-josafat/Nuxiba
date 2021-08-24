using System;

namespace Nuxiba.TestArch.Web.Exceptions
{
    public class BusinessLogicException : Exception
    {
        public BusinessLogicException(string message)
            : base(message)
        {
        }
    }
}