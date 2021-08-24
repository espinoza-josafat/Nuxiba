using System;

namespace Nuxiba.TestArch.Web.Exceptions
{
    public class ProcessingException : Exception
    {
        public ProcessingException(string message)
            : base(message)
        {
        }
    }
}