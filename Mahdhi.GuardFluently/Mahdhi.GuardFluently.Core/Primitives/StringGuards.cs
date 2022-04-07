//************************************************************************************
// Token [and personalized] from https://github.com/fluentassertions/fluentassertions
//************************************************************************************ 
using Microsoft.Toolkit.Diagnostics;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Mahdhi.GuardFluently.Core.Primitives
{
    /// <summary>
    /// Contains a number of methods to assert that a <see cref="string"/> is in the expected state.
    /// </summary>
    [DebuggerNonUserCode]
    public class StringGuards : StringGuards<StringGuards>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringGuards"/> class.
        /// </summary>
        public StringGuards(string value, [CallerArgumentExpression("value")] string name = "")
            : base(value, name)
        {
        }
    }

    /// <summary>
    /// Contains a number of methods to assert that a <see cref="string"/> is in the expected state.
    /// </summary>
    [DebuggerNonUserCode]
    public class StringGuards<TAssertions> : ReferenceTypeGuards<string, TAssertions>
         where TAssertions : StringGuards<TAssertions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringGuards{TAssertions}"/> class.
        /// </summary>
        public StringGuards(string value, [CallerArgumentExpression("value")] string name = "")
            : base(value, name)
        {
        }

        /// <summary>
        /// Asserts that a string is exactly the same as another string, including the casing and any leading or trailing whitespace.
        /// </summary>
        /// <param name="expected">The expected string.</param>
        public AndConstraint<TAssertions> Be(string expected)
        {
            Guard.IsEqualTo(Subject, expected, Name);

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that the <see cref="string"/> is one of the specified <paramref name="validValues"/>.
        /// </summary>
        /// <param name="validValues">
        /// The values that are valid.
        /// </param>
        public AndConstraint<TAssertions> BeOneOf(params string[] validValues)
        {
            return BeOneOf(validValues.ToList());
        }

        /// <summary>
        /// Asserts that the <see cref="string"/> is not one of the specified <paramref name="validValues"/>.
        /// </summary>
        /// <param name="validValues">
        /// The values that are valid.
        /// </param>
        public AndConstraint<TAssertions> NotBeOneOf(params string[] validValues)
        {
            return NotBeOneOf(validValues.ToList());
        }

        /// <summary>
        /// Asserts that the <see cref="string"/> is one of the specified <paramref name="validValues"/>.
        /// </summary>
        /// <param name="validValues">
        /// The values that are valid.
        /// </param>
        public AndConstraint<TAssertions> BeOneOf(IEnumerable<string> validValues)
        {
            if (validValues == null || !validValues.Any())
            {
                ThrowHelper.ThrowArgumentNullException(nameof(validValues), "Can not check with <null> or empty value.");
            }

            if (!validValues.Contains(Subject))
            {
                ThrowHelper.ThrowArgumentException($"Parameter \"{Name}\" ({typeof(string)}) must be one of {string.Join(", ", validValues)}.");
            }

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that the <see cref="string"/> is not one of the specified <paramref name="validValues"/>.
        /// </summary>
        /// <param name="validValues">
        /// The values that are valid.
        /// </param>
        public AndConstraint<TAssertions> NotBeOneOf(IEnumerable<string> validValues)
        {
            if (validValues == null || !validValues.Any())
            {
                ThrowHelper.ThrowArgumentNullException(nameof(validValues), "Can not check with <null> or empty value.");
            }

            if (validValues.Contains(Subject))
            {
                ThrowHelper.ThrowArgumentException($"Parameter \"{Name}\" ({typeof(string)}) must not be one of {string.Join(", ", validValues)}.");
            }

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that a string is exactly the same as another string, including any leading or trailing whitespace, with
        /// the exception of the casing.
        /// </summary>
        /// <param name="expected">
        /// The string that the subject is expected to be equivalent to.
        /// </param>
        public AndConstraint<TAssertions> BeEquivalentTo(string expected)
        {
            if (Subject?.ToLower()?.Trim() != expected?.ToLower()?.Trim())
            {
                ThrowHelper.ThrowArgumentException($"Parameter \"{Name}\" ({typeof(string)}) must be equivaent to '{expected}'.");
            }

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that a string is not exactly the same as another string, including any leading or trailing whitespace, with
        /// the exception of the casing.
        /// </summary>
        /// <param name="unexpected">
        /// The string that the subject is not expected to be equivalent to.
        /// </param>
        public AndConstraint<TAssertions> NotBeEquivalentTo(string unexpected)
        {
            if (Subject?.ToLower()?.Trim() == unexpected?.ToLower()?.Trim())
            {
                ThrowHelper.ThrowArgumentException($"Parameter \"{Name}\" ({typeof(string)}) must be not equivalent to '{unexpected}'.");
            }

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that a string is not exactly the same as the specified <paramref name="unexpected"/>,
        /// including the casing and any leading or trailing whitespace.
        /// </summary>
        /// <param name="unexpected">The string that the subject is not expected to be equivalent to.</param> 
        public AndConstraint<TAssertions> NotBe(string unexpected)
        {
            if (Subject?.Trim() == unexpected?.Trim())
            {
                ThrowHelper.ThrowArgumentException($"Parameter \"{Name}\" ({typeof(string)}) must not be '{unexpected}'.");
            }

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that a string matches the <paramref name="wildcardPattern"/>.
        /// </summary>
        /// <param name="wildcardPattern">
        /// The pattern to match against the subject. This parameter can contain a combination of literal text and wildcard
        /// (* and ?) characters, but it doesn't support regular expressions.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="wildcardPattern"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="wildcardPattern"/> is empty.</exception>
        public AndConstraint<TAssertions> Match(string wildcardPattern)
        {
            if (wildcardPattern == null)
            {
                ThrowHelper.ThrowArgumentNullException(nameof(wildcardPattern), "Cannot match string against <null>. Provide a wildcard pattern or use the NotBeNull method.");
            }

            if (wildcardPattern.Length == 0)
            {
                ThrowHelper.ThrowArgumentException(nameof(wildcardPattern), "Cannot match string against an empty string. Provide a wildcard pattern or use the NotBeEmpty method.");
            }

            Guard.IsNotNull(Subject, Name);
            //TO DO : change with Guard.Match method proposed with my issue #196 if it is approved and merged
            // https://github.com/CommunityToolkit/dotnet/issues/196

            if (!Regex.IsMatch(Subject, WildCardToRegular(wildcardPattern)))
            {
                ThrowHelper.ThrowArgumentException($"Parameter {Name} ({typeof(string).ToTypeString()}) must match the given wildcard pattern.");
            }

            return new AndConstraint<TAssertions>((TAssertions)this);

        }

        /// <summary>
        /// Asserts that a string does not match the <paramref name="wildcardPattern"/>.
        /// </summary>
        /// <param name="wildcardPattern">
        /// The pattern to match against the subject. This parameter can contain a combination literal text and wildcard of
        /// (* and ?) characters, but it doesn't support regular expressions.
        /// </param> 
        /// <exception cref="ArgumentNullException"><paramref name="wildcardPattern"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="wildcardPattern"/> is empty.</exception>
        public AndConstraint<TAssertions> NotMatch(string wildcardPattern)
        {
            if (wildcardPattern == null)
            {
                ThrowHelper.ThrowArgumentException(nameof(wildcardPattern), "Cannot match string against <null>. Provide a wildcard pattern or use the NotBeNull method.");
            }

            if (wildcardPattern.Length == 0)
            {
                ThrowHelper.ThrowArgumentException(nameof(wildcardPattern), "Cannot match string against an empty string. Provide a wildcard pattern or use the NotBeEmpty method.");
            }

            Guard.IsNotNull(Subject, Name);

            //TO DO : change with Guard.Match method proposed with my issue #196 if it is approved and merged
            // https://github.com/CommunityToolkit/dotnet/issues/196

            if (Regex.IsMatch(Subject, WildCardToRegular(wildcardPattern)))
            {
                ThrowHelper.ThrowArgumentException($"Parameter {Name} ({typeof(string).ToTypeString()}) must not match the given wildcard pattern.");
            }

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that a string matches the <paramref name="wildcardPattern"/>.
        /// </summary>
        /// <param name="wildcardPattern">
        /// The pattern to match against the subject. This parameter can contain a combination of literal text and wildcard
        /// (* and ?) characters, but it doesn't support regular expressions.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="wildcardPattern"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="wildcardPattern"/> is empty.</exception>
        public AndConstraint<TAssertions> MatchEquivalentOf(string wildcardPattern)
        {
            if (wildcardPattern == null)
            {
                ThrowHelper.ThrowArgumentException(nameof(wildcardPattern), "Cannot match string against <null>. Provide a wildcard pattern or use the NotBeNull method.");
            }

            if (wildcardPattern.Length == 0)
            {
                ThrowHelper.ThrowArgumentException(nameof(wildcardPattern), "Cannot match string against an empty string. Provide a wildcard pattern or use the NotBeEmpty method.");
            }

            Guard.IsNotNull(Subject, Name);

            //TO DO : change with Guard.Match method proposed with my issue #196 if it is approved and merged
            // https://github.com/CommunityToolkit/dotnet/issues/196

            if (!Regex.IsMatch(Subject.ToLower().Trim(), WildCardToRegular(wildcardPattern.ToLower().Trim())))
            {
                ThrowHelper.ThrowArgumentException($"Parameter {Name} ({typeof(string).ToTypeString()}) must match the given wildcard pattern.");
            }

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that a string does not match the <paramref name="wildcardPattern"/>.
        /// </summary>
        /// <param name="wildcardPattern">
        /// The pattern to match against the subject. This parameter can contain a combination of literal text and wildcard
        /// (* and ?) characters, but it doesn't support regular expressions.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="wildcardPattern"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="wildcardPattern"/> is empty.</exception>
        public AndConstraint<TAssertions> NotMatchEquivalentOf(string wildcardPattern, string because = "",
            params object[] becauseArgs)
        {
            if (wildcardPattern == null)
            {
                ThrowHelper.ThrowArgumentException(nameof(wildcardPattern), "Cannot match string against <null>. Provide a wildcard pattern or use the NotBeNull method.");
            }

            if (wildcardPattern.Length == 0)
            {
                ThrowHelper.ThrowArgumentException(nameof(wildcardPattern), "Cannot match string against an empty string. Provide a wildcard pattern or use the NotBeEmpty method.");
            }

            Guard.IsNotNull(Subject, Name);
            //TO DO : change with Guard.Match method proposed with my issue #196 if it is approved and merged
            // https://github.com/CommunityToolkit/dotnet/issues/196

            if (Regex.IsMatch(Subject.ToLower().Trim(), WildCardToRegular(wildcardPattern.ToLower().Trim())))
            {
                ThrowHelper.ThrowArgumentException($"Parameter {Name} ({typeof(string).ToTypeString()}) must not match the given wildcard pattern.");
            }

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that a string matches a regular expression.
        /// </summary>
        /// <param name="regularExpression">
        /// The regular expression with which the subject is matched.
        /// </param>
        public AndConstraint<TAssertions> MatchRegex(string regularExpression)
        {
            Regex regex;
            try
            {
                regex = new Regex(regularExpression);
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }

            return MatchRegex(regex);
        }

        /// <summary>
        /// Asserts that a string matches a regular expression.
        /// </summary>
        /// <param name="regularExpression">
        /// The regular expression with which the subject is matched.
        /// </param>
        public AndConstraint<TAssertions> MatchRegex(Regex regularExpression)
        {
            if (regularExpression == null)
            {
                ThrowHelper.ThrowArgumentException(nameof(regularExpression), "Cannot match string against <null>. Provide a regex expression or use the NotBeNull method.");
            }
            string regEx = regularExpression.ToString();
            if (regEx.Length == 0)
            {
                ThrowHelper.ThrowArgumentException(nameof(regularExpression), "Cannot match string against an empty string. Provide a expression pattern or use the NotBeEmpty method.");
            }

            Guard.IsNotNull(Subject, Name);

            if (!regularExpression.IsMatch(Subject))
            {
                ThrowHelper.ThrowArgumentException($"Parameter {Name} ({typeof(string).ToTypeString()}) must match the given regular expression.");
            }

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that a string does not match a regular expression.
        /// </summary>
        /// <param name="regularExpression">
        /// The regular expression with which the subject is matched.
        /// </param>
        public AndConstraint<TAssertions> NotMatchRegex(string regularExpression)
        {
            if (regularExpression == null)
            {
                ThrowHelper.ThrowArgumentException(nameof(regularExpression), "Cannot match string against <null>. Provide a regex expression or use the NotBeNull method.");
            }

            if (regularExpression.Length == 0)
            {
                ThrowHelper.ThrowArgumentException(nameof(regularExpression), "Cannot match string against an empty string. Provide a expression pattern or use the NotBeEmpty method.");
            }

            Guard.IsNotNull(Subject, Name);

            if (Regex.IsMatch(Subject, regularExpression))
            {
                ThrowHelper.ThrowArgumentException($"Parameter {Name} ({typeof(string).ToTypeString()}) must not match the given regular expression.");
            }

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that a string does not match a regular expression.
        /// </summary>
        /// <param name="regularExpression">
        /// The regular expression with which the subject is matched.
        /// </param>
        public AndConstraint<TAssertions> NotMatchRegex(Regex regularExpression)
        {
            if (regularExpression == null)
            {
                ThrowHelper.ThrowArgumentException(nameof(regularExpression), "Cannot match string against <null>. Provide a regex expression or use the NotBeNull method.");
            }
            string regEx = regularExpression.ToString();
            if (regEx.Length == 0)
            {
                ThrowHelper.ThrowArgumentException(nameof(regularExpression), "Cannot match string against an empty string. Provide a expression pattern or use the NotBeEmpty method.");
            }

            Guard.IsNotNull(Subject, Name);

            if (regularExpression.IsMatch(Subject))
            {
                ThrowHelper.ThrowArgumentException($"Parameter {Name} ({typeof(string).ToTypeString()}) must not match the given regular expression.");
            }

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that a string starts exactly with the specified <paramref name="expected"/> value,
        /// including the casing and any leading or trailing whitespace.
        /// </summary>
        /// <param name="expected">The string that the subject is expected to start with.</param>
        public AndConstraint<TAssertions> StartWith(string expected)
        {
            if (expected == null)
            {
                ThrowHelper.ThrowArgumentException(nameof(expected), "Cannot compare start of string with <null>.");
            }

            Guard.IsNotNull(Subject, Name);

            if (!Subject.StartsWith(expected, StringComparison.Ordinal))
            {
                ThrowHelper.ThrowArgumentException($"Parameter {Name} ({typeof(string).ToTypeString()}) must contain a value that starts with '{expected}'.");
            }

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that a string does not start with the specified <paramref name="unexpected"/> value,
        /// including the casing and any leading or trailing whitespace.
        /// </summary>
        /// <param name="unexpected">The string that the subject is not expected to start with.</param>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <paramref name="because" />.
        /// </param>
        public AndConstraint<TAssertions> NotStartWith(string unexpected)
        {
            if (unexpected == null)
            {
                ThrowHelper.ThrowArgumentException(nameof(unexpected), "Cannot compare start of string with <null>.");
            }

            Guard.IsNotNull(Subject, Name);

            if (Subject.StartsWith(unexpected, StringComparison.Ordinal))
            {
                ThrowHelper.ThrowArgumentException($"Parameter {Name} ({typeof(string).ToTypeString()}) must contain a value that doesn't start with '{unexpected}'.");
            }

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that a string starts with the specified <paramref name="expected"/>,
        /// including any leading or trailing whitespace, with the exception of the casing.
        /// </summary>
        /// <param name="expected">The string that the subject is expected to start with.</param>
        public AndConstraint<TAssertions> StartWithEquivalentOf(string expected, string because = "",
            params object[] becauseArgs)
        {
            if (expected == null)
            {
                ThrowHelper.ThrowArgumentException(nameof(expected), "Cannot compare start of string with <null>.");
            }

            Guard.IsNotNull(Subject, Name);

            if (!Subject.Trim().StartsWith(expected.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                ThrowHelper.ThrowArgumentException($"Parameter {Name} ({typeof(string).ToTypeString()}) must contain a value that starts with '{expected}'.");
            }

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that a string does not start with the specified <paramref name="unexpected"/> value,
        /// including any leading or trailing whitespace, with the exception of the casing.
        /// </summary>
        /// <param name="unexpected">The string that the subject is not expected to start with.</param>
        public AndConstraint<TAssertions> NotStartWithEquivalentOf(string unexpected)
        {
            if (unexpected == null)
            {
                ThrowHelper.ThrowArgumentException(nameof(unexpected), "Cannot compare start of string with <null>.");
            }

            Guard.IsNotNull(Subject, Name);

            if (Subject.Trim().StartsWith(unexpected.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                ThrowHelper.ThrowArgumentException($"Parameter {Name} ({typeof(string).ToTypeString()}) must contain a value that doesn't start with '{unexpected}'.");
            }

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that a string ends exactly with the specified <paramref name="expected"/>,
        /// including the casing and any leading or trailing whitespace.
        /// </summary>
        /// <param name="expected">The string that the subject is expected to end with.</param>
        public AndConstraint<TAssertions> EndWith(string expected)
        {
            if (expected == null)
            {
                ThrowHelper.ThrowArgumentException(nameof(expected), "Cannot compare end of string with <null>.");
            }

            Guard.IsNotNull(Subject, Name);

            if (!Subject.EndsWith(expected, StringComparison.Ordinal))
            {
                ThrowHelper.ThrowArgumentException($"Parameter {Name} ({typeof(string).ToTypeString()}) must contain a value that ends with '{expected}'.");
            }

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that a string does not end exactly with the specified <paramref name="unexpected"/>,
        /// including the casing and any leading or trailing whitespace.
        /// </summary>
        /// <param name="unexpected">The string that the subject is not expected to end with.</param>
        public AndConstraint<TAssertions> NotEndWith(string unexpected)
        {
            if (unexpected == null)
            {
                ThrowHelper.ThrowArgumentException(nameof(unexpected), "Cannot compare end of string with <null>.");
            }

            Guard.IsNotNull(Subject, Name);

            if (Subject.EndsWith(unexpected, StringComparison.Ordinal))
            {
                ThrowHelper.ThrowArgumentException($"Parameter {Name} ({typeof(string).ToTypeString()}) must contain a value that doesn't end with '{unexpected}'.");
            }

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that a string ends with the specified <paramref name="expected"/>,
        /// including any leading or trailing whitespace, with the exception of the casing.
        /// </summary>
        /// <param name="expected">The string that the subject is expected to end with.</param>
        public AndConstraint<TAssertions> EndWithEquivalentOf(string expected)
        {
            if (expected == null)
            {
                ThrowHelper.ThrowArgumentException(nameof(expected), "Cannot compare end of string with <null>.");
            }

            Guard.IsNotNull(Subject, Name);

            if (!Subject.Trim().EndsWith(expected.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                ThrowHelper.ThrowArgumentException($"Parameter {Name} ({typeof(string).ToTypeString()}) must contain a value that ends with '{expected}'.");
            }

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that a string does not end with the specified <paramref name="unexpected"/>,
        /// including any leading or trailing whitespace, with the exception of the casing.
        /// </summary>
        /// <param name="unexpected">The string that the subject is not expected to end with.</param>
        public AndConstraint<TAssertions> NotEndWithEquivalentOf(string unexpected)
        {
            if (unexpected == null)
            {
                ThrowHelper.ThrowArgumentException(nameof(unexpected), "Cannot compare end of string with <null>.");
            }

            Guard.IsNotNull(Subject, Name);

            if (Subject.Trim().EndsWith(unexpected.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                ThrowHelper.ThrowArgumentException($"Parameter {Name} ({typeof(string).ToTypeString()}) must contain a value that doesn't end with '{unexpected}'.");
            }

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that a string contains another (fragment of a) string.
        /// </summary>
        /// <param name="expected">
        /// The (fragment of a) string that the current string should contain.
        /// </param> 
        public AndConstraint<TAssertions> Contain(string expected)
        {
            if (expected == null)
            {
                ThrowHelper.ThrowArgumentException(nameof(expected), "Cannot assert string containment against <null>.");
            }

            Guard.IsNotNull(Subject, Name);

            if (!Contains(Subject, expected, StringComparison.Ordinal))
            {
                ThrowHelper.ThrowArgumentException($"Parameter {Name} ({typeof(string).ToTypeString()}) must contain the string '{expected}'.");
            }

            return new AndConstraint<TAssertions>((TAssertions)this);

        }


        /// <summary>
        /// Asserts that a string contains the specified <paramref name="expected"/>,
        /// including any leading or trailing whitespace, with the exception of the casing.
        /// </summary>
        /// <param name="expected">The string that the subject is expected to contain.</param>
        public AndConstraint<TAssertions> ContainEquivalentOf(string expected)
        {
            if (expected == null)
            {
                ThrowHelper.ThrowArgumentException(nameof(expected), "Cannot assert string containment against <null>.");
            }

            Guard.IsNotNull(Subject, Name);

            if (!Contains(Subject.Trim(), expected.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                ThrowHelper.ThrowArgumentException($"Parameter {Name} ({typeof(string).ToTypeString()}) must contain a equivalent of the string '{expected}'.");
            }

            return new AndConstraint<TAssertions>((TAssertions)this);
        }


        /// <summary>
        /// Asserts that a string contains all values present in <paramref name="values"/>.
        /// </summary>
        /// <param name="values">
        /// The values that should all be present in the string
        /// </param>
        public AndConstraint<TAssertions> ContainAll(IEnumerable<string> values)
        {
            if (values == null || !values.Any())
            {
                ThrowHelper.ThrowArgumentException(nameof(values), "Cannot assert string containment against <null> or empty.");
            }

            Guard.IsNotNull(Subject, Name);

            if (!values.All(v => Contains(Subject, v, StringComparison.Ordinal)))
            {
                ThrowHelper.ThrowArgumentException($"Parameter {Name} ({typeof(string).ToTypeString()}) must contain all following values [{string.Join(", ", values.Select(i => $"'{i}'"))}].");
            }

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that a string contains all values present in <paramref name="values"/>.
        /// </summary>
        /// <param name="values">
        /// The values that should all be present in the string
        /// </param>
        public AndConstraint<TAssertions> ContainAll(params string[] values)
        {
            return ContainAll(values.ToList());
        }

        /// <summary>
        /// Asserts that a string contains at least one value present in <paramref name="values"/>,.
        /// </summary>
        /// <param name="values">
        /// The values that should will be tested against the string
        /// </param>
        public AndConstraint<TAssertions> ContainAny(IEnumerable<string> values)
        {
            if (values == null || !values.Any())
            {
                ThrowHelper.ThrowArgumentException(nameof(values), "Cannot assert string containment against <null> or empty.");
            }

            Guard.IsNotNull(Subject, Name);

            if (!values.Any(v => Contains(Subject, v, StringComparison.Ordinal)))
            {
                ThrowHelper.ThrowArgumentException($"Parameter {Name} ({typeof(string).ToTypeString()}) must contain any of following values [{string.Join(", ", values.Select(i => $"'{i}'"))}].");
            }

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that a string contains at least one value present in <paramref name="values"/>,.
        /// </summary>
        /// <param name="values">
        /// The values that should will be tested against the string
        /// </param>
        public AndConstraint<TAssertions> ContainAny(params string[] values)
        {
            return ContainAny(values.ToList());
        }

        /// <summary>
        /// Asserts that a string does not contain another (fragment of a) string.
        /// </summary>
        /// <param name="unexpected">
        /// The (fragment of a) string that the current string should not contain.
        /// </param>
        public AndConstraint<TAssertions> NotContain(string unexpected)
        {
            if (unexpected == null)
            {
                ThrowHelper.ThrowArgumentException(nameof(unexpected), "Cannot assert string containment against <null>.");
            }

            Guard.IsNotNull(Subject, Name);

            if (Contains(Subject, unexpected, StringComparison.Ordinal))
            {
                ThrowHelper.ThrowArgumentException($"Parameter {Name} ({typeof(string).ToTypeString()}) must not contain the string '{unexpected}'.");
            }

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that a string does not contain all of the strings provided in <paramref name="values"/>. The string
        /// may contain some subset of the provided values.
        /// </summary>
        /// <param name="values">
        /// The values that should not be present in the string
        /// </param>
        public AndConstraint<TAssertions> NotContainAll(IEnumerable<string> values)
        {
            if (values == null || !values.Any())
            {
                ThrowHelper.ThrowArgumentException(nameof(values), "Cannot assert string containment against <null> or empty.");
            }

            Guard.IsNotNull(Subject, Name);

            if (values.All(v => Contains(Subject, v, StringComparison.Ordinal)))
            {
                ThrowHelper.ThrowArgumentException($"Parameter {Name} ({typeof(string).ToTypeString()}) must not contain all following values [{string.Join(", ", values.Select(i => $"'{i}'"))}].");
            }

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that a string does not contain all of the strings provided in <paramref name="values"/>. The string
        /// may contain some subset of the provided values.
        /// </summary>
        /// <param name="values">
        /// The values that should not be present in the string
        /// </param>
        public AndConstraint<TAssertions> NotContainAll(params string[] values)
        {
            return NotContainAll(values.ToList());
        }

        /// <summary>
        /// Asserts that a string does not contain any of the strings provided in <paramref name="values"/>.
        /// </summary>
        /// <param name="values">
        /// The values that should not be present in the string
        /// </param>
        public AndConstraint<TAssertions> NotContainAny(IEnumerable<string> values)
        {
            if (values == null || !values.Any())
            {
                ThrowHelper.ThrowArgumentException(nameof(values), "Cannot assert string containment against <null> or empty.");
            }

            Guard.IsNotNull(Subject, Name);

            if (values.Any(v => Contains(Subject, v, StringComparison.Ordinal)))
            {
                ThrowHelper.ThrowArgumentException($"Parameter {Name} ({typeof(string).ToTypeString()}) must not contain any of following values [{string.Join(", ", values.Select(i => $"'{i}'"))}].");
            }

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that a string does not contain any of the strings provided in <paramref name="values"/>.
        /// </summary>
        /// <param name="values">
        /// The values that should not be present in the string
        /// </param>
        public AndConstraint<TAssertions> NotContainAny(params string[] values)
        {
            return NotContainAny(values.ToList());
        }

        /// <summary>
        /// Asserts that a string does not contain the specified <paramref name="unexpected"/> string,
        /// including any leading or trailing whitespace, with the exception of the casing.
        /// </summary>
        /// <param name="unexpected">The string that the subject is not expected to contain.</param>
        public AndConstraint<TAssertions> NotContainEquivalentOf(string unexpected)
        {
            if (Contains(Subject.Trim(), unexpected.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                ThrowHelper.ThrowArgumentException($"Parameter {Name} ({typeof(string).ToTypeString()}) must not contain an equivalent value of '{unexpected}'.");
            }

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        private static bool Contains(string actual, string expected, StringComparison comparison)
        {
            return (actual ?? string.Empty).Contains(expected ?? string.Empty, comparison);
        }

        /// <summary>
        /// Asserts that a string is <see cref="string.Empty"/>.
        /// </summary>
        public AndConstraint<TAssertions> BeEmpty()
        {
            Guard.IsEmpty(Subject, Name);

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that a string is not <see cref="string.Empty"/>.
        /// </summary>
        public AndConstraint<TAssertions> NotBeEmpty()
        {
            Guard.IsNotEmpty(Subject, Name);

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that a string has the specified <paramref name="expected"/> length.
        /// </summary>
        /// <param name="expected">The expected length of the string</param>
        public AndConstraint<TAssertions> HaveLength(int expected)
        {
            Guard.IsNotNull(Subject, Name);

            Guard.HasSizeEqualTo(Subject, expected, Name);

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that a string has the length less than <paramref name="expected"/>.
        /// </summary>
        /// <param name="expected">The expected length of the string</param>
        public AndConstraint<TAssertions> HaveLengthLessThan(int expected)
        {
            Guard.IsNotNull(Subject, Name);

            Guard.HasSizeLessThan(Subject, expected, Name);

            return new AndConstraint<TAssertions>((TAssertions)this);
        }
        /// <summary>
        /// Asserts that a string has the length less than or equal to <paramref name="expected"/>.
        /// </summary>
        /// <param name="expected">The expected length of the string</param>
        public AndConstraint<TAssertions> HaveLengthLessThanOrEqualTo(int expected)
        {
            Guard.IsNotNull(Subject, Name);

            Guard.HasSizeLessThanOrEqualTo(Subject, expected, Name);

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that a string has the length greater than <paramref name="expected"/>.
        /// </summary>
        /// <param name="expected">The expected length of the string</param>
        public AndConstraint<TAssertions> HaveLengthGreaterThan(int expected)
        {
            Guard.IsNotNull(Subject, Name);

            Guard.HasSizeGreaterThan(Subject, expected, Name);

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that a string has the length greater than or equal to <paramref name="expected"/>.
        /// </summary>
        /// <param name="expected">The expected length of the string</param>
        public AndConstraint<TAssertions> HaveLengthGreaterThanOrEqual(int expected)
        {
            Guard.IsNotNull(Subject, Name);

            Guard.HasSizeGreaterThanOrEqualTo(Subject, expected, Name);

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that a string is neither <c>null</c> nor <see cref="string.Empty"/>.
        /// </summary>
        public AndConstraint<TAssertions> NotBeNullOrEmpty()
        {
            Guard.IsNotNullOrEmpty(Subject, Name);

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that a string is either <c>null</c> or <see cref="string.Empty"/>.
        /// </summary>
        public AndConstraint<TAssertions> BeNullOrEmpty()
        {
            Guard.IsNullOrEmpty(Subject, Name);

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that a string is neither <c>null</c> nor <see cref="string.Empty"/> nor white space
        /// </summary>
        /// <param name="because">
        public AndConstraint<TAssertions> NotBeNullOrWhiteSpace(string because = "", params object[] becauseArgs)
        {
            Guard.IsNotNullOrWhiteSpace(Subject, Name);

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that a string is either <c>null</c> or <see cref="string.Empty"/> or white space
        /// </summary>
        public AndConstraint<TAssertions> BeNullOrWhiteSpace()
        {
            Guard.IsNullOrWhiteSpace(Subject, Name);

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that all characters in a string are in upper casing.
        /// </summary>
        /// <remarks>
        /// Be careful that numbers and special characters don't have casing, so  <see cref="BeUpperCased"/>
        /// will always fail on a string that contains anything but alphabetic characters.
        /// In those cases, we recommend using <see cref="NotBeLowerCased"/>.
        /// </remarks>
        public AndConstraint<TAssertions> BeUpperCased()
        {
            if (!Subject?.All(char.IsUpper) == true)
            {
                ThrowHelper.ThrowArgumentException($"Parameter {Name} ({typeof(string).ToTypeString()}) must have only uppercased chars.");
            }

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that all characters in a string are not in upper casing.
        /// </summary> 
        public AndConstraint<TAssertions> NotBeUpperCased()
        {
            if (Subject?.All(char.IsUpper) == true)
            {
                ThrowHelper.ThrowArgumentException($"Parameter {Name} ({typeof(string).ToTypeString()}) must not have only uppercased chars.");
            }

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that all characters in a string are in lower casing.
        /// </summary>
        /// <remarks>
        /// Be careful that numbers and special characters don't have casing, so <see cref="BeLowerCased"/> will always fail on
        /// a string that contains anything but alphabetic characters.
        /// In those cases, we recommend using <see cref="NotBeUpperCased"/>.
        /// </remarks> 
        public AndConstraint<TAssertions> BeLowerCased()
        {
            if (!Subject?.All(char.IsLower) == true)
            {
                ThrowHelper.ThrowArgumentException($"Parameter {Name} ({typeof(string).ToTypeString()}) must have only lowercased chars.");
            }

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that all characters in a string are not in lower casing.
        /// </summary> 
        public AndConstraint<TAssertions> NotBeLowerCased()
        {
            if (!Subject?.All(char.IsLower) == true)
            {
                ThrowHelper.ThrowArgumentException($"Parameter {Name} ({typeof(string).ToTypeString()}) must not have only lowercased chars.");
            }

            return new AndConstraint<TAssertions>((TAssertions)this);
        }


        private static string WildCardToRegular(string value)
        {
            return "^" + Regex.Escape(value).Replace("\\?", ".").Replace("\\*", ".*") + "$";
        }
    }
}
