using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserCourseTest1.EmailAddress.Practice1;

namespace Tests;

[TestClass]
public class EmailAddressTests
{
    [TestMethod]
    public void T1_MustNotBeEmpty()
    {
        var (success, ea) = EmailAddressParser.Parse("");
        Assert.IsFalse(success);
    }

    [TestMethod]
    public void T2_ParsesProperEmailAddress()
    {
        var (success, ea) = EmailAddressParser.Parse("name@start_domain.end_domain");
        Assert.IsTrue(success);
        Assert.AreEqual("name", ea.Name);
        Assert.AreEqual("start_domain", ea.StartDomain);
        Assert.AreEqual("end_domain", ea.EndDomain);
    }

    [TestMethod]
    public void T3_StopsAtSpace()
    {
        var (success, ea) = EmailAddressParser.Parse("name@start_domain.end_domain hello");
        Assert.IsTrue(success);
        Assert.AreEqual("end_domain", ea.EndDomain);
    }

    [TestMethod]
    public void T4_CanNotHaveSpaceInside()
    {
        var (success, ea) = EmailAddressParser.Parse("name@start_ domain.end_domain");
        Assert.IsFalse(success);
    }

    [TestMethod]
    public void T5_PeriodInNameIsIgnored()
    {
        var (success, ea) = EmailAddressParser.Parse("n.a.me@start_domain.end_domain");
        Assert.IsTrue(success);
        Assert.AreEqual("name", ea.Name);
    }

    [TestMethod]
    public void T6_PlusAndRestOfNameIsIgnored()
    {
        var (success, ea) = EmailAddressParser.Parse("name+thisisignored@start_domain.end_domain");
        Assert.IsTrue(success);
        Assert.AreEqual("name", ea.Name);
        Assert.AreEqual("start_domain", ea.StartDomain);
    }
}