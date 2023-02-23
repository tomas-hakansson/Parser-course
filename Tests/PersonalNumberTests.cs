using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserCourseTest1.PersonalNumber.Practice1;

namespace Tests;

[TestClass]
public class PersonalNumberTests
{
    [TestMethod]
    public void T1_BasicErrorChecks()
    {
        var (success, pn) = PersonalNumberParser.Parse("");
        Assert.IsFalse(success);
        Assert.IsNull(pn);

        (success, pn) = PersonalNumberParser.Parse("nondigitsarewrong");
        Assert.IsFalse(success);
        Assert.IsNull(pn);

        (success, pn) = PersonalNumberParser.Parse("1234");//Too short.
        Assert.IsFalse(success);
        Assert.IsNull(pn);

        (success, pn) = PersonalNumberParser.Parse("1234123412341234");//Too long.
        Assert.IsFalse(success);
        Assert.IsNull(pn);
    }

    [TestMethod]
    public void T2_JustRight()
    {
        var (success, pn) = PersonalNumberParser.Parse("120424-1234");//Chef's kiss.
        Assert.IsTrue(success);
        Assert.AreEqual(12, pn.Year);
        Assert.AreEqual(4, pn.Month);
        Assert.AreEqual(24, pn.Day);
        Assert.AreEqual(Separator.BelowHundred, pn.Separator);
        Assert.AreEqual("1234", pn.ControlNumber);
    }

    [TestMethod]
    public void T3_SuroundingSpacesAreFine()
    {
        var (success, pn) = PersonalNumberParser.Parse("   120424-1234   ");//Spaces around are ok.
        Assert.IsTrue(success);
    }

    [TestMethod]
    public void T4_APersonalNumberCanNotContainSpaces()
    {
        var (success, pn) = PersonalNumberParser.Parse("1204 4-1234");//Spaces inside are bad.
        Assert.IsFalse(success);
        Assert.IsNull(pn);
    }

    [TestMethod]
    public void T5_NotASeparator()
    {
        var (success, pn) = PersonalNumberParser.Parse("120424&1234");//Not a valid separator.
        Assert.IsFalse(success);

        (success, pn) = PersonalNumberParser.Parse("12042491234");//Right length but digit is not a valid separator.
        Assert.IsFalse(success);
    }

    [TestMethod]
    public void T6_OldPeopleMattersToo()
    {
        var (success, pn) = PersonalNumberParser.Parse("120424+1234");
        Assert.IsTrue(success);
        Assert.AreEqual(12, pn.Year);
        Assert.AreEqual(4, pn.Month);
        Assert.AreEqual(24, pn.Day);
        Assert.AreEqual(Separator.AHundredOrAbove, pn.Separator);
        Assert.AreEqual("1234", pn.ControlNumber);
    }

    [TestMethod]
    public void T7_ThereAre12Months()
    {
        var (success, pn) = PersonalNumberParser.Parse("123424+1234");
        Assert.IsFalse(success);
    }

    [TestMethod]
    public void T8_DifferentMonthsHaveDifferentNumberOfDays()
    {
        var (success, pn) = PersonalNumberParser.Parse("120231-1234");//Febuary has 28 days.
        Assert.IsFalse(success);

        (success, pn) = PersonalNumberParser.Parse("120132-1234");//January has 31 days.
        Assert.IsFalse(success);

        (success, pn) = PersonalNumberParser.Parse("120431-1234");//April has 30 days.
        Assert.IsFalse(success);

        (success, pn) = PersonalNumberParser.Parse("121231-1234");//December has 31 days.
        Assert.IsTrue(success);
    }
}
