using Devon4Net.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Devon4Net.WebAPI.Implementation.Business.JumpTheQueueManagement.Exceptions
{
    /// <summary>
    /// Custom exception QueueNotFoundException
    /// </summary>
    [Serializable]
    public class QueueNotFoundException : Exception, IWebApiException
    {
        /// <summary>
        /// The forced http status code to be fired on the exception manager
        /// </summary>
        public int StatusCode => StatusCodes.Status404NotFound;

        /// <summary>
        /// Show the message on the response?
        /// </summary>
        public bool ShowMessage => true;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueNotFoundException"/> class.
        /// </summary>
        public QueueNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public QueueNotFoundException(string message)
            : base(message)
        {
        }
    }
}
