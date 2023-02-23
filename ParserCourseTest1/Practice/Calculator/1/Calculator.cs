using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserCourseTest1.Calculator.Practice1
{
    public class Calculator
    {
        Dictionary<string, double> _environment;
        public Calculator()
        {
            _environment = new Dictionary<string, double>();
        }

        public double Calculate(string expression)
        {
            var result = Expression(expression);
            // ToDo: Update existing dictionary instead of overwriting old one.
            _environment = result;
            return result["@"]; // Note: "@" is the latest calculation.
        }

        Dictionary<string, double> Expression(string expression)
        {
            Calculate c = new(expression, _environment);
            return c.Success ? c.Result : throw new ArgumentException();
        }
    }
}
