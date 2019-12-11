using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SQS.Helper;
using SQS.ServiceBusQueue;
using SQS.ServiceBusQueue.Interface;
using SQS.ServiceBusQueue.Messages;
using System;
using System.Collections.Generic;

namespace SQS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SQSController : ControllerBase
    {
        /// <summary>
        /// Send List of Messages to Azure Service Bus
        /// </summary>
        /// <param name="value"></param>
        // POST: api/SQS
        [HttpPost]
        public void Post([FromBody] string value)
        {
            string ServiceBusConnectionString = Common.GetSecret().Result;
            string QueueName = QueueRoutes.QueueBuild;
            var messages = JsonConvert.DeserializeObject<List<BuildQueueModel>>(value);

            if (messages is null)
            {
                throw new ArgumentException(MessageConstants.NullOrEmptyMessage, nameof(messages));
            }
            IQueueOperator queueOperator = new QueueOperator();
            queueOperator.SendMessagesAsync(ServiceBusConnectionString, QueueName, messages);

        }

        /// <summary>
        /// Batch Processing of Messages
        /// </summary>
        // GET: api/SQS
        [HttpGet]
        public void ProcessMessages()
        {
            string ServiceBusConnectionString = Common.GetSecret().Result;
            string QueueName = QueueRoutes.QueueBuild;
            IQueueOperator queueOperator = new QueueOperator();
            queueOperator.ReceiveMessagesAsync(ServiceBusConnectionString, QueueName, 10);

        }




    }
}
