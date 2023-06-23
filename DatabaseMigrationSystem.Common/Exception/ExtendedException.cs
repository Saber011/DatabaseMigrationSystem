using System;
using System.Collections.Generic;
using DatabaseMigrationSystem.Common;

namespace Action.Platform.Common.Exceptions
{
    /// <summary>
    /// Extended exception with additional info.
    /// </summary>
    public class ExtendedException: CommonException
    {
        /// <summary>
        /// Additional info about exception.
        /// </summary>
        public IDictionary<string, string> AdditionalData { get; protected set; }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="innerException">Inner exception.</param>
        /// <param name="additionalData">Additional info about exception.</param>
        public ExtendedException(string message,
                                 Exception innerException,
                                 IDictionary<string, string> additionalData) : base(message, innerException)
        {
            AdditionalData = additionalData;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public ExtendedException(string message) : this(message, null , null)
        {

        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="innerException">Inner exception.</param>
        public ExtendedException(string message,
                                 Exception innerException) : this(message, innerException, null)
        {

        }
    }
}
