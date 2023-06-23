using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using DatabaseMigrationSystem.Common;


namespace Action.Platform.Common.Exceptions
{
    /// <summary>
    /// Broken rules exception.
    /// </summary>
    /// <remarks>Ошибки валидации и нарушения логики.</remarks>
    [Serializable]
    public class BrokenRulesException : ExtendedException
    {
        private readonly ReadOnlyCollection<BrokenRule> _brokenRules;

        private readonly string _brokenRulesSeparator;


        /// <summary>
        /// Gets list of broken rules.
        /// </summary>
        [NotNull]
        public ReadOnlyCollection<BrokenRule> BrokenRules => _brokenRules;


        /// <inheritdoc />
        public override string Message
        {
            get
            {
                return string.Join(_brokenRulesSeparator, BrokenRules.Select(x => x.Message));
            }
        }


        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="brokenRulesCollection">Broken rules collection.</param>
        /// <param name="brokenRulesSeparator">Broken rules separator.</param>
        /// <param name="innerException">Inner exception.</param>
        public BrokenRulesException([NotNull] BrokenRulesCollection brokenRulesCollection,
                                    [NotNull] string brokenRulesSeparator = "\n",
                                     Exception innerException = null)
            : base("Broken rules validation exception.",
                   innerException)
        {

            _brokenRules = brokenRulesCollection.ToReadOnlyCollection();
            _brokenRulesSeparator = brokenRulesSeparator;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="brokenRuleMessage">Broken rule message.</param>
        /// <param name="brokenRuleCode">Broken rule code.</param>
        /// <param name="brokenRulesSeparator">Broken rules separator.</param>
        /// <param name="innerException">Inner exception.</param>
        public BrokenRulesException([NotNull] string brokenRuleMessage,
                                    long brokenRuleCode = 0,
                                    [NotNull] string brokenRulesSeparator = "\n",
                                     Exception innerException = null)
            : this(new BrokenRulesCollection(brokenRuleMessage, brokenRuleCode),
                   brokenRulesSeparator,
                   innerException) { }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="brokenRule">Broken rule.</param>
        /// <param name="brokenRulesSeparator">Broken rules separator.</param>
        /// <param name="innerException">Inner exception.</param>
        public BrokenRulesException([NotNull] BrokenRule brokenRule,
                                    [NotNull] string brokenRulesSeparator = "\n",
                                    Exception innerException = null)
            : this(new BrokenRulesCollection(brokenRule),
                   brokenRulesSeparator,
                   innerException) { }
        
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="brokenRule">Broken rule.</param>
        /// <param name="innerException">Inner exception.</param>
        public BrokenRulesException([NotNull] BrokenRule brokenRule,
                                     Exception innerException)
            : this(new BrokenRulesCollection(brokenRule),
                   innerException: innerException)
        { }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="brokenRule">Broken rule.</param>
        /// <param name="additionalData">additional info about exception.</param>
        public BrokenRulesException([NotNull] BrokenRule brokenRule,
                                     IDictionary<string, string> additionalData)
            : this(brokenRule)
        {
            AdditionalData = additionalData;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="brokenRule">Broken rule.</param>
        /// <param name="innerException">Inner exception.</param>
        /// <param name="additionalData">additional info about exception.</param>
        public BrokenRulesException([NotNull] BrokenRule brokenRule,
                                     Exception innerException,
                                     IDictionary<string, string> additionalData)
            : this(new BrokenRulesCollection(brokenRule),
                   innerException: innerException)
        {
            AdditionalData = additionalData;
        }
    }
}