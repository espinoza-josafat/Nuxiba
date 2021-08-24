using System;

namespace Nuxiba.TestArch.Tools.Exceptions
{
    public class ProcessingException : Exception
    {
        public ProcessingException(string message)
            : base(message)
        {
        }
    }
}