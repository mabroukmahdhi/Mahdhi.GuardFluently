using Mahdhi.GuardFluently.Core.Extensions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Mahdhi.GuardFluently.Core.Tests
{
    public class String_Guard_Tests
    {
        #region Guard Be ...
        [Fact]
        public void Test_Guard_Be_OK()
        {
            "A".Should().Be("A");
        }

        [Fact]
        public void Test_Guard_Be_Fail()
        {
            Assert.Throws<ArgumentException>(() => "A".Should().Be("K"));
        }

        [Fact]
        public void Test_Guard_NotBe_OK()
        {
            "A".Should().NotBe("C");
        }

        [Fact]
        public void Test_Guard_NotBe_Fail()
        {
            Assert.Throws<ArgumentException>(() => "A".Should().NotBe("A"));
        }

        [Fact]
        public void Test_Guard_BeOneOf_OK()
        {
            string str = null;
            str.Should().BeOneOf("A", "B", "C", "", null);
            "".Should().BeOneOf("A", "B", "C", "");
            "A".Should().BeOneOf("A", "B", "C");
            "B".Should().BeOneOf(new List<string>() { "B", "C", "D" });
        }

        [Fact]
        public void Test_Guard_BeOneOf_Fail()
        {
            Assert.Throws<ArgumentNullException>(() => "A".Should().BeOneOf());
            Assert.Throws<ArgumentException>(() => "A".Should().BeOneOf("B", "C", "D"));
            Assert.Throws<ArgumentException>(() => "B".Should().BeOneOf(new List<string>() { "A", "C", "D" }));
        }

        [Fact]
        public void Test_Guard_NotBeOneOf_OK()
        {
            "A".Should().NotBeOneOf("B", "C", "D");
            "A".Should().NotBeOneOf(new List<string>() { "B", "C", "D" });
        }

        [Fact]
        public void Test_Guard_NotBeOneOf_Fail()
        {
            Assert.Throws<ArgumentNullException>(() => "A".Should().NotBeOneOf());
            Assert.Throws<ArgumentException>(() => "B".Should().NotBeOneOf("B", "C", "D"));
            Assert.Throws<ArgumentException>(() => "A".Should().NotBeOneOf(new List<string>() { "A", "C", "D" }));
        }

        [Fact]
        public void Test_Guard_BeEquivalentTo_OK()
        {
            "A".Should().BeEquivalentTo("A");
            "  a ".Should().BeEquivalentTo("a");
            "a ".Should().BeEquivalentTo(" A  ");
            "A".Should().BeEquivalentTo(" a  ");
        }

        [Fact]
        public void Test_Guard_BeEquivalentTo_Fail()
        {
            Assert.Throws<ArgumentException>(() => "A".Should().BeEquivalentTo("B"));
            Assert.Throws<ArgumentException>(() => "  a ".Should().BeEquivalentTo("b"));
            Assert.Throws<ArgumentException>(() => "a ".Should().BeEquivalentTo(" B  "));
            Assert.Throws<ArgumentException>(() => "A".Should().BeEquivalentTo(" b  "));
        }

        [Fact]
        public void Test_Guard_NotBeEquivalentTo_OK()
        {
            "A".Should().NotBeEquivalentTo("Ac");
            "  a ".Should().NotBeEquivalentTo("ac");
            "a ".Should().NotBeEquivalentTo(" Ac  ");
            "A".Should().NotBeEquivalentTo(" a  .");
        }

        [Fact]
        public void Test_Guard_NotBeEquivalentTo_Fail()
        {
            Assert.Throws<ArgumentException>(() => "A".Should().NotBeEquivalentTo("a"));
            Assert.Throws<ArgumentException>(() => "  a ".Should().NotBeEquivalentTo("a"));
            Assert.Throws<ArgumentException>(() => "a ".Should().NotBeEquivalentTo(" a  "));
            Assert.Throws<ArgumentException>(() => "A".Should().NotBeEquivalentTo(" a  "));
        }

        #endregion

        #region Guard Match

        [Fact]
        public void Test_Guard_Match_Wildcard_OK()
        {
            "A".Should().Match("?");
            "Abmachen".Should().Match("?b*");
            "Germany".Should().Match("*erm*");
        }

        [Fact]
        public void Test_Guard_Match_Wildcard_Fail()
        {
            Assert.Throws<ArgumentException>(() => "A".Should().Match("??"));
            Assert.Throws<ArgumentException>(() => "Abmachen".Should().Match("?B*"));
            Assert.Throws<ArgumentException>(() => "How many".Should().Match("*erm*"));
        }

        [Fact]
        public void Test_Guard_NotMatch_Wildcard_OK()
        {
            "A".Should().NotMatch("??");
            "Abmachen".Should().NotMatch("?B*");
            "How many".Should().NotMatch("*erm*");
        }

        [Fact]
        public void Test_Guard_NotMatch_Wildcard_Fail()
        {
            Assert.Throws<ArgumentException>(() => "A".Should().NotMatch("?"));
            Assert.Throws<ArgumentException>(() => "Abmachen".Should().NotMatch("?b*"));
            Assert.Throws<ArgumentException>(() => "Germany".Should().NotMatch("*erm*"));
        }


        [Fact]
        public void Test_Guard_MatchEquivalentOf_Wildcard_OK()
        {
            "A  ".Should().MatchEquivalentOf("?");
            " abmachen    ".Should().MatchEquivalentOf("?B*");
            "GERMANY".Should().MatchEquivalentOf("*erm*");
        }

        [Fact]
        public void Test_Guard_MatchEquivalentOf_Wildcard_Fail()
        {
            Assert.Throws<ArgumentException>(() => "A . ".Should().MatchEquivalentOf("?"));
            Assert.Throws<ArgumentException>(() => " .abmachen    ".Should().MatchEquivalentOf("?B*"));
            Assert.Throws<ArgumentException>(() => "GER.MANY".Should().MatchEquivalentOf("*erm*"));
        }

        [Fact]
        public void Test_Guard_NotMatchEquivalentOf_Wildcard_OK()
        {
            "A . ".Should().NotMatchEquivalentOf("?");
            "....abmachen    ".Should().NotMatchEquivalentOf("?B*");
            "GER.MANY".Should().NotMatchEquivalentOf("*erm*");
        }

        [Fact]
        public void Test_Guard_NotMatchEquivalentOf_Wildcard_Fail()
        {
            Assert.Throws<ArgumentException>(() => "A . ".Should().NotMatchEquivalentOf("A . "));
            Assert.Throws<ArgumentException>(() => " abmachen    ".Should().NotMatchEquivalentOf("?B*"));
            Assert.Throws<ArgumentException>(() => "GERMANY".Should().NotMatchEquivalentOf("*erm*"));
        }

        [Fact]
        public void Test_Guard_MatchRegex_OK()
        {
            "A . ".Should().MatchRegex("^A");
            "abmachen    ".Should().MatchRegex("[a-zA-Z].*");
            "germany".Should().MatchRegex(".*any$");
        }

        [Fact]
        public void Test_Guard_MatchRegex_Fail()
        {
            Assert.Throws<ArgumentException>(() => "A . ".Should().MatchRegex("A .. "));
            Assert.Throws<ArgumentException>(() => " abmachen    ".Should().MatchRegex("B.*"));
            Assert.Throws<ArgumentException>(() => "GERMANY".Should().MatchRegex(".*erm.*"));
        }

        [Fact]
        public void Test_Guard_NotMatchRegex_OK()
        {
            "A . ".Should().NotMatchRegex("^B");
            "abmachen    ".Should().NotMatchRegex("^[c-z].*");
            "germany".Should().NotMatchRegex(".*ay$");
        }

        [Fact]
        public void Test_Guard_NotMatchRegex_Fail()
        {
            Assert.Throws<ArgumentException>(() => "A . ".Should().NotMatchRegex("A . "));
            Assert.Throws<ArgumentException>(() => " abmachen    ".Should().NotMatchRegex(".*b.*"));
            Assert.Throws<ArgumentException>(() => "GERMANY".Should().NotMatchRegex("GER.*"));
        }


        #endregion

        #region Guard Starts / ends with ...
        [Fact]
        public void Test_Guard_StartWith_OK()
        {
            "Mabrouk".Should().StartWith("Mab");
            ".NET".Should().StartWith(".");
            "Mahdhi".Should().StartWith("Mahdhi");
        }

        [Fact]
        public void Test_Guard_StartWith_Fail()
        {
            Assert.Throws<ArgumentException>(() => "A".Should().StartWith("B"));
            Assert.Throws<ArgumentException>(() => "Abmachen".Should().StartWith("An"));
            Assert.Throws<ArgumentException>(() => "How many".Should().Match("Who"));
        }

        [Fact]
        public void Test_Guard_NotStartWith_OK()
        {
            "Mabrouk".Should().NotStartWith("Y");
            ".NET".Should().NotStartWith("NET");
            "Mahdhi".Should().NotStartWith("CHOUAT");
        }

        [Fact]
        public void Test_Guard_NotStartWith_Fail()
        {
            Assert.Throws<ArgumentException>(() => "A".Should().NotStartWith("A"));
            Assert.Throws<ArgumentException>(() => "Abmachen".Should().NotStartWith("Ab"));
            Assert.Throws<ArgumentException>(() => "How many".Should().NotStartWith("How"));
        }

        [Fact]
        public void Test_Guard_StartWithEquivalentOf_OK()
        {
            "Mabrouk".Should().StartWithEquivalentOf("   Mab");
            ".NET".Should().StartWithEquivalentOf(" .n");
            "Mahdhi".Should().StartWithEquivalentOf("mahdhi");
        }

        [Fact]
        public void Test_Guard_StartWithEquivalentOf_Fail()
        {
            Assert.Throws<ArgumentException>(() => "A ".Should().StartWithEquivalentOf("B"));
            Assert.Throws<ArgumentException>(() => "Abmachen".Should().StartWithEquivalentOf("A b"));
            Assert.Throws<ArgumentException>(() => "How many".Should().StartWithEquivalentOf("  ow"));
        }

        [Fact]
        public void Test_Guard_NotStartWithEquivalentOf_OK()
        {
            "A ".Should().NotStartWithEquivalentOf("B");
            "Abmachen".Should().NotStartWithEquivalentOf("A b");
            "How many".Should().NotStartWithEquivalentOf("  ow");
        }

        [Fact]
        public void Test_Guard_NotStartWithEquivalentOf_Fail()
        {
            Assert.Throws<ArgumentException>(() => "Mabrouk".Should().NotStartWithEquivalentOf("   Mab"));
            Assert.Throws<ArgumentException>(() => ".NET".Should().NotStartWithEquivalentOf(" .n"));
            Assert.Throws<ArgumentException>(() => "Mahdhi".Should().NotStartWithEquivalentOf("mahdhi"));
        }


        [Fact]
        public void Test_Guard_EndWith_OK()
        {
            "Mabrouk".Should().EndWith("ouk");
            ".NET".Should().EndWith("T");
            "Mahdhi".Should().EndWith("Mahdhi");
        }

        [Fact]
        public void Test_Guard_EndWith_Fail()
        {
            Assert.Throws<ArgumentException>(() => "A".Should().EndWith("B"));
            Assert.Throws<ArgumentException>(() => "Abmachen".Should().EndWith("An"));
            Assert.Throws<ArgumentException>(() => "How many".Should().EndWith("Who"));
        }

        [Fact]
        public void Test_Guard_NotEndWith_OK()
        {
            "Mabrouk".Should().NotEndWith("Y");
            ".NET".Should().NotEndWith("NET.");
            "Mahdhi".Should().NotEndWith("CHOUAT");
        }

        [Fact]
        public void Test_Guard_NotEndWith_Fail()
        {
            Assert.Throws<ArgumentException>(() => "A".Should().NotEndWith("A"));
            Assert.Throws<ArgumentException>(() => "Abmachen".Should().NotEndWith("en"));
            Assert.Throws<ArgumentException>(() => "How many".Should().NotEndWith("many"));
        }

        [Fact]
        public void Test_Guard_EndWithEquivalentOf_OK()
        {
            "Mabrouk".Should().EndWithEquivalentOf("   ouk");
            ".NET".Should().EndWithEquivalentOf(" .net");
            "Mahdhi".Should().EndWithEquivalentOf("   dhi");
        }

        [Fact]
        public void Test_Guard_EndWithEquivalentOf_Fail()
        {
            Assert.Throws<ArgumentException>(() => "A ".Should().EndWithEquivalentOf("B"));
            Assert.Throws<ArgumentException>(() => "Abmachen".Should().EndWithEquivalentOf("A b"));
            Assert.Throws<ArgumentException>(() => "How many".Should().EndWithEquivalentOf("  ow"));
        }

        [Fact]
        public void Test_Guard_NotEndWithEquivalentOf_OK()
        {
            "A ".Should().NotEndWithEquivalentOf("B");
            "Abmachen".Should().NotEndWithEquivalentOf("A b");
            "How many".Should().NotEndWithEquivalentOf("  ow");
        }

        [Fact]
        public void Test_Guard_NotEndWithEquivalentOf_Fail()
        {
            Assert.Throws<ArgumentException>(() => "Mabrouk".Should().NotEndWithEquivalentOf("   ouk"));
            Assert.Throws<ArgumentException>(() => ".NET".Should().NotEndWithEquivalentOf(" t"));
            Assert.Throws<ArgumentException>(() => "Mahdhi".Should().NotEndWithEquivalentOf("mahdhi"));
        }
        #endregion

        #region Guard Contain ...
        [Fact]
        public void Test_Guard_Contain_OK()
        {
            "Mabrouk".Should().Contain("Mab");
            ".NET".Should().Contain(".");
            "Mahdhi".Should().Contain("Mahdhi");
        }

        [Fact]
        public void Test_Guard_Contain_Fail()
        {
            Assert.Throws<ArgumentException>(() => "A".Should().Contain("B"));
            Assert.Throws<ArgumentException>(() => "Abmachen".Should().Contain("An"));
            Assert.Throws<ArgumentException>(() => "How many".Should().Contain("Who"));
        }

        [Fact]
        public void Test_Guard_ContainEquivalentOf_OK()
        {
            "Mabrouk".Should().ContainEquivalentOf("  mab   ");
            ".NET".Should().ContainEquivalentOf(".net");
            "Mahdhi".Should().ContainEquivalentOf("     hi");
        }

        [Fact]
        public void Test_Guard_ContainEquivalentOf_Fail()
        {
            Assert.Throws<ArgumentException>(() => "A".Should().ContainEquivalentOf("B"));
            Assert.Throws<ArgumentException>(() => "Abmachen".Should().ContainEquivalentOf(" An "));
            Assert.Throws<ArgumentException>(() => "How many".Should().ContainEquivalentOf("Who "));
        }

        [Fact]
        public void Test_Guard_ContainAll_OK()
        {
            "Mabrouk".Should().ContainAll("ab", "rou", "Ma");
            "Mabrouk".Should().ContainAll(new List<string>() { "ab", "rou", "Ma" });
        }

        [Fact]
        public void Test_Guard_ContainAll_Fail()
        {
            Assert.Throws<ArgumentException>(() => "Mabrouk".Should().ContainAll("aba", "rou", "Mab"));
            Assert.Throws<ArgumentException>(() => "Mabrouk".Should().ContainAll(new List<string>() { "aba", "krou", "May" }));
        }


        [Fact]
        public void Test_Guard_ContainAny_OK()
        {
            "Mabrouk".Should().ContainAny("aba", "rou", "May");
            "Mabrouk".Should().ContainAny(new List<string>() { "aba", "rou", "May" });
        }

        [Fact]
        public void Test_Guard_ContainAny_Fail()
        {
            Assert.Throws<ArgumentException>(() => "Mabrouk".Should().ContainAny("aba", "krou", "May"));
            Assert.Throws<ArgumentException>(() => "Mabrouk".Should().ContainAny(new List<string>() { "aba", "krou", "May" }));
        }


        [Fact]
        public void Test_Guard_NotContain_OK()
        {
            "Mabrouk".Should().NotContain("kour");
            ".NET".Should().NotContain(" ");
            "Mahdhi".Should().NotContain("Yusuf");
        }

        [Fact]
        public void Test_Guard_NotContain_Fail()
        {
            Assert.Throws<ArgumentException>(() => "A".Should().NotContain("A"));
            Assert.Throws<ArgumentException>(() => "Abmachen".Should().NotContain("Ab"));
            Assert.Throws<ArgumentException>(() => "How many".Should().NotContain("How"));
        }

        [Fact]
        public void Test_Guard_NotContainEquivalentOf_OK()
        {
            "Mabrouk".Should().NotContainEquivalentOf("  mab .  ");
            ".NET".Should().NotContainEquivalentOf(".net.");
            "Mahdhi".Should().NotContainEquivalentOf("  .   hi");
        }

        [Fact]
        public void Test_Guard_NotContainEquivalentOf_Fail()
        {
            Assert.Throws<ArgumentException>(() => "A".Should().NotContainEquivalentOf("    A"));
            Assert.Throws<ArgumentException>(() => "Abmachen".Should().NotContainEquivalentOf(" Ab "));
            Assert.Throws<ArgumentException>(() => "How many".Should().NotContainEquivalentOf("How "));
        }

        [Fact]
        public void Test_Guard_NotContainAll_OK()
        {
            "Mabrouk".Should().NotContainAll("acb", "rocu", "mab");
            "Mabrouk".Should().NotContainAll(new List<string>() { "axb", "roxu", "Mxa" });
        }

        [Fact]
        public void Test_Guard_NotContainAll_Fail()
        {
            Assert.Throws<ArgumentException>(() => "Mabrouk".Should().NotContainAll("ab", "rou", "Ma"));
            Assert.Throws<ArgumentException>(() => "Mabrouk".Should().NotContainAll(new List<string>() { "ab", "rou", "Ma" }));
        }


        [Fact]
        public void Test_Guard_NotContainAny_OK()
        {
            "Mabrouk".Should().NotContainAny("aba", "rosu", "Msay");
            "Mabrouk".Should().NotContainAny(new List<string>() { "aba", "rosu", "Msay" });
        }

        [Fact]
        public void Test_Guard_NotContainAny_Fail()
        {
            Assert.Throws<ArgumentException>(() => "Mabrouk".Should().NotContainAny("aba", "krou", "Mab"));
            Assert.Throws<ArgumentException>(() => "Mabrouk".Should().NotContainAny(new List<string>() { "aba", "krou", "Mab" }));
        }

        #endregion

        #region Guard Empty
        [Fact]
        public void Test_Guard_BeEmpty_OK()
        {
            "".Should().BeEmpty();
            string.Empty.Should().BeEmpty();
        }

        [Fact]
        public void Test_Guard_BenEmpty_Fail()
        {
            Assert.Throws<ArgumentException>(() => "Hallo".Should().BeEmpty());
        }

        [Fact]
        public void Test_Guard_NotBeEmpty_OK()
        {
            "Hallo".Should().NotBeEmpty();
        }

        [Fact]
        public void Test_Guard_NotBenEmpty_Fail()
        {
            Assert.Throws<ArgumentException>(() => "".Should().NotBeEmpty());
        }

        [Fact]
        public void Test_Guard_BeNullOrEmpty_OK()
        {
            string str = null;
            str.Should().BeNullOrEmpty();
            "".Should().BeNullOrEmpty();
        }

        [Fact]
        public void Test_Guard_BeNullOrEmpty_Fail()
        {
            Assert.Throws<ArgumentException>(() => "Mabrouk".Should().BeNullOrEmpty());
        }

        [Fact]
        public void Test_Guard_NotBeNullOrEmpty_OK()
        {
            "Mabrouk".Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void Test_Guard_NotBeNullOrEmpty_Fail()
        {
            string str = null;
            Assert.Throws<ArgumentNullException>(() => str.Should().NotBeNullOrEmpty());
            Assert.Throws<ArgumentException>(() => "".Should().NotBeNullOrEmpty());
        }

        [Fact]
        public void Test_Guard_BeNullOrWhiteSpace_OK()
        {
            string str = null;
            str.Should().BeNullOrWhiteSpace();
            "".Should().BeNullOrWhiteSpace();
            "   ".Should().BeNullOrWhiteSpace();
        }

        [Fact]
        public void Test_Guard_BeNullOrWhiteSpace_Fail()
        {
            Assert.Throws<ArgumentException>(() => "Mabrouk".Should().BeNullOrWhiteSpace());
            Assert.Throws<ArgumentException>(() => "M ".Should().BeNullOrWhiteSpace());
        }

        [Fact]
        public void Test_Guard_NotBeNullOrWhiteSpace_OK()
        {
            "Mabrouk".Should().NotBeNullOrWhiteSpace();
            "M  ".Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void Test_Guard_NotBeNullOrWhiteSpace_Fail()
        {
            string str = null;
            Assert.Throws<ArgumentNullException>(() => str.Should().NotBeNullOrWhiteSpace());
            Assert.Throws<ArgumentException>(() => "".Should().NotBeNullOrWhiteSpace());
            Assert.Throws<ArgumentException>(() => "  ".Should().NotBeNullOrWhiteSpace());
        }
        #endregion

        #region Guard Cases
        [Fact]
        public void Test_Guard_BeUpperCased_OK()
        {
            "MABROUK".Should().BeUpperCased();
        }

        [Fact]
        public void Test_Guard_BeUpperCased_Fail()
        {
            Assert.Throws<ArgumentException>(() => "Mabrouk".Should().BeUpperCased());
        }

        [Fact]
        public void Test_Guard_BeLowerCased_OK()
        {
            "mabrouk".Should().BeLowerCased();
        }

        [Fact]
        public void Test_Guard_BeLowerCased_Fail()
        {
            Assert.Throws<ArgumentException>(() => "Mabrouk".Should().BeLowerCased());
        }

        #endregion

        #region Guard Length
        [Fact]
        public void Test_Guard_HaveLength_OK()
        {
            "Mabrouk".Should().HaveLength(7);
        }

        [Fact]
        public void Test_Guard_HaveLength_Fail()
        {
            Assert.Throws<ArgumentException>(() => "Mabrouk".Should().HaveLength(10));
        }

        [Fact]
        public void Test_Guard_HaveLengthLessThan_OK()
        {
            "Mabrouk".Should().HaveLengthLessThan(10);
        }

        [Fact]
        public void Test_Guard_HaveLengthLessThan_Fail()
        {
            Assert.Throws<ArgumentException>(() => "Mabrouk".Should().HaveLengthLessThan(5));
        }

        [Fact]
        public void Test_Guard_HaveLengthLessThanOrEqualTo_OK()
        {
            "Mabrouk".Should().HaveLengthLessThanOrEqualTo(17); // less than
            "Mabrouk".Should().HaveLengthLessThanOrEqualTo(7); // equal to
        }

        [Fact]
        public void Test_Guard_HaveLengthLessThanOrEqualTo_Fail()
        {
            Assert.Throws<ArgumentException>(() => "Mabrouk".Should().HaveLengthLessThanOrEqualTo(5));
        }
        #endregion

    }
}
