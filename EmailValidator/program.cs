using System;

string[] emails = {"jan.novak@mensagymnazium.cz", "jan.novak", "jan@.cz"};

bool validateEmail(string input)
{
    var email = input.ToCharArray();
    var emailAllowedChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz+-_~!#$%&'./=^'{}|";
    var domainAllowedChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-";
    var state = "NAME"; // NAME, AT, DOMAIN, DOT, FAIL

    var hasAtSymbol = false;
    var hasDotInDomain = false;

    foreach (var c in email)
    {
        switch (state)
        {
            case "NAME":
                if (emailAllowedChars.Contains(c))
                {
                    state = "NAME";
                }
                else if (c == '@')
                {
                    state = "AT";
                    hasAtSymbol = true;
                }
                else
                {
                    state = "FAIL";
                }
                break;
            case "AT":
                if (domainAllowedChars.Contains(c))
                {
                    state = "DOMAIN";
                }
                else
                {
                    state = "FAIL";
                }
                break;
            case "DOMAIN":
                if (domainAllowedChars.Contains(c))
                {
                    state = "DOMAIN";
                }
                else if (c == '.')
                {
                    state = "DOT";
                    hasDotInDomain = true;
                }
                else
                {
                    state = "FAIL";
                }
                break;
            case "DOT":
                if (domainAllowedChars.Contains(c))
                {
                    state = "DOMAIN";
                }
                else
                {
                    state = "FAIL";
                }
                break;
            case "FAIL":
                return false;
        }
    }

    return hasAtSymbol && hasDotInDomain && state == "DOMAIN";
}

foreach (var email in emails)
{
    Console.WriteLine(email);
    Console.WriteLine(validateEmail(email));
    Console.WriteLine();
}