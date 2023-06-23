using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace DatabaseMigrationSystem.Common
{
    /// <summary>
    /// Broken rule.
    /// </summary>
    [DataContract]
    public sealed class BrokenRule
    {
        /// <summary>
        /// Gets a message of rule.
        /// </summary>
        [DataMember(Name = "message")]
        [Required]
        [NotNull]
        public string Message { get; private set; }

        /// <summary>
        /// Gets a code of rule.
        /// </summary>
        [DataMember(Name = "code")]
        [Required]
        public long Code { get; private set; }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Rule message.</param>
        /// <param name="code">Rule code.</param>
        public BrokenRule([NotNull] string message,
                          long code = 0)
        {
            Message = message;
            Code = code;
        }
    }
}