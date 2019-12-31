using System;
using System.Collections.Generic;
using System.Text;

namespace KinesisListener.exceptions
{
    public class BatchProcessingException : BaseException
    {
        public BatchProcessingException(string message, Exception innerException = null) : base(message, innerException)
        {
        }
    }
  
}
