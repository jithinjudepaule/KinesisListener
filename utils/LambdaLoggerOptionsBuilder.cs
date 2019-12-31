using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinesisListener.utils
{
    public static class LambdaLoggerOptionsBuilder
    {
        private const string VIMSCategoryPrefix = "ERequestDispatchListener";
        private const string MicrosoftCategoryPrefix = "Microsoft.";

        public static LambdaLoggerOptions Build(IConfiguration config, string defaultSection)
        {
            // Create and populate LambdaLoggerOptions object.
            var loggerOptions = new LambdaLoggerOptions();
            loggerOptions.IncludeCategory = TryGetValueFromConfig<bool>(config[$"{defaultSection}:IncludeCategory"]);
            loggerOptions.IncludeLogLevel = TryGetValueFromConfig<bool>(config[$"{defaultSection}:IncludeLogLevel"]);
            loggerOptions.IncludeNewline = TryGetValueFromConfig<bool>(config[$"{defaultSection}:IncludeNewline"]);

            loggerOptions.Filter = GetFiltersFromConfig(config.GetSection(defaultSection).GetSection("LogLevel"));
            return loggerOptions;
        }

        private static Func<string, LogLevel, bool> GetFiltersFromConfig(IConfiguration logLevelsSection)
        {
            // Empty section means log everything.
            var logLevels = logLevelsSection.GetChildren().ToList();
            if (logLevels.Count == 0)
            {
                return null;
            }

            var logLevelsMapping = new Dictionary<string, LogLevel>(StringComparer.Ordinal);

            foreach (var logLevel in logLevels)
            {
                var category = logLevel.Key;
                var minLevelValue = logLevel.Value;
                LogLevel minLevel;
                if (!Enum.TryParse(logLevel.Value, out minLevel))
                {
                    throw new InvalidCastException($"Unable to convert level '{minLevelValue}' for category '{category}' to LogLevel.");
                }
                logLevelsMapping[category] = minLevel;
            }

            return (string category, LogLevel logLevel) =>
            {
                // For some categories, only log events with minimum LogLevel.
                if (category.StartsWith(MicrosoftCategoryPrefix, StringComparison.Ordinal))
                {
                    return (logLevel >= logLevelsMapping.TryGetLogLevel("Microsoft"));
                }
                else if (category.Contains(VIMSCategoryPrefix))
                {
                    var clientConnectCategory = category.Replace(VIMSCategoryPrefix, "").Replace(".", "");
                    if (logLevelsMapping.ContainsKey(clientConnectCategory))
                    {
                        return (logLevel >= logLevelsMapping[clientConnectCategory]);
                    }
                    else
                    {
                        return (logLevel >= logLevelsMapping.TryGetLogLevel("Default"));
                    }
                }

                return true;
            };
        }

        private static LogLevel TryGetLogLevel(this Dictionary<string, LogLevel> logLevelMappings, string key)
        {
            if (logLevelMappings.ContainsKey(key))
            {
                return logLevelMappings[key];
            }
            return LogLevel.Debug;
        }

        private static T TryGetValueFromConfig<T>(string value)
        {
            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }

}
