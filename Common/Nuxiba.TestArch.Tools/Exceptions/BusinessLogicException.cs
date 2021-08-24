using System;

namespace Nuxiba.TestArch.Tools.Exceptions
{
    public class BusinessLogicException : Exception
    {
        public BusinessLogicException(string message)
            : base(message)
        {
        }
    }
}