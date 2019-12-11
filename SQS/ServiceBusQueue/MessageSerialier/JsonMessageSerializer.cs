// <copyright file="JsonMessageSerializer.cs" company="Microsoft Inc.">
// Copyright (c) Microsoft Inc.. All rights reserved.
// </copyright>

namespace SQS.ServiceBusQueue.MessageSerialier
{
    using System;
    using Newtonsoft.Json;
    using SQS.ServiceBusQueue.Infrastructure;
    using SQS.ServiceBusQueue.Interface;
    using SQS.ServiceBusQueue.Messages;

    /// <summary>
    /// The class for provide Deserialize and Serialize methods.
    /// </summary>
    public class JsonMessageSerializer : IMessageSerializer
    {
        /// <summary>
        /// Deserialize the input message to an object T.
        /// </summary>
        /// <typeparam name="T">Object instance.</typeparam>
        /// <param name="message">The message string from queue.</param>
        /// <returns>Object instance from deserialize.</returns>
        public T Deserialize<T>(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException(MessageConstants.NullOrEmptyMessage, nameof(message));
            }

            var obj = JsonConvert.DeserializeObject<T>(message.ToDecompressed());
            return obj;
        }

        /// <summary>
        /// Serialize an object to a string.
        /// </summary>
        /// <param name="obj">Object instance to serialize.</param>
        /// <returns>Serialized string.</returns>
        public string Serialize(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj), MessageConstants.NullMessage);
            }

            var message = JsonConvert.SerializeObject(obj);
            return message.ToCompressed();
        }
    }
}
