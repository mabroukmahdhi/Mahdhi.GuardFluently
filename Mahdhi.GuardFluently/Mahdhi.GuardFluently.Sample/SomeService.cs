using Mahdhi.GuardFluently.Core.Extensions;

namespace Mahdhi.GuardFluently.Sample
{
    internal class SomeService
    {
        public SomeService()
        {

        }

        public void DoSomeCall(string name, object length)
        {
            //guard
            name.Should()
                .NotBeNullOrWhiteSpace()
                .And
                .HaveLengthGreaterThan(10);

            length.Should().BeAssignableTo(typeof(int));

            // do something
        }
    }
}
