
namespace SQS.ServiceBusQueue.Interface
{
    using SQS.ServiceBusQueue.Messages;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The interface for queue operations: send message and receive message.
    /// </summary>
    public interface IQueueOperator
    {
        /// <summary>
        /// Send message to queue in Service Bus.
        /// </summary>
        /// <param name="connectionString">The connection string of Service Bus.</param>
        /// <param name="queueName">The name of a queue in Service Bus.</param>
        /// <param name="jsonData">The JSON string of a message.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task SendMessagesAsync(string connectionString, string queueName, string jsonData);

        /// <summary>
        /// Send message to queue in Service Bus.
        /// </summary>
        /// <param name="connectionString">The connection string of Service Bus.</param>
        /// <param name="queueName">The name of a queue in Service Bus.</param>
        /// <param name="jsonData">The JSON string of a message.</param>
        /// <param name="messageId">The message Id to create a new message object which is used to communicate and transfer data with Service Bus.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task SendMessagesAsync(string connectionString, string queueName, List<BuildQueueModel> QueueData );

        /// <summary>
        /// Receive a message from queue in Service Bus.
        /// </summary>
        /// <param name="connectionString">The connection string of Service Bus.</param>
        /// <param name="queueName">The name of a queue in Service Bus.</param>
        /// <param name="numberofConcurrentMessages">no of message to process in batch</param>
        /// <returns></returns>
        Task ReceiveMessagesAsync(string connectionString, string queueName, int numberofConcurrentMessages);
    }
}