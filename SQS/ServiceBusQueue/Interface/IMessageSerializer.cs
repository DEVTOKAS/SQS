
namespace SQS.ServiceBusQueue.Interface
{
    /// <summary>
    /// The interface of MessageSerializer, provide Deserialize and Serialize methods.
    /// </summary>
    public interface IMessageSerializer
    {
        /// <summary>
        /// Deserialize the input message to an object T.
        /// </summary>
        /// <typeparam name="T">Object instance.</typeparam>
        /// <param name="message">The message string from queue.</param>
        /// <returns>Object instance from deserialize.</returns>
        T Deserialize<T>(string message);

        /// <summary>
        /// Serialize an object to a string.
        /// </summary>
        /// <param name="obj">Object instance to serialize.</param>
        /// <returns>Serialized string.</returns>
        string Serialize(object obj);
    }
}
