using Amazon.Lambda.Core;
using Amazon.Lambda.KinesisEvents;
using KinesisListener.models;
using KinesisListener.service;
using KinesisListener.utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace KinesisListener
{
    public class LambdaEntryPoint
    {
        private static readonly string _aspNetCoreEnvVariable = "ASPNETCORE_ENVIRONMENT";
        private static readonly string _logging = "Logging";

        private IServiceProvider _container;
        private KinesisEventProcessor _kinesisEventProcessor;

        public LambdaEntryPoint()
        {
            Init();
        }

        
        public async Task FunctionHandlerAsync(KinesisEvent kinesisEvent, ILambdaContext lambdaContext)
        {
            var context = new StreamRequestContext
            {
                AppName = lambdaContext.FunctionName,
                Version = lambdaContext.FunctionVersion,
                SystemInformation = new
                {
                    lambdaRequestId = lambdaContext.AwsRequestId,
                    sourceType = "LAMBDA_FUNCTION"
                }
            };

            _kinesisEventProcessor.ProcessKinesisEvent(kinesisEvent, context);
        }

        private void Init()
        {
            var services = new ServiceCollection();

            // Configuration
            var configBuilder = new ConfigurationBuilder()
                                    .AddJsonFile("appsettings.json",
                                                 true,
                                                 true)
                                    //.AddJsonFile(string.Format("appsettings.{0}.json", Environment.GetEnvironmentVariable(_aspNetCoreEnvVariable)), true, true)
                                    .AddEnvironmentVariables();

            var configuration = configBuilder.Build();
            

            // Logging
            var loggerFactory = new LoggerFactory().AddLambdaLogger(LambdaLoggerOptionsBuilder.Build(configuration, _logging));
            services.AddSingleton(loggerFactory);
            services.AddLogging();

            // Services
            services.AddSingleton<HttpClient>();
            services.AddSingleton<KinesisEventProcessor>();

            // Finalize
            _container = services.BuildServiceProvider();
            _kinesisEventProcessor = _container.GetService<KinesisEventProcessor>();
        }
    }
}
