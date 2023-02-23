using System.Globalization;

namespace ParserCourseTest1.Calculator.Practice2;

public class Calculate
{
    public bool Success { get; set; }
    public Dictionary<string, double> Result { get; set; }

    readonly string _expression;
    int _index;

    public Calculate(string expression, Dictionary<string, double> environment)
    {
        Result = environment;
        _expression = expression;
        _index = 0;

        (Success, var result) = Expression();
        Result["@"] = result.Number;
    }

    /*
     * expression => atom (operator atom)*
     * operator   => '+' | '-' | '*' | '/' | ':'
     * atom       => number | '(' expression ')' | variable
     * number     => '-'? digit* '.'? digit+
     * variable   => letter+
     */

    // expression => atom (operator atom)*
    // operator   => '+' | '-' | '*' | '/'
    (bool success, Atom value) Expression()
    {
        var (isValue, left) = Atom();
        if (!isValue) return (false, new Atom(-1));
        if (left.Type == AtomType.Variable)
            left = new Atom(Result[left.VariableName]);

        var operators = A('+', '-', '*', '/', ':');
        var (isOp, opVal) = EitherChar(operators);
        while (isOp)
        {
            (isValue, var right) = Atom();
            if (!isValue) return (false, new Atom(-1));
            if (opVal != ':' && right.Type == AtomType.Variable)
                right = new Atom(Result[right.VariableName]);

            left = opVal switch
            {
                '+' => left + right,
                '-' => left - right,
                '*' => left * right,
                '/' => left / right,
                ':' => Assignment(left, right),
                _ => throw new Exception()
            };
            (isOp, opVal) = EitherChar(operators);
        }
        return (true, left);

        static char[] A(params char[] chars) => chars;
    }


    Atom Assignment(Atom value, Atom variable)
    {
        if (variable.Type != AtomType.Variable) return value;//Hack: Bad error handling, bad error handling!!!

        Result[variable.VariableName] = value.Number;
        return value;
    }

    // atom => number | '(' expression ')' | variable
    (bool success, Atom value) Atom()
    {
        // Note: Checks for eos.
        if (_index >= _expression.Length) return (false, new Atom(-1));
        var cc = _expression[_index];
        if (char.IsDigit(cc) || cc == '-' || cc == '.')
        {
            return Number();
        }
        else if (cc == '(')
        {
            return ParenthesizedExpression();
        }
        else if (char.IsLetter(cc))
        {
            return Variable();
        }
        return (false, new Atom(-1));
    }

    // number => '-'? digit* '.'? digit+
    (bool success, Atom value) Number()
    {
        var stringValue = string.Empty;
        var cc = _expression[_index];

        if (cc == '-')
        {
            stringValue += cc;
            _index++;
            // Note: Checks for eos.
            if (_index >= _expression.Length) return (false, new Atom(-1));
            cc = _expression[_index];
        }

        while (char.IsDigit(cc))
        {
            stringValue += cc;
            _index++;

            // Note: Checks for eos.
            if (_index >= _expression.Length) break;
            cc = _expression[_index];
        }

        if (cc == '.')
        {
            stringValue += cc;
            _index++;
            // Note: Checks for eos.
            if (_index >= _expression.Length) return (false, new Atom(-1));
            cc = _expression[_index];
            if (!char.IsDigit(cc)) return (false, new Atom(-1));
            while (char.IsDigit(cc))
            {
                stringValue += cc;
                _index++;

                // Note: Checks for eos.
                if (_index >= _expression.Length) break;
                cc = _expression[_index];
            }
        }

        var value = double.Parse(stringValue, CultureInfo.InvariantCulture);
        return (true, new Atom(value));
    }

    (bool success, Atom value) ParenthesizedExpression()
    {
        Char('(');
        var result = Expression();
        var rightParen = Char(')');
        if (rightParen) return result;
        return (false, new Atom(-1));
    }

    // variable => letter+
    (bool success, Atom value) Variable()
    {
        var variableName = string.Empty;
        var cc = _expression[_index];

        while (char.IsLetter(cc))
        {
            variableName += cc;
            _index++;
            // Note: Checks for eos.
            if (_index >= _expression.Length) break;
            cc = _expression[_index];
        }

        return (true, new Atom(variableName));
    }

    (bool charSuccess, char charValue) EitherChar(char[] chars)
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