using System;
using System.Collections.Generic;
using System.Text;

namespace KinesisListener.exceptions
{
    public class GatewayException : BaseException
    {
        public GatewayException(string message, Exception innerException = null) : base(message, innerException)
        {
        }
    }
    class Class1
    {
    }
}
