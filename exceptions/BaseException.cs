using System;
using System.Collections.Generic;
using System.Text;

namespace KinesisListener.exceptions
{
    public abstract class BaseException : Exception
    {
        public BaseException(string message, Exception innerException = null) : base(message, innerException)
        {
        }
    }
}
