using System.Diagnostics;

namespace Mahdhi.GuardFluently.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DebuggerNonUserCode]
    public class AndConstraint<T>
    {
        public T And { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AndConstraint{T}"/> class.
        /// </summary>
        public AndConstraint(T parentConstraint)
        {
            And = parentConstraint;
        }
    }
}
