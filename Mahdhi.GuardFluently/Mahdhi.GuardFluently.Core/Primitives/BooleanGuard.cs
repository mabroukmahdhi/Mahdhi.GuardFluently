//************************************************************************************
// Token {and personalized] from https://github.com/fluentassertions/fluentassertions
//************************************************************************************ 

using Microsoft.Toolkit.Diagnostics;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Mahdhi.GuardFluently.Core.Primitives
{
    [DebuggerNonUserCode]
    public class BooleanGuards
       : BooleanGuards<BooleanGuards>
    {
        public BooleanGuards(bool? value, [CallerArgumentExpression("value")] string name = "")
            : base(value)
        {
        }
    }

    /// <summary>
    /// Contains a number of methods to assert that a <see cref="bool"/> arugument is in the expected state.
    /// </summary>
    [DebuggerNonUserCode]
    public class BooleanGuards<TAssertions>
        where TAssertions : BooleanGuards<TAssertions>
    {

        public BooleanGuards(bool? value, [CallerArgumentExpression("value")] string name = "")
        {
            Subject = value;
            Name = name;
        }


        /// <summary>
        /// Gets the object which value is being asserted.
        /// </summary>
        public bool? Subject { get; }
        public string Name { get; }

        /// <summary>
        /// Asserts that the fluency object is <see langword="true"/>.
        /// </summary> 
        /// <exception cref="ArgumentException">Thrown if the fluency object is not <see langword="true"/>.</exception>
        public AndConstraint<TAssertions> BeTrue()
        {
            return BeTrueWithMessage(string.Empty);
        }

        /// <summary>
        /// Asserts that the fluency object is <see langword="true"/>.
        /// </summary> 
        /// <exception cref="ArgumentException">Thrown if the fluency object is not <see langword="true"/>.</exception>
        public AndConstraint<TAssertions> BeTrueWithMessage(string message, params object[] messageArgs)
        {
            Guard.IsNotNull(Subject, Name);
            Guard.IsTrue(Subject.Value, Name, string.Format(message, messageArgs));
            return new AndConstraint<TAssertions>((TAssertions)this);
        }


        /// <summary>
        /// Asserts that the fluency object is <see langword="true"/>.
        /// </summary> 
        /// <exception cref="ArgumentException">Thrown if the fluency object is not <see langword="true"/>.</exception>
        public AndConstraint<TAssertions> BeFalse()
        {
            return BeFalseWithMessage(string.Empty);
        }


        /// <summary>
        /// Asserts that the fluency object is <see langword="true"/>.
        /// </summary> 
        /// <exception cref="ArgumentException">Thrown if the fluency object is not <see langword="true"/>.</exception>
        public AndConstraint<TAssertions> BeFalseWithMessage(string message, params object[] messageArgs)
        {
            Guard.IsNotNull(Subject, Name);
            Guard.IsFalse(Subject.Value, Name, string.Format(message, messageArgs));
            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that the value is equal to the specified <paramref name="expected"/> value.
        /// </summary>
        public AndConstraint<TAssertions> Be(bool expected)
        {
            Guard.IsNotNull(Subject, Name);
            Guard.IsEqualTo(Subject.Value, expected, Name);
            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that the value is not equal to the specified <paramref name="unexpected"/> value.
        /// </summary>
        public AndConstraint<TAssertions> NotBe(bool unexpected)
        {
            Guard.IsNotNull(Subject, Name);
            Guard.IsNotEqualTo(Subject.Value, unexpected, Name);
            return new AndConstraint<TAssertions>((TAssertions)this);
        }
    }
}
