using Devon4Net.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Devon4Net.WebAPI.Implementation.Business.JumpTheQueueManagement.Exceptions
{
    /// <summary>
    /// Custom exception VisitorNotFoundException
    /// </summary>
    [Serializable]
    public class VisitorNotFoundException : Exception, IWebApiException
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
        /// Initializes a new instance of the <see cref="VisitorNotFoundException"/> class.
        /// </summary>
        public VisitorNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VisitorNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public VisitorNotFoundException(string message)
            : base(message)
        {
        }
    }
}
