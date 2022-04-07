//************************************************************************************
// Token [and personalized] from https://github.com/fluentassertions/fluentassertions
//************************************************************************************ 
using Microsoft.Toolkit.Diagnostics;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Mahdhi.GuardFluently.Core.Primitives
{
#pragma warning disable CS0659 // Ignore not overriding Object.GetHashCode()
#pragma warning disable CA1065 // Ignore throwing NotSupportedException from Equals
    /// <summary>
    /// Contains a number of methods to assert that a reference type object is in the expected state.
    /// </summary>
    [DebuggerNonUserCode]
    public abstract class ReferenceTypeGuards<TSubject, TAssertions>
        where TAssertions : ReferenceTypeGuards<TSubject, TAssertions>
        where TSubject : class
    {
        protected ReferenceTypeGuards(TSubject subject, [CallerArgumentExpression("subject")] string name = "")
        {
            Subject = subject;
            Name = name;

        }

        /// <summary>
        /// Gets the object which value is being asserted.
        /// </summary>
        public TSubject Subject { get; }
        public string Name { get; }

        /// <summary>
        /// Asserts that the current object has not been initialized.
        /// </summary>
        public AndConstraint<TAssertions> BeNull()
        {
            Guard.IsNull(Subject, Name);

            return new AndConstraint<TAssertions>((TAssertions)this);

        }

        /// <summary>
        /// Asserts that the current object has been initialized.
        /// </summary>
        public AndConstraint<TAssertions> NotBeNull()
        {
            Guard.IsNotNull(Subject, Name);

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that an object reference refers to the exact same object as another object reference.
        /// </summary>
        /// <param name="expected">The expected object</param>
        public AndConstraint<TAssertions> BeSameAs(TSubject expected)
        {
            Guard.IsReferenceEqualTo(Subject, expected, Name);

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that an object reference refers to a different object than another object reference refers to.
        /// </summary>
        /// <param name="unexpected">The unexpected object</param>
        public AndConstraint<TAssertions> NotBeSameAs(TSubject unexpected)
        {
            Guard.IsReferenceNotEqualTo(Subject, unexpected, Name);

            return new AndConstraint<TAssertions>((TAssertions)this);
        }


        /// <summary>
        /// Asserts that the object is of the <paramref name="expectedType"/>.
        /// </summary>
        public AndConstraint<TAssertions> BeOfType(Type expectedType)
        {
            Guard.IsOfType(Subject, expectedType, Name);
            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that the object is not of the specified type <typeparamref name="T"/>.
        /// </summary> 
        public AndConstraint<TAssertions> NotBeOfType<T>()
        {
            Guard.IsNotOfType(Subject, typeof(T), Name);
            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that the object is not the <paramref name="unexpectedType"/>.
        /// </summary> 
        public AndConstraint<TAssertions> NotBeOfType(Type unexpectedType)
        {
            Guard.IsNotOfType(Subject, unexpectedType, Name);
            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that the object is assignable to a variable of given <paramref name="type"/>.
        /// </summary>
        public AndConstraint<TAssertions> BeAssignableTo<T>()
        {
            Guard.IsAssignableToType<T>(Subject, Name);
            return new AndConstraint<TAssertions>((TAssertions)this);
        }


        /// <summary>
        /// Asserts that the object is assignable to a variable of given <paramref name="type"/>.
        /// </summary>
        public AndConstraint<TAssertions> BeAssignableTo(Type type)
        {
            Guard.IsAssignableToType(Subject, type, Name);
            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that the object is not assignable to a variable of type <typeparamref name="T"/>.
        /// </summary> 
        public AndConstraint<TAssertions> NotBeAssignableTo<T>()
        {
            Guard.IsNotAssignableToType<T>(Subject, Name);
            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that the object is not assignable to a variable of given <paramref name="type"/>.
        /// </summary>
        public AndConstraint<TAssertions> NotBeAssignableTo(Type type)
        {
            Guard.IsNotAssignableToType(Subject, type, Name);
            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that the <paramref name="predicate" /> is satisfied.
        /// </summary>
        public AndConstraint<TAssertions> Match(Expression<Func<TSubject, bool>> predicate)
        {
            return Match<TSubject>(predicate);
        }

        /// <summary>
        /// Asserts that the <paramref name="predicate" /> is satisfied.
        /// </summary>
        public AndConstraint<TAssertions> Match<T>(Expression<Func<T, bool>> predicate)
            where T : TSubject
        {
            //TO DO : change with Guard.Match method proposed with my issue #196 if it is approved and merged
            // https://github.com/CommunityToolkit/dotnet/issues/196

            if (!predicate.Compile()((T)Subject))
            {
                ThrowHelper.ThrowArgumentException($"Parameter {Name} ({typeof(T).ToTypeString()}) must match the predicate.");
            }

            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        /// <summary>
        /// Asserts that the <paramref name="predicate" /> is satisfied.
        /// </summary>
        public AndConstraint<TAssertions> NotMatch(Expression<Func<TSubject, bool>> predicate)
        {
            return NotMatch<TSubject>(predicate);
        }

        /// <summary>
        /// Asserts that the <paramref name="predicate" /> is satisfied.
        /// </summary>
        public AndConstraint<TAssertions> NotMatch<T>(Expression<Func<T, bool>> predicate)
            where T : TSubject
        {
            //TO DO : change with Guard.NotMatch method proposed with my issue #196 if it is approved and merged
            // https://github.com/CommunityToolkit/dotnet/issues/196

            if (predicate.Compile()((T)Subject))
            {
                ThrowHelper.ThrowArgumentException($"Parameter {Name} ({typeof(T).ToTypeString()}) must match the predicate.");
            }

            return new AndConstraint<TAssertions>((TAssertions)this);
        }


        /// <inheritdoc/>
        public override bool Equals(object? obj) =>
            throw new NotSupportedException("Calling Equals on Guard classes is not supported.");
    }
}
