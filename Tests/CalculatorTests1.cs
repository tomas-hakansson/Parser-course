using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserCourseTest1.Calculator.Practice1;
using System.Collections.Generic;

namespace Tests;

[TestClass]
public class CalculatorTests1
{
    [TestMethod]
    public void CanAdd_WithoutSpaces()
    {
        Calculator calc = new();
        var result = calc.Calculate("1+2");
        Assert.AreEqual(3, result);
    }

    [TestMethod]
    public void CanAddSeveral_WithoutSpaces()
    {
        Calculator calc = new();
        var result = calc.Calculate("1+2+3+4+5");
        Assert.AreEqual(15, result);
    }

    [TestMethod]
    public void CanSubtract_WithoutSpaces()
    {
        Calculator calc = new();
        var result = calc.Calculate("2-1");
        Assert.AreEqual(1, result);
    }

    [TestMethod]
    public void CanAddAndSubtract_WithoutSpaces()
    {
        Calculator calc = new();
        var result = calc.Calculate("1+2+3+4-3");
        Assert.AreEqual(7, result);
    }

    [TestMethod]
    public void CanMultiply_WithoutSpaces()
    {
        Calculator calc = new();
        var result = calc.Calculate("2*3");
        Assert.AreEqual(6, result);
    }

    [TestMethod]
    public void CanMultiplySeveral_WithoutSpaces()
    {
        Calculator calc = new();
        var result = calc.Calculate("2*3*5");
        Assert.AreEqual(30, result);
    }

    [TestMethod]
    public void ProperOperatorPrecedence()
    {
        Calculator calc = new();
        var result = calc.Calculate("2+3*5");
        Assert.AreEqual(17, result);
    }

    [TestMethod]
    public void ParenthesesWorkInTheBeginningOfTheExpression()
    {
        Calculator calc = new();
        var result = calc.Calculate("(2+3)*5");
        Assert.AreEqual(25, result);
    }

    [TestMethod]
    public void ParenthesesWorkAtTheEndOfTheExpression()
    {
        Calculator calc = new();
        var result = calc.Calculate("5*(2+3)");
        Assert.AreEqual(25, result);
    }

    [TestMethod]
    public void CanDivide_WithoutSpaces()
    {
        Calculator calc = new();
        var result = calc.Calculate("10/2");
        Assert.AreEqual(5, result);
    }

    [TestMethod]
    public void DivideCanResultInDecimalNumbers_WithoutSpaces()
    {
        Calculator calc = new();
        var result = calc.Calculate("11/2");
        Assert.AreEqual(5.5, result);
    }

    [TestMethod]
    public void CanMultiplyDecimalNumbers()
    {
        Calculator calc = new();
        var result = calc.Calculate("5.5*2");
        Assert.AreEqual(11, result);
    }

    [TestMethod]
    public void CanResultInNegativeNumber_WithoutSpaces()
    {
        Calculator calc = new();
        var result = calc.Calculate("5-10");
        Assert.AreEqual(-5, result);
    }

    [TestMethod]
    public void CanCalculateWithNegativeNumbers_WithoutSpaces()
    {
        Calculator calc = new();
        var result = calc.Calculate("5*-10");
        Assert.AreEqual(-50, result);
    }

    [TestMethod]
    public void CanCalculateWithVariables_WithoutSpaces()
    {
        Calculate calc = new("1+a", new Dictionary<string, double>() { { "a", 41 } });
        var result = calc.Result["@"];
        Assert.AreEqual(42, result);
    }

    [TestMethod]
    public void CanAssignToVariables_WithoutSpaces()
    {
        Calculator calc = new();
        calc.Calculate("a:41");
        var result = calc.Calculate("1+a");
        Assert.AreEqual(42, result);
    }

    [TestMethod]
    public void CanChainAssignments_WithoutSpaces()
    {
        Calculator calc = new();
        calc.Calculate("a:b:1");
        var result = calc.Calculate("a+b");
        Assert.AreEqual(2, result);
    }

    [TestMethod]
    public void FixingAssignmentBug_1a()
    {
        Calculator calc = new();
        calc.Calculate("a:1");
        var result = calc.Calculate("b:a");
        Assert.AreEqual(1, result);
    }

    [TestMethod]
    public void FixingAssignmentBug_1b()
    {
        Calculator calc = new();
        calc.Calculate("a:1");
        var result = calc.Calculate("a");
        Assert.AreEqual(1, result);
    }

    [TestMethod]
    public void CanUseAllFourArithmeticOperators_WithoutSpaces()
    {
        Calculator calc = new();
        var result = calc.Calculate("2+4*5-6/2");
        Assert.AreEqual(19, result);
    }
}