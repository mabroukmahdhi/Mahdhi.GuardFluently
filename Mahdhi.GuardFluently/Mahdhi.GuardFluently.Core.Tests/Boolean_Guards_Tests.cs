using Mahdhi.GuardFluently.Core.Extensions;
using System;
using Xunit;

namespace Mahdhi.GuardFluently.Core.Tests
{
    public class Boolean_Guards_Tests
    {
        [Fact]
        public void Test_Guard_Be_OK()
        {
            true.Should().Be(true);
            false.Should().Be(false);
        }

        [Fact]
        public void Test_Guard_Be_Fail()
        {
            Assert.Throws<ArgumentException>(() => false.Should().Be(true));
            Assert.Throws<ArgumentException>(() => true.Should().Be(false));
        }

        [Fact]
        public void Test_Guard_NotBe_OK()
        {
            true.Should().NotBe(false);
            false.Should().NotBe(true);
        }

        [Fact]
        public void Test_Guard_NotBe_Fail()
        {
            Assert.Throws<ArgumentException>(() => false.Should().NotBe(false));
            Assert.Throws<ArgumentException>(() => true.Should().NotBe(true));
        }

        [Fact]
        public void Test_Guard_BeTrue_OK()
        {
            true.Should().BeTrue();
            true.Should().BeTrueWithMessage("Hello world");
        }

        [Fact]
        public void Test_Guard_BeTrue_Fail()
        {
            Assert.Throws<ArgumentException>(() => false.Should().BeTrue());
        }

        [Fact]
        public void Test_Guard_BeTrue_Fail_WithMessage()
        {
            var message = "Hello world";
            try
            {
                false.Should().BeTrueWithMessage(message);
            }
            catch (ArgumentException e)
            {
                Assert.Contains(message, e.Message);

                return;
            }
        }

        [Fact]
        public void Test_Guard_BeFalse_OK()
        {
            false.Should().BeFalse();
            false.Should().BeFalseWithMessage("Hello world");
        }

        [Fact]
        public void Test_Guard_BeFalse_Fail()
        {
            Assert.Throws<ArgumentException>(() => true.Should().BeFalse());
        }

        [Fact]
        public void Test_Guard_BeFalse_Fail_WithMessage()
        {
            var message = "Hello world";
            try
            {
                true.Should().BeFalseWithMessage(message);
            }
            catch (ArgumentException e)
            {
                Assert.Contains(message, e.Message);

                return;
            }
        }
    }
}