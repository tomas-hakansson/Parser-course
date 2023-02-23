using System.Data;
using System.Globalization;

namespace ParserCourseTest1.Calculator.Practice1
{
    public class Calculate
    {
        public bool Success { get; set; }
        public Dictionary<string, double> Result { get; set; }

        readonly string _expression;
        int _index;

        public Calculate(string expression, Dictionary<string, double> environment)
        {
            Success = false;
            Result = environment; // Ponder: for readability maybe have a separate environment variable?
            _expression = expression;
            _index = 0;

            Evaluate();
        }

        private void Evaluate()
        {
            (Success, var value) = Expression();
            Result["@"] = value; // Note: '@' is the variable containing the latest calculation.
        }

        // expression => (variable ':')* sum
        (bool success, double value) Expression()
        {
            // expression => (variable ':')* sum

            // Note: Checks for eos.
            if (_index >= _expression.Length) return (false, -1);
            var cc = _expression[_index];
            var names = new List<string>();
            while (char.IsLetter(cc))
            {
                var (name, newIndex) = VariableName();
                // Note: Checks for eos.
                if (newIndex >= _expression.Length) break;// Note: 'name' is a variable.
                cc = _expression[newIndex];
                if (cc == ':')
                {
                    names.Add(name);
                    _index = newIndex + 1;
                    // Note: Checks for eos.
                    if (_index >= _expression.Length) return (false, -1);
                    cc = _expression[_index];
                }
            }
            var (success, sum) = Sum();
            if (!success) return (false, -1);
            foreach (var name in names)
                Result[name] = sum;
            return (true, sum);
        }

        // Variable => letter*
        (string name, int newIndex) VariableName()
        {
            // Variable => letter*

            var name = string.Empty;
            var cc = _expression[_index];
            var newIndex = _index;
            while (char.IsLetter(cc))
            {
                newIndex++;
                name += cc;
                // Note: Checks for eos.
                if (newIndex >= _expression.Length) break;
                cc = _expression[newIndex];
            }
            return (name, newIndex);
        }

        // sum => product (('+' | '-') product)*
        (bool success, double value) Sum()
        {
            // sum => product (('+' | '-') product)*

            var (success, x) = Product();
            //var (success, x) = Number();
            if (!success) return (false, -1);

            var match = true;
            while (match)
            {
                var (charSuccess, charValue) = EitherChar('+', '-');
                if (charSuccess)
                {
                    (success, var y) = Product();
                    if (!success) return (false, -1);

                    x = charValue switch
                    {
                        '+' => x + y,
                        '-' => x - y,
                        _ => throw new Exception()
                    };
                }
                else
                    match = false;
            }
            return (true, x);
        }

        // product => atom (('*' | '/') atom)*
        (bool success, double value) Product()
        {
            // product => number ('*' number)*

            var (success, x) = Atom();
            //var (success, x) = Number();
            if (!success) return (false, -1);

            var match = true;
            while (match)
            {
                var (charSuccess, charValue) = EitherChar('*', '/');
                if (charSuccess)
                {
                    (success, var y) = Atom();
                    if (!success) return (false, -1);

                    x = charValue switch
                    {
                        '*' => x * y,
                        '/' => x / y,
                        _ => throw new Exception()
                    };
                }
                else
                    match = false;
            }
            return (true, x);
        }

        // atom   => number | '(' expression ')' | variable
        (bool success, double value) Atom()
        {
            // atom   => number | '(' expression ')' | variable

            // Note: Checks for eos.
            if (_index >= _expression.Length) return (false, -1);
            var cc = _expression[_index];
            if (char.IsDigit(cc) || cc == '-')
            {
                return Number();
            }
            else if (cc == '(')
            {
                return ParenthesizedExpression();
            }
            else if (char.IsLetter(cc))
            {
                return VariableValue();
            }
            return (false, -1);
        }

        // number => digit*
        // number => number '.' number
        // number => '_' number
        // digit  => 0-9
        (bool success, double value) Number()
        {
            // number => digit*
            // number => number '.' number
            // number => '_' number
            // digit  => 0-9

            var stringValue = string.Empty;
            var cc = _expression[_index];
            if (cc == '-')
            {
                stringValue += cc;
                _index++;
                // Note: Checks for eos.
                if (_index >= _expression.Length) return (false, -1);
                cc = _expression[_index];
                if (!char.IsDigit(cc)) return (false, -1);
            }
            while (char.IsDigit(cc))
            {
                _index++;
                stringValue += cc;
                // Note: Checks for eos.
                if (_index >= _expression.Length)
                    break;
                cc = _expression[_index];
                if (cc == '.')
                {
                    stringValue += cc;
                    _index++;
                    // Note: Checks for eos.
                    if (_index >= _expression.Length) return (false, -1);
                    cc = _expression[_index];
                    if (!char.IsDigit(cc)) return (false, -1);
                }
            }
            var value = double.Parse(stringValue, CultureInfo.InvariantCulture);
            return (true, value);
        }

        // parenthesized expression => '(' expression ')'
        (bool success, double value) ParenthesizedExpression()
        {
            // parenthesized expression => '(' expression ')'
            // ToDo: check for eos
            var leftParen = Char('(');
            if (leftParen)
            {
                var result = Expression();
                var rightParen = Char(')');
                if (!rightParen) return (false, -1);
                return result;
            }
            return (false, -1);
        }

        // Variable => letter+
        (bool success, double value) VariableValue()
        {
            // Variable => letter+

            var variable = string.Empty;
            var cc = _expression[_index];
            while (char.IsLetter(cc))
            {
                _index++;
                variable += cc;
                // Note: Checks for eos.
                if (_index >= _expression.Length) break;
                cc = _expression[_index];
            }
            return (true, value: Result[variable]);
        }

        (bool charSuccess, char charValue) EitherChar(params char[] chars)
        {
            foreach (var cc in chars)
                if (Char(cc)) return (true, cc);
            return (false, '\0');
        }

        bool Char(char c)
        {
            // Note: Checks for eos.
            if (_index >= _expression.Length) return false;
            var cc = _expression[_index];
            if (c == cc)
            {
                _index++;
                return true;
            }
            return false;
        }
    }
}