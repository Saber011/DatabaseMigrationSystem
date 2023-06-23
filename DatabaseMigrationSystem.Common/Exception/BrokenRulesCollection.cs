using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace DatabaseMigrationSystem.Common
{
    /// <summary>
    /// Broken rules collection.
    /// </summary>
    public sealed class BrokenRulesCollection
    {
        private readonly List<BrokenRule> _brokenRules = new List<BrokenRule>();


        /// <summary>
        /// Gets valid flag.
        /// </summary>
        /// <returns>True, if collection empty, otherwise, false.</returns>
        public bool IsValid => _brokenRules.Count == 0;


        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public BrokenRulesCollection() { }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message">Broken rule message.</param>
        /// <param name="code">Broken rule code.</param>
        public BrokenRulesCollection([NotNull] string message,
                                     long code = 0)
            : this()
        {
            AddBrokenRule(message, code);
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="brokenRule">Broken rule.</param>
        public BrokenRulesCollection([NotNull] BrokenRule brokenRule)
            : this()
        {
            AddBrokenRule(brokenRule);
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="brokenRules">Broken rules.</param>
        public BrokenRulesCollection([NotNull] IEnumerable<BrokenRule> brokenRules)
            : this()
        {
            AddBrokenRules(brokenRules);
        }


        /// <summary>
        /// Adds new broken rules to collection.
        /// </summary>
        /// <param name="brokenRules">Broken rules.</param>
        public void AddBrokenRules([NotNull] IEnumerable<BrokenRule> brokenRules)
        {
            _brokenRules.AddRange(brokenRules);
        }

        /// <summary>
        /// Adds new broken rule to collection.
        /// </summary>
        /// <param name="message">Broken rule message.</param>
        /// <param name="code">Broken rule code.</param>
        public void AddBrokenRule([NotNull] string message,
                                  long code = 0)
        {

            _brokenRules.Add(new BrokenRule(message, code));
        }

        /// <summary>
        /// Adds new broken rule to collection.
        /// </summary>
        /// <param name="brokenRule">Broken rule.</param>
        public void AddBrokenRule([NotNull] BrokenRule brokenRule)
        {
            _brokenRules.Add(brokenRule);
        }

        /// <summary>
        /// Clears the broken rule collection.
        /// </summary>
        public void Clear()
        {
            _brokenRules.Clear();
        }

        /// <summary>
        /// Gets read-only copy of list of broken rules.
        /// </summary>
        /// <returns>Read only collection of broken rules.</returns>
        public ReadOnlyCollection<BrokenRule> ToReadOnlyCollection()
        {
            return _brokenRules.AsReadOnly();
        }

        /// <summary>
        /// Returns a string of concatinated broken rule messages.
        /// </summary>
        /// <param name="separator">Separator between broken rules messages.</param>
        /// <returns>String representation  of collection.</returns>
        public string ToString([NotNull] string separator)
        {

            if (IsValid)
            {
                return null;
            }

            return string.Join(separator, _brokenRules.Select(x => x.Message));
        }

        /// <inheritdoc />
        public override string ToString()
        {
            // ReSharper disable AssignNullToNotNullAttribute
            return ToString(". ");
            // ReSharper restore AssignNullToNotNullAttribute
        }
    }
}