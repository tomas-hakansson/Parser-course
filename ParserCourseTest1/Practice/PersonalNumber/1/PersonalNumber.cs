namespace ParserCourseTest1.PersonalNumber.Practice1;

public enum Separator
{
    BelowHundred,
    AHundredOrAbove
}
public class PersonalNumber
{
    public int Year { get; set; }
    public int Month { get; set; }
    public int Day { get; set; }
    public string ControlNumber { get; set; }
    public Separator Separator { get; set; }

    public PersonalNumber()
    {
        ControlNumber = string.Empty;
    }

    internal void SetSeparator(char separator)
    {
        switch (separator)
        {
            case '-':
                Separator = Separator.BelowHundred;
                break;
            case '+':
                Separator = Separator.AHundredOrAbove;
                break;
        }
    }
}
