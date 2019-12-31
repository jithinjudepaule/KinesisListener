using System;
using System.Collections.Generic;
using System.Text;

namespace EDispatchListener.models
{
    public class CustomerNotificationDTO
    {
        public string TraceId { get; set; }
        public EventDataDTO Event { get; set; }
        public CustomerDTO Customer { get; set; }
        public CaseDataDTO CaseData { get; set; }
        public LambdaData LambdaData { get; set; }
    }

    public class LambdaData
    {
        public string Cloudwatch_LogGroupName { get; set; }
        public string Cloudwatch_LogStreamName { get; set; }
        public string RequestId { get; set; }
        public string ARN { get; set; }
    }
    public class EventDataDTO
    {
        /// <summary>
        /// Source of the Event i.e. AgeroDashboard/ServiceBus/etc
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Some Unique Id of the source system.
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// Roadside request type i.e Digital/NonDigital/etc
        /// </summary>
        public string RequestType { get; set; }

        /// <summary>
        /// Touchless/NonTouchless
        /// </summary>
        public string RequestChannel { get; set; }

        /// <summary>
        /// Status when the event is triggered i.e. RequestConfirmed
        /// </summary>
        public string RequestStatus { get; set; }

    }
    public class CustomerDTO
    {
        public string CallbackRefNumber { get; set; }
        public string NotificationNumber { get; set; }
        public string NotificationPreference { get; set; }
        public bool? OptInFlag { get; set; }
    }

    public class CaseDataDTO
    {
        public long CaseId { get; set; }
        public short TaskId { get; set; }
        public short ClientId { get; set; }
        public string Status { get; set; }
    }
}
