using Amazon.Lambda.KinesisEvents;
using KinesisListener.exceptions;

using KinesisListener.models;
using KinesisListener.utils;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace KinesisListener.service
{
    public class KinesisEventProcessor

    {
        private readonly ILogger<KinesisEventProcessor> _logger;

        public KinesisEventProcessor(ILogger<KinesisEventProcessor> logger)
        {
            this._logger = logger;
        }

        public void ProcessKinesisEvent(KinesisEvent kinesisEvent, StreamRequestContext context)
        {
            

            try
            {
                foreach (var record in kinesisEvent.Records)
                {
                   
                    ProcessRecord(context, record);
                }
            }
            catch (BatchProcessingException batchEx)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async void ProcessRecord(StreamRequestContext context, KinesisEvent.KinesisEventRecord record)
        {
            var correlationId = string.Empty;
            try
            {
                var customerNotifications = StreamUtils.GetStreamRecord<CustomerNotificationDTO>(record.Kinesis.Data);

                _logger.LogInfo($"Call Type: {customerNotifications.Event.RequestType} number: {customerNotifications.Customer.NotificationNumber} status:{customerNotifications.Event.RequestStatus}",context);
                if (customerNotifications.Event.RequestType == "Digital" && customerNotifications.Event.RequestStatus == "REQUEST CONFIRMED")
                {
                    HttpClient client = new HttpClient();

                    var caseId = customerNotifications.CaseData != null ? customerNotifications.CaseData.CaseId.ToString() : "";
                    var clientId = customerNotifications.CaseData != null ? customerNotifications.CaseData.ClientId.ToString() : "";
                    var callbackRefNumber = customerNotifications.Customer != null ? customerNotifications.Customer.CallbackRefNumber.ToString() : "";
                    var notificationNumber = customerNotifications.Customer != null ? customerNotifications.Customer.NotificationNumber.ToString() : "";
                    var values = new Dictionary<string, string>
                {
                    { "CallbackRefNumber", callbackRefNumber }
              

                };

                    var content = new FormUrlEncodedContent(values);
                    var response = await client.PostAsync("", content);
                    var responseString = await response.Content.ReadAsStringAsync();

                }
            }


            catch (BatchProcessingException processingException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

    }


}
