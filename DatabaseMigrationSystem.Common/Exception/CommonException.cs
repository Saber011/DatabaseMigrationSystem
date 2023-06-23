namespace DatabaseMigrationSystem.Common
{
    /// <summary>
    /// Common exception.
    /// </summary>
    public class CommonException : Exception
    {
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner exception.</param>
        public CommonException(string message = null,
                               Exception innerException = null)
            : base(message, innerException) { }
    }
}