using System;

namespace BoothItems
{
    /// <summary>
    /// The exception which is used to represent a missing item error.
    /// </summary>
    public class MissingItemException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the MissingItemException class.
        /// </summary>
        /// <param name="message">The message for the exception.</param>
        public MissingItemException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the MissingItemException class.
        /// </summary>
        /// <param name="message">The message for the exception.</param>
        /// <param name="innerException">The inner exception.</param>
        public MissingItemException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
