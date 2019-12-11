namespace SQS.ServiceBusQueue
{
    using Microsoft.Azure.ServiceBus;
    using Newtonsoft.Json;
    using SQS.Helper;
    using SQS.ServiceBusQueue.Interface;
    using SQS.ServiceBusQueue.Messages;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The class for queue operations: send message and receive message.
    /// </summary>
    public class QueueOperator : IQueueOperator
    {
        /// <summary>
        /// Send message to queue in Service Bus.
        /// </summary>
        /// <param name="connectionString">The connection string of Service Bus.</param>
        /// <param name="queueName">The name of a queue in Service Bus.</param>
        /// <param name="jsonData">The JSON string of a message.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task SendMessagesAsync(string connectionString, string queueName, string jsonData)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException(MessageConstants.NullOrEmptyMessage, nameof(connectionString));
            }

            if (string.IsNullOrEmpty(queueName))
            {
                throw new ArgumentException(MessageConstants.NullOrEmptyMessage, nameof(queueName));
            }

            if (string.IsNullOrEmpty(jsonData))
            {
                throw new ArgumentException(MessageConstants.NullOrEmptyMessage, nameof(jsonData));
            }

            var sender = new QueueClient(connectionString, queueName); ;// new MessageSender(connectionString, queueName);
            string label = MessageConstants.BuildMessageLabel;
               

            var message = new Message(Encoding.UTF8.GetBytes(jsonData))
            {
                ContentType = MessageConstants.ContentType,
                Label = label,
                MessageId = Guid.NewGuid().ToString(),
                TimeToLive = TimeSpan.FromMinutes(100),
            };

            await sender.SendAsync(message);

            // close the connection.
            await sender.CloseAsync();
        }

        /// <summary>
        /// Send message to queue in Service Bus.
        /// </summary>
        /// <param name="connectionString">The connection string of Service Bus.</param>
        /// <param name="queueName">The name of a queue in Service Bus.</param>
        /// <param name="jsonData">The JSON string of a message.</param>
        /// <param name="messageId">The message Id to create a new message object which is used to communicate and transfer data with Service Bus.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task SendMessagesAsync(string connectionString, string queueName, List<BuildQueueModel> QueueData)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException(MessageConstants.NullOrEmptyMessage, nameof(connectionString));
            }

            if (string.IsNullOrEmpty(queueName))
            {
                throw new ArgumentException(MessageConstants.NullOrEmptyMessage, nameof(queueName));
            }

            if (QueueData is null)
            {
                throw new ArgumentException(MessageConstants.NullOrEmptyMessage, nameof(QueueData));
            }

            var sender = new QueueClient(connectionString, queueName); ;
            string label = MessageConstants.BuildMessageLabel;
            List<Message> messages = null;
            foreach (var messagedata in QueueData)
            {
                var message = new Message(Encoding.UTF8.GetBytes(messagedata.StoredData))
                {
                    ContentType = MessageConstants.ContentType,
                    Label = label,
                    TimeToLive = TimeSpan.FromMinutes(100),
                };
                messages.Add(message);
            }

            await sender.SendAsync(messages);

            // close the connection.
            await sender.CloseAsync();
        }

        /// <summary>
        /// Receive a message from queue in Service Bus.
        /// </summary>
        public async Task ReceiveMessagesAsync(string connectionString, string queueName, int numberofConcurrentMessages)
        {
            // Configure the MessageHandler Options in terms of exception handling, number of concurrent messages to deliver etc.
            try
            {
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new ArgumentException(MessageConstants.NullOrEmptyMessage, nameof(connectionString));
                }

                if (string.IsNullOrEmpty(queueName))
                {
                    throw new ArgumentException(MessageConstants.NullOrEmptyMessage, nameof(queueName));
                }

               

                var queueClient = new QueueClient(connectionString, queueName); ;

                queueClient.RegisterMessageHandler(
                    async (message, token) =>
                    {
                        string test = Encoding.UTF8.GetString(message.Body);

                        var messageJson = Deserialize<BuildQueueModel>(test);
                        //var updateMessage = JsonConvert.DeserializeObject<ProductRatingUpdateMessage>(messageJson);

                        Console.WriteLine($"Received message with Message: {messageJson}");

                        await queueClient.CompleteAsync(message.SystemProperties.LockToken);
                    },
                     new MessageHandlerOptions((e) => this.LogMessageHandlerException(e)) { AutoComplete = false, MaxConcurrentCalls = numberofConcurrentMessages });
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

        }

        private Task LogMessageHandlerException(ExceptionReceivedEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e), MessageConstants.NullMessage);
            }

            Console.WriteLine("Exception: \"{0}\" {0}", e.Exception.Message, e.ExceptionReceivedContext.EntityPath);
            return Task.CompletedTask;
        }

        public static T Deserialize<T>(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException("Null Message", nameof(message));
            }
            string decompressed = message.ToDecompressed();
            var obj = JsonConvert.DeserializeObject<T>(decompressed);
            return obj;
        }
    }
}
