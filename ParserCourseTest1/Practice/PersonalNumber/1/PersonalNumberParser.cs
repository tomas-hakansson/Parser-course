namespace ParserCourseTest1.PersonalNumber.Practice1;

public class PersonalNumberParser
{
    public static (bool success, PersonalNumber? personalNumber) Parse(string source)
    {
        // personal-number  => year month day ('-' | '+') control-number
        // year             => [00-99]
        // month            => [01-12]
        // day              => [01-31]
        // the number of days differ per month.
        // control-number   => [0000-9999]

        source = source.Trim();

        (bool, PersonalNumber?) failedParse = (false, null);
        if (string.IsNullOrWhiteSpace(source))
            return failedParse;
        var length = source.Length;
        if (length < 11 || length > 11)
            return failedParse;
        var cursor = 0;

        PersonalNumber result = new();

        //year parsing:
        var (success, digits) = Digits(2);
        if (!success)
            return failedParse;
        result.Year = int.Parse(digits);
        //Note: year parsing, once it has reached this point, can never fail.

        //month parsing:
        (success, digits) = Digits(2);
        if (!success)
            return failedParse;
        var month = int.Parse(digits);
        if (month < 1 || month > 12)
            return failedParse;
        result.Month = month;

        //day parsing:
        (success, digits) = Digits(2);
        if (!success)
            return failedParse;
        var day = int.Parse(digits);
        var validDayCounts = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        var validDayCount = validDayCounts[month - 1];
        if (day < 1 || day > validDayCount)
            return failedParse;
        result.Day = day;

        //separator parsing:
        var separator = source[cursor];
        cursor++;
        if (!"-+".Contains(separator))
            return failedParse;
        result.SetSeparator(separator);

        //control number parsing:
        (success, var controlNumber) = Digits(4);
        if (!success)
            return failedParse;
        result.ControlNumber = controlNumber;
        //Note: control number parsing, once it has reached this point, can never fail.

        return (true, result);

        (bool success, string digits) Digits(int count)
        {
            var result = string.Empty;
            var length = cursor + count;
            for (; cursor < length; cursor++)
            {
                var cc = source[cursor];

                if (!char.IsDigit(cc))
                    return (false, "");
                result += cc;
            }

            return (true, result);
        }
    }
}
