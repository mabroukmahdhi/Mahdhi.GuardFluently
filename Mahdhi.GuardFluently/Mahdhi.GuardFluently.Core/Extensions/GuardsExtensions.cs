//************************************************************************************
// Token [and personalized] from https://github.com/fluentassertions/fluentassertions 
//************************************************************************************ 

using Mahdhi.GuardFluently.Core.Primitives;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Mahdhi.GuardFluently.Core.Extensions
{
    /// <summary>
    /// Contains extension methods for custom assertions in unit tests.
    /// </summary>
    [DebuggerNonUserCode]
    public static class GuardsExtensions
    {
        /// <summary>
        /// Returns an <see cref="BooleanGuards"/> object that can be used to assert the
        /// current <see cref="bool"/>.
        /// </summary>
        public static BooleanGuards Should(this bool actualValue, [CallerArgumentExpression("actualValue")] string name = "")
        {
            return new BooleanGuards(actualValue, name);
        }

        /// <summary>
        /// Returns an <see cref="ObjectGuards"/> object that can be used to assert the
        /// current <see cref="bool"/>.
        /// </summary>
        public static ObjectGuards Should(this object actualValue, [CallerArgumentExpression("actualValue")] string name = "")
        {
            return new ObjectGuards(actualValue, name);
        }

        /// <summary>
        /// Returns an <see cref="StringGuards"/> object that can be used to assert the
        /// current <see cref="string"/>.
        /// </summary>
        public static StringGuards Should(this string actualValue, [CallerArgumentExpression("actualValue")] string name = "")
        {
            return new StringGuards(actualValue, name);
        }
    }
}
