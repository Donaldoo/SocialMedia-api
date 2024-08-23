using System.Text;

namespace TicketSystem.Domain.Common.Utilities;

public static class StringGenerator
{
    public static string RandomAlphaNumericString(int length = 10)
    {
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static string RandomEmail(string email = "peoplekeep.com")
    {
        return $"{RandomAlphaNumericString(10)}@{email}";
    }

    public static string RandomPhoneNumber()
    {
        return $"({RandomNumericStringFromOne(1)}{RandomNumericString(2)}) {RandomNumericString(3)}-{RandomNumericString(4)}";
    }

    public static string RandomName()
    {
        var random = new Random();
        var names = new[]
        {
            "aaron", "abdul", "abe", "abel", "abraham", "adam", "adan", "adolph", "abby", "abigail", "adele",
            "adrian"
        };

        return names[random.Next(names.Length)];
    }

    public static string RandomImageName(string extension = "jpg")
    {
        return $"{RandomAlphaNumericString(10)}.{extension}";
    }

    public static string RandomPublicName()
    {
        return $"{RandomAlphaNumericString(10).ToLower()}";
    }

    public static string LoremIpsum(int maxWords = 160, int maxSentences = 1, int numParagraphs = 1)
    {
        var words = new[]
        {
            "lorem", "ipsum", "dolor", "sit", "amet", "consectetuer",
            "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod",
            "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat"
        };

        const int minWords = 10;
        const int minSentences = 1;
        var rand = new Random();
        var numSentences = rand.Next(maxSentences - minSentences)
                           + minSentences + 1;
        var numWords = rand.Next(maxWords - minWords) + minWords + 1;

        var result = new StringBuilder();

        for (var p = 0; p < numParagraphs; p++)
        {
            result.Append("<p>");
            for (var s = 0; s < numSentences; s++)
            {
                for (var w = 0; w < numWords; w++)
                {
                    if (w > 0)
                    {
                        result.Append(" ");
                    }

                    result.Append(words[rand.Next(words.Length)]);
                }

                result.Append(". ");
            }

            result.Append("</p>");
        }

        return result.ToString();
    }

    public static string RandomNumericString(int length = 10)
    {
        var random = new Random();
        var s = string.Empty;
        for (var i = 0; i < length; i++)
            s = string.Concat(s, random.Next(10).ToString());
        return s;
    }

    public static string RandomNumericStringFromOne(int length = 10)
    {
        var random = new Random();
        var s = string.Empty;
        for (var i = 0; i < length; i++)
            s = string.Concat(s, random.Next(1,10).ToString());
        return s;
    }
}