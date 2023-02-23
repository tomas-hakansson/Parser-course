namespace ParserCourseTest1.EmailAddress.Practice1;

public class EmailAddressParser
{
    public static (bool success, EmailAddress? emailAddress) Parse(string source)
    {
        /* email-address    => name '@' start-domain '.' end-domain
         * name             => [a-zA-Z.] ('+' [a-zA-Z])?
         * The period ('.') is ignored by the parser as is the optional ('+') and following string.
         * start-domain     => [a-zA-Z]
         * end-domain       => [a-zA-Z]
         */

        source = source.Trim();

        (bool, EmailAddress?) failedParsing = (false, null);
        if (string.IsNullOrWhiteSpace(source))
            return failedParsing;

        var cursor = 0;

        //parse name:
        var (success, name) = TakeUntil('@');
        if (!success || string.IsNullOrWhiteSpace(name))
            return failedParsing;
        EmailAddress ea = new();
        name = name.Replace(".", String.Empty);
        var indexOfPlus = name.IndexOf('+');
        if (indexOfPlus >= 0)
            name = name[..indexOfPlus];
        ea.Name = name;

        //parse '@'
        cursor++;

        //parse start-domain
        (success, var start_domain) = TakeUntil('.');
        if (!success || string.IsNullOrWhiteSpace(start_domain))
            return failedParsing;
        ea.StartDomain = start_domain;

        //parse '.'
        cursor++;

        //parse end-domain
        (success, var end_domain) = TakeUntil();
        if (!success || string.IsNullOrWhiteSpace(end_domain))
            return failedParsing;
        ea.EndDomain = end_domain;

        return (true, ea);

        (bool success, string result) TakeUntil(char? character = null)
        {
            var result = string.Empty;

            //Note: take the string until character or whitespace or end of string.
            // if character not null and whitespace or eos then error.

            var foundCharacter = false;
            for (; cursor < source.Length; cursor++)
            {
                var cc = source[cursor];

                if (character != null && cc == character)
                {
                    foundCharacter = true;
                    break;
                }
                if (char.IsWhiteSpace(cc))
                    break;

                result += cc;
            }
            if (character != null && !foundCharacter)
                return (false, result);

            return (true, result);
        }
    }

}
