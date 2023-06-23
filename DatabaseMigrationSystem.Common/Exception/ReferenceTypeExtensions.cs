using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Action.Platform.Common.Exceptions;

namespace DatabaseMigrationSystem.Common
{
    /// <summary>
    /// Reference type extensions.
    /// </summary>
    public static class ReferenceTypeExtensions
    {
        #region general

        /// <summary>
        /// Checks instance to null.
        /// </summary>
        /// <typeparam name="T">Type of the obj.</typeparam>
        /// <param name="instance">Instance.</param>
        /// <returns>True if specified class is null, otherwise, false.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNull<T>( this T instance) where T : class
        {
            return instance is null;
        }

        /// <summary>
        /// Checks if instance is not null.
        /// </summary>
        /// <typeparam name="T">Type of the instance.</typeparam>
        /// <param name="instance">Instance.</param>
        /// <returns>True if specified class is not null, otherwise, false.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNotNull<T>(this T instance) where T : class
        {
            return !(instance is null);
        }

        #endregion

        #region required - RequiredNotNull

      
        /// <summary>
        /// Throw <see cref="BrokenRulesException" /> if instance is null.
        /// </summary>
        /// <typeparam name="T">Type of the obj.</typeparam>
        /// <param name="instance">Instance.</param>
        /// <param name="brokenRule"><see cref="BrokenRule"/></param>
        /// <exception cref="BrokenRulesException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiredNotNullAsBrokenRules<T>(this T instance,
                                                            BrokenRule brokenRule) where T : class
        {
            if (instance.IsNull())
            {
                throw new BrokenRulesException(brokenRule);
            }
        }

        /// <summary>
        /// Throw <see cref="BrokenRulesException" /> if instance is null.
        /// </summary>
        /// <typeparam name="T">Type of the obj.</typeparam>
        /// <param name="instance">Instance.</param>
        /// <param name="exceptionMessage">Exception message.</param>
        /// <param name="brokenRuleCode">Broken rule code.</param>
        /// <exception cref="BrokenRulesException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiredNotNullAsBrokenRules<T>( this T instance,
                                                           [NotNull] string exceptionMessage,
                                                           long brokenRuleCode = 0) where T : class
        {
            if (instance.IsNull())
            {
                throw new BrokenRulesException(string.IsNullOrEmpty(exceptionMessage)
                                                   ? "Argument is null."
                                                   : exceptionMessage,
                                               brokenRuleCode);
            }
        }

        #endregion
    }
}