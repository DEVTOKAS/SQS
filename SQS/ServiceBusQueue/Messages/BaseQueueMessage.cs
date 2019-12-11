
namespace SQS.ServiceBusQueue.Messages
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Base class for queue message model.
    /// </summary>
    public class BaseQueueMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseQueueMessage"/> class.
        /// </summary>
        /// <param name="route">the queue name for the message to send or receive.</param>
        public BaseQueueMessage(string route)
        {
            if (string.IsNullOrEmpty(route))
            {
                throw new ArgumentNullException(nameof(route), MessageConstants.NullOrEmptyMessage);
            }

            this.Route = route;
        }

        /// <summary>
        /// Gets or sets set the queue name for send or receive message.
        /// </summary>
        [JsonIgnore]
        public string Route { get; set; }
    }
}
