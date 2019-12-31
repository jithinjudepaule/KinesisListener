using System;
using System.Collections.Generic;
using System.Text;

namespace KinesisListener.models
{
    public class StreamRequestContext
    {
        public string AppName { get; set; }
        public string Version { get; set; }
        public object SystemInformation { get; set; }
    } 
}
