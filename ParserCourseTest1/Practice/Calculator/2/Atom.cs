using System.Threading.Tasks;

namespace ParserCourseTest1.Calculator.Practice2;
internal enum AtomType
{
    Number,
    Variable
}

internal class Atom
{
    public double Number;
    public string VariableName;
    public AtomType Type;

    public Atom(double number)
    {
        Number = number;
        VariableName = string.Empty;
        Type = AtomType.Number;
    }

    public Atom(string variableName)
    {
        Number = -1;
        VariableName = variableName;
        Type = AtomType.Variable;
    }

    public static Atom operator +(Atom x, Atom y) => new Atom(x.Number + y.Number);
    public static Atom operator -(Atom x, Atom y) => new Atom(x.Number - y.Number);
    public static Atom operator *(Atom x, Atom y) => new Atom(x.Number * y.Number);
    public static Atom operator /(Atom x, Atom y) => new Atom(x.Number / y.Number);
}