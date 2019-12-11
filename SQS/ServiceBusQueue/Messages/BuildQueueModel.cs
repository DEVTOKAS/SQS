namespace SQS.ServiceBusQueue.Messages
{
    /// <summary>
    /// The entity class for queue build.
    /// </summary>
    public class BuildQueueModel : BaseQueueMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildQueueModel"/> class.
        /// </summary>
        public BuildQueueModel()
            : base(QueueRoutes.QueueBuild)
        {
        }

        /// <summary>
        /// Gets or sets set the the ExecutionId.
        /// </summary>
        public string StoredData { get; set; }

        
    }
}
