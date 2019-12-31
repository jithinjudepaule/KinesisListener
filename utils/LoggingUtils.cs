using KinesisListener.models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KinesisListener.utils
{
    public static class LoggingUtils
    {
        private static readonly string AssemblyName = (typeof(LoggingUtils)).Assembly.FullName;

        public static string BuildLogMessage(
                        StreamRequestContext context,
                        LogTypes logType,
                        string message,
                        object applicationData = null)
        {
            context = context ?? new StreamRequestContext
            {
                AppName = $"{AssemblyName}-UnknownEnvironment",
                Version = string.Empty,
                SystemInformation = null
            };

            return JsonConvert.SerializeObject(new LogEvent()
            {
                LogType = logType.ToString(),
                Message = message,
                ApplicationName = context.AppName,
                ApplicationVersion = context.Version,
                ApplicationData = applicationData,
                SystemInformation = context.SystemInformation
            });
        }
    }
}
