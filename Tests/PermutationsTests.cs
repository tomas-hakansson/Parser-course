using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserCourseTest1;
using System.Collections.Generic;
using System.Linq;

namespace Tests;

[TestClass]
public class PermutationsTests
{
    [TestMethod]
    public void GetPermutationsForLength3()
    {
        List<List<int>> expected = new()
        {
            new List<int>() { 1, 2, 3 },
            new List<int>() { 1, 3, 2 },
            new List<int>() { 2, 1, 3 },
            new List<int>() { 2, 3, 1 },
            new List<int>() { 3, 1, 2 },
            new List<int>() { 3, 2, 1 }
        };
        Permutations p = new();
        List<List<int>> result = p.GetPermutations(3);

        for (int i = 0; i < expected.Count; i++)
        {
            CollectionAssert.AreEqual(expected[i], result[i]);
        }
    }

    [TestMethod]
    public void GetPermutationsForLength4()
    {
        List<List<int>> expected = new()
        {
            new List<int>() { 1, 2, 3, 4 },
            new List<int>() { 1, 2, 4, 3 },
            new List<int>() { 1, 3, 2, 4 },
            new List<int>() { 1, 3, 4, 2 },
            new List<int>() { 1, 4, 2, 3 },
            new List<int>() { 1, 4, 3, 2 },
            new List<int>() { 2, 1, 3, 4 },
            new List<int>() { 2, 1, 4, 3 },
        };
        Permutations p = new();
        List<List<int>> result = p.GetPermutations(4);

        for (int i = 0; i < expected.Count; i++)
        {
            CollectionAssert.AreEqual(expected[i], result[i]);
        }
    }

    [TestMethod]
    public void GetList()
    {
        Permutations p = new();
        List<int> expected = new() { 1, 2, 3 };
        List<int> result = p.GetList(3);
        CollectionAssert.AreEqual(expected, result);
    }

    [TestMethod]
    public void GetRest()
    {
        Permutations p = new();
        List<int> ourList = p.GetList(3);

        List<int> expected = new() { 2, 3 };
        List<int> result = p.GetRest(0, ourList);
        CollectionAssert.AreEqual(expected, result);

        expected = new() { 1, 3 };
        result = p.GetRest(1, ourList);
        CollectionAssert.AreEqual(expected, result);

        expected = new() { 1, 2 };
        result = p.GetRest(2, ourList);
        CollectionAssert.AreEqual(expected, result);
    }
}