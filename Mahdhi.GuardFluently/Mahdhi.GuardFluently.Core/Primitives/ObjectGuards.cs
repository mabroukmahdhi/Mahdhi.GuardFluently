//************************************************************************************
// Token [and personalized] from https://github.com/fluentassertions/fluentassertions
//************************************************************************************ 
using Mahdhi.GuardFluently.Core.Extensions;
using Microsoft.Toolkit.Diagnostics;
using System.Runtime.CompilerServices;

namespace Mahdhi.GuardFluently.Core.Primitives
{
    /// <summary>
    /// Contains a number of methods to assert that an <see cref="object"/> is in the expected state.
    /// </summary>
    public class ObjectGuards : ObjectGuards<object, ObjectGuards>
    {
        public ObjectGuards(object value, [CallerArgumentExpression("value")] string name = "")
            : base(value)
        {

        }
    }

    /// <summary>
    /// Contains a number of methods to assert that an <see cref="object"/> is in the expected state.
    /// </summary>
    public class GenericGuards<TSubject> : ObjectGuards<TSubject, GenericGuards<TSubject>>
        where TSubject : class
    {
        public GenericGuards(TSubject value, [CallerArgumentExpression("value")] string name = "")
            : base(value)
        {
        }
    }

    /// <summary>
    /// Contains a number of methods to assert that a <typeparamref name="TSubject"/> is in the expected state.
    /// </summary>
    public class ObjectGuards<TSubject, TAssertions> : ReferenceTypeGuards<TSubject, TAssertions>
        where TAssertions : ObjectGuards<TSubject, TAssertions>
        where TSubject : class
    {
        public ObjectGuards(TSubject value, [CallerArgumentExpression("value")] string name = "")
            : base(value, name)
        {

        }

        /// <summary>
        /// Asserts that a <typeparamref name="TSubject"/> equals another <typeparamref name="TSubject"/> using its <see cref="object.Equals(object)" /> implementation.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <paramref name="because" />.
        /// </param>
        public AndConstraint<TAssertions> Be(TSubject expected)
        {
            return Be(expected);
        }

        /// <summary>
        /// Asserts that a <typeparamref name="TSubject"/> equals another <typeparamref name="TSubject"/> using its <see cref="object.Equals(object)" /> implementation.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <paramref name="because" />.
        /// </param>
        public AndConstraint<TAssertions> Be<T>(T expected)
            where T : TSubject
        {
            if (!ObjectExtensions.GetComparer<TSubject>()(Subject, expected))
            {
                ThrowHelper.ThrowArgumentException($"Parameter {Name} must be equal to expected object of type {nameof(T)}");
            }
            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that a <typeparamref name="TSubject"/> does not equal another <typeparamref name="TSubject"/> using its <see cref="object.Equals(object)" /> method.
        /// </summary>
        /// <param name="unexpected">The unexpected value</param>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <paramref name="because"/>.
        /// </param>
        public AndConstraint<TAssertions> NotBe(TSubject unexpected)
        {
            return NotBe<TSubject>(unexpected);
        }

        /// <summary>
        /// Asserts that a <typeparamref name="TSubject"/> does not equal another <typeparamref name="TSubject"/> using its <see cref="object.Equals(object)" /> method.
        /// </summary>
        /// <param name="unexpected">The unexpected value</param>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <paramref name="because"/>.
        /// </param>
        public AndConstraint<TAssertions> NotBe<T>(T unexpected)
            where T : TSubject
        {
            if (ObjectExtensions.GetComparer<TSubject>()(Subject, unexpected))
            {
                ThrowHelper.ThrowArgumentException($"Parameter {Name} must be not equal to expected object '{nameof(unexpected)}'");
            }

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

    }
}
