using Mahdhi.GuardFluently.Core.Extensions;
using System;
using Xunit;

namespace Mahdhi.GuardFluently.Core.Tests
{
    public class Guard_Tests
    {

        [Fact]
        public void Test_Guard_Be_OK()
        {
            5.Should().Be(5);
            "A".Should().Be("A");
        }

        [Fact]
        public void Test_Guard_Be_Fail()
        {
            Assert.Throws<ArgumentException>(() => 5.Should().Be(6));
            Assert.Throws<ArgumentException>(() => "A".Should().Be("K"));
        }

        [Fact]
        public void Test_Guard_NotBe_OK()
        {
            5.Should().NotBe(15);
            "A".Should().NotBe("P");
        }

        [Fact]
        public void Test_Guard_NotBe_Fail()
        {
            Assert.Throws<ArgumentException>(() => 5.Should().NotBe(5));
            Assert.Throws<ArgumentException>(() => "A".Should().NotBe("A"));
        }

        [Fact]
        public void Test_Guard_BeNull_OK()
        {
            SomeClass someClass = null;
            someClass.Should().BeNull();
        }

        [Fact]
        public void Test_Guard_BeNull_Fail()
        {
            SomeClass someClass = new();
            Assert.Throws<ArgumentException>(() => someClass.Should().BeNull());
        }

        [Fact]
        public void Test_Guard_NotBeNull_OK()
        {
            SomeClass someClass = new();
            someClass.Should().NotBeNull();
        }

        [Fact]
        public void Test_Guard_NotBeNull_Fail()
        {
            SomeClass someClass = null;
            Assert.Throws<ArgumentNullException>(() => someClass.Should().NotBeNull());
        }

        [Fact]
        public void Test_Guard_BeSameAs_OK()
        {
            SomeClass someClass = new();
            someClass.Should().BeSameAs(someClass);
        }

        [Fact]
        public void Test_Guard_BeSameAs_Fail()
        {
            Assert.Throws<ArgumentException>(() => new SomeClass().Should().BeSameAs(new()));
        }

        [Fact]
        public void Test_Guard_NotBeSameAs_OK()
        {
            new SomeClass().Should().NotBeSameAs(new SomeClass());
        }

        [Fact]
        public void Test_Guard_NotBeSameAs_Fail()
        {
            SomeClass someClass = new();
            Assert.Throws<ArgumentException>(() => someClass.Should().NotBeSameAs(someClass));
        }

        [Fact]
        public void Test_Guard_BeOfType_OK()
        {
            new SomeClass().Should().BeOfType(typeof(SomeClass));
        }

        [Fact]
        public void Test_Guard_BeOfType_Fail()
        {
            Assert.Throws<ArgumentException>(() => new SomeClass().Should().BeOfType(typeof(String)));
        }

        [Fact]
        public void Test_Guard_NotBeOfType_OK()
        {
            new SomeClass().Should().NotBeOfType(typeof(String));
        }

        [Fact]
        public void Test_Guard_NotBeOfType_Fail()
        {
            Assert.Throws<ArgumentException>(() => new SomeClass().Should().NotBeOfType(typeof(SomeClass)));
        }

        [Fact]
        public void Test_Guard_BeAssignableTo_OK()
        {
            "Mabrouk".Should().BeAssignableTo(typeof(string));
            "Mahdhi".Should().BeAssignableTo<string>();
        }

        [Fact]
        public void Test_Guard_BeAssignableTo_Fail()
        {
            Assert.Throws<ArgumentException>(() => 7.Should().BeAssignableTo(typeof(string)));
            Assert.Throws<ArgumentException>(() => 7.Should().BeAssignableTo<string>());
        }

        [Fact]
        public void Test_Guard_NotBeAssignableTo_OK()
        {
            7.Should().NotBeAssignableTo(typeof(string));
            7.Should().NotBeAssignableTo<string>();
        }

        [Fact]
        public void Test_Guard_NotBeAssignableTo_Fail()
        {
            Assert.Throws<ArgumentException>(() => "Mabrouk".Should().NotBeAssignableTo(typeof(string)));
            Assert.Throws<ArgumentException>(() => "Mahdhi".Should().NotBeAssignableTo<string>());
        }


        [Fact]
        public void Test_Guard_Match_OK()
        {
            var obj = new SomeClass() { Id = 7 };

            obj.Should().Match<SomeClass>(i => i.Id == 7);
        }

        [Fact]
        public void Test_Guard_Match_Fail()
        {
            var obj = new SomeClass() { Id = 7 };

            obj.Should().Match<SomeClass>(i => i.Id == 7);
            Assert.Throws<ArgumentException>(() => obj.Should().Match<SomeClass>(i => i.Id == 8));
        }

    }
    class SomeClass
    {
        public int Id { get; set; }
    }
}
