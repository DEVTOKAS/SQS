namespace SQS.ServiceBusQueue.Messages
{
    /// <summary>
    /// The class for all the constants strings of queue message.
    /// </summary>
    public static class MessageConstants
    {
        /// <summary>
        /// Message label: QueueBuild.
        /// </summary>
        public const string BuildMessageLabel = "QueueBuild";

        /// <summary>
        /// Message label: QueueBuild.
        /// </summary>
        public const string QueueName = "SQSQueue";


        /// <summary>
        /// Content type for message: application/json.
        /// </summary>
        public const string ContentType = "application/json";

        /// <summary>
        /// The message for string parameter is null or empty.
        /// </summary>
        public const string NullOrEmptyMessage = "Parameter cannot be null or empty.";

        /// <summary>
        /// The message for object parameter is null.
        /// </summary>
        public const string NullMessage = "Parameter cannot be null.";
    }
}
