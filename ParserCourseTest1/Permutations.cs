using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserCourseTest1;

public class Permutations
{
    public List<List<int>> GetPermutations(int to)
    {
        var initial = GetList(to);
        if (initial.Count == 0 || initial.Count == 1)
            return new List<List<int>>() { initial };
        return GetPermutations(initial);
    }

    private List<List<int>> GetPermutations(List<int> initial)
    {
        /* permutations of 1 2 3:
         * 1 2 3
         * 1 3 2
         * 2 1 3
         * 2 3 1
         * 3 1 2
         * 3 2 1
         * 
         * recursive steps:
         * 1 2 3
         * 2 3
         * 1 + 2 3
         * 1 + 3 2
         */
        List<List<int>> result = new();
        if (initial.Count == 2)
        {
            return Swap(initial);
        }
        else

            for (int i = 0; i < initial.Count; i++)
            {
                var first = initial[i];
                var rest = GetRest(i, initial);
                var last = GetPermutations(rest);
                foreach (var p in last)
                {
                    p.Insert(0, first);
                    result.Add(p);
                }
            }

        return result;
    }

    private List<List<int>> Swap(List<int> initial)
    {
        return new List<List<int>>()
        {
            initial,
            new List<int> { initial[1], initial[0] }
        };
    }

    public List<int> GetList(int to)
    {
        return Enumerable.Range(1, to).ToList();
    }

    public List<int> GetRest(int index, List<int> list)
    {
        List<int> result = new();

        for (int i = 0; i < list.Count; i++)
        {
            if (i == index) continue;
            result.Add(list[i]);
        }

        return result;
    }
}
