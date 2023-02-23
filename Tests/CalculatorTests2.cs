using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserCourseTest1.Calculator.Practice2;
using System.Collections.Generic;

namespace Tests;

[TestClass]
public class CalculatorTests2
{
    [TestMethod]
    public void CanAddTwoNumbers()
    {
        Calculator calc = new();
        var result = calc.Calculate("1+2");
        Assert.AreEqual(3, result);
    }

    [TestMethod]
    public void CanAddSeveralNumbers()
    {
        Calculator calc = new();
        var result = calc.Calculate("1+2+3+4+5");
        Assert.AreEqual(15, result);
    }

    [TestMethod]
    public void CanUseAllFourOperators()
    {
        Calculator calc = new();
        var result = calc.Calculate("2+4*5-6/2");
        Assert.AreEqual(12, result);
    }

    [TestMethod]
    public void CanUseParentheses()
    {
        Calculator calc = new();
        var result = calc.Calculate("2+(4*5)");
        Assert.AreEqual(22, result);
    }

    [TestMethod]
    public void CanUseVariables()
    {
        Calculate calc = new("1+a", new Dictionary<string, double>() { { "a", 41 } });
        var result = calc.Result["@"];
        Assert.AreEqual(42, result);
    }

    [TestMethod]
    public void HasMultiDigitNumbers()
    {
        Calculator calc = new();
        var result = calc.Calculate("1234");
        Assert.AreEqual(1234, result);
    }

    [TestMethod]
    public void HasDecimalNumbers()
    {
        Calculator calc = new();
        var result = calc.Calculate("1234.1234");
        Assert.AreEqual(1234.1234, result);
    }

    [TestMethod]
    public void CanCalculateWithDecimalNumbers()
    {
        Calculator calc = new();
        var result = calc.Calculate("1+1234.1234");
        Assert.AreEqual(1235.1234, result);
    }

    [TestMethod]
    public void HasDecimalNumbersThatStartsWithDot()
    {
        Calculator calc = new();
        var result = calc.Calculate(".1234");
        Assert.AreEqual(.1234, result);
    }

    [TestMethod]
    public void HasNegativeNumbers()
    {
        Calculator calc = new();
        var result = calc.Calculate("-42");
        Assert.AreEqual(-42, result);
    }

    [TestMethod]
    public void HasNegativeDecimalNumbers()
    {
        Calculator calc = new();
        var result = calc.Calculate("-4.2");
        Assert.AreEqual(-4.2, result);
    }

    [TestMethod]
    public void CanCalculateWithNegativeNumbers()
    {
        Calculator calc = new();
        var result = calc.Calculate("5--4.2");
        Assert.AreEqual(9.2, result);
    }

    [TestMethod]
    public void CanAssignValuesToVariables()
    {
        Calculator calc = new();
        calc.Calculate("41:x");
        var result = calc.Calculate("x+1");
        Assert.AreEqual(42, result);
    }

    [TestMethod]
    public void CanAssignToSeveralVariablesAtOnce()
    {
        Calculator calc = new();
        calc.Calculate("1:x:y");
        var result = calc.Calculate("x+y");
        Assert.AreEqual(2, result);
    }

    [TestMethod]
    public void VariablesPassThroughValues()
    {
        Calculator calc = new();
        var result = calc.Calculate("42:x");
        Assert.AreEqual(42, result);
    }

    [TestMethod]
    public void VariablesCanConsistOfMoreThanOneLetter()
    {
        Calculator calc = new();
        calc.Calculate("41:xTraLong");
        var result = calc.Calculate("xTraLong+1");
        Assert.AreEqual(42, result);
    }
}