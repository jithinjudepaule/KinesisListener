using EDispatchListener.models;
using EDispatchListener.utils;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace EDispatchListener.extensions
{
    public static class LoggerExtensions
    {
        public static void LogInfo(this ILogger logger, string logMessage, StreamRequestContext context, object appData = null)
        {
            logger.LogInformation(LoggingUtils.BuildLogMessage(context, LogTypes.INFO, logMessage, appData));
        }

        public static void LogWarning(this ILogger logger, string logMessage, StreamRequestContext context, object appData = null)
        {
            logger.LogWarning(LoggingUtils.BuildLogMessage(context, LogTypes.WARN, logMessage, appData));
        }

        public static void LogError(this ILogger logger, string logMessage, StreamRequestContext context, object appData = null)
        {
            logger.LogError(LoggingUtils.BuildLogMessage(context, LogTypes.ERROR, logMessage, appData));
        }
    }
}
