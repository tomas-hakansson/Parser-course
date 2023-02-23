using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserCourseTest1.Calculator.Practice2;

public class Calculator
{
    Dictionary<string, double> _environment;

    public Calculator()
    {
        _environment= new Dictionary<string, double>();
    }

    public double Calculate(string expression)
    {
        Calculate calc = new(expression, _environment);
        _environment = calc.Result;
        return calc.Result["@"];
    }
}
