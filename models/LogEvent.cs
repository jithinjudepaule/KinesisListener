using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EDispatchListener.models
{
    public class LogEvent
    {
        [JsonProperty("correlationId")]
        public string CorrelationId { get; set; }

        [JsonProperty("type")]
        public string LogType { get; set; }

        [JsonProperty("message")]
        public object Message { get; set; }

        [JsonProperty("app")]
        public object ApplicationData { get; set; }

        [JsonProperty("name")]
        public string ApplicationName { get; set; }

        [JsonProperty("version")]
        public string ApplicationVersion { get; set; }

        [JsonProperty("system")]
        public object SystemInformation { get; set; }
    }
}
