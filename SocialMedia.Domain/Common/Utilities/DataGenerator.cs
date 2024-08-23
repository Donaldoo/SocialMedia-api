using System.Text;

namespace TicketSystem.Domain.Common.Utilities;

public static class DataGenerator
{
    public static T RandomEnumValue<T>()
    {
        var v = Enum.GetValues(typeof(T));
        return (T)v.GetValue(new Random().Next(v.Length));
    }

    public static int RandomNumber(int min = 1, int max = 100000000) => new Random().Next(min, max);

    public static string RandomAlphaNumericString(int length = 10)
    {
        var random = new Random();
        const string chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static string RandomEmail(string domain = "cloudmeet.us")
    {
        return $"{RandomAlphaNumericString(10)}@{domain}";
    }

    public static string RandomPassword()
    {
        var random = new Random();
        const string specialChar = "!@#$%^&*,.";
        var passwords = new[]
        {
            "gwEe4vsk", "xxtXr7na", "wt7MwT4A", "Zfw2s8Vm", "5vks4dLa", "wVd8Ve8n", "C87xWTH4", 
            "D6H7aHWx", "ZmQjk2tK", "VbGXA5hq","hLHEkr5B", "NRreTgJ3", "BJCMh8jC", "Hm2SXmPY",
            "H9FEfMvm", "2jF9VKNW", "YmVT7sDx", "JCn936qJ", "btjhJ76Z", "CRXL7ums", "MxxZD9QW",
            "UK8jTLvn", "3sthLJVP", "fydKehC6", "Nay4c8qU"
        };

        return passwords[random.Next(passwords.Length)] + specialChar[random.Next(specialChar.Length)];
    }

    public static string RandomPersonName()
    {
        var random = new Random();
        var names = new[]
        {
            "john", "alice", "bob", "emily", "david", "sarah", "tom", "laura",
            "steve", "anna", "mike", "jessica", "mark", "emma", "james", "olivia",
            "robert", "ava", "william", "sophia", "michael", "mia", "richard", "charlotte",
            "joseph", "amelia", "charles", "harper", "thomas", "evelyn", "christopher", "abby",
            "daniel", "madison", "paul", "ella", "matthew", "scarlett", "donald", "grace",
            "george", "chloe", "kenneth", "victoria", "steven", "aubrey", "edward", "stella",
            "brian", "natalie", "ronald", "zoe", "anthony", "lily", "kevin", "hazel",
            "jason", "violet", "jeff", "luna", "frank", "bailey", "scott", "addison",
            "greg", "ellie", "brandon", "layla", "ben", "pippa", "samuel", "mila",
            "fred", "claire", "derek", "sienna", "patrick", "aaliyah", "sean", "ivy",
            "alex", "kylie", "nathan", "tessa", "peter", "piper", "jeremy", "ruby",
            "ian", "serena", "carl", "willow", "gary", "fiona", "leon", "hannah",
            "adam", "olive", "ernest", "harriet", "philip", "imogen", "craig", "maisie",
            "alan", "freya", "shane", "lexi", "kyle", "elise", "neil", "francesca",
            "barry", "molly", "stuart", "megan", "elliott", "carolyn", "seth", "joanna",
            "dale", "rebecca", "brent", "diana", "devon", "sylvia", "owen", "erica", "blake",
            "tanya", "cody", "denise", "dominic", "lydia", "marshall", "rose", "norman",
            "tara", "clifford", "dawn", "warren", "monica", "clarence", "rita", "kirk", "joanne", "daryl", "naomi",
            "alberto", "candace", "arthur", "maureen", "jordan", "carrie", "corey", "karen",
            "raul", "tina", "brad", "judith", "darren", "robyn", "neal", "gina",
            "jamie", "kathleen", "brett", "susan", "jon", "rachel", "andre", "sharon",
            "karl", "paula", "gordon", "nancy", "hunter", "lauren", "kurt", "heather",
            "ernie", "julia", "tyler", "sandy", "aaron", "beth", "terry", "bethany",
            "lucas", "holly", "zachary", "melinda", "clayton", "angela", "lee", "lillian",
            "ethan", "courtney", "wesley", "claire", "dennis", "elaine", "bruce", "janet",
            "bryan", "joyce", "allan", "samantha", "edwin", "deborah", "roger", "miranda",
            "howard", "tracy", "ralph", "doris", "charlie", "eleanor", "herbert", "jillian",
            "gene", "june", "roy", "stacy", "don", "whitney"
        };

        return names[random.Next(names.Length)];
    }

    public static string RandomImageName(string extension = "jpg")
    {
        return $"{RandomAlphaNumericString(10)}.{extension}";
    }

    public static string RandomVideoFile(string extension = ".mp4")
    {
        return $"{RandomAlphaNumericString(10)}.{extension}";
    }

    public static string RandomFileUrl(string filename)
    {
        return $"https://files.cloudmeet.us/{filename}";
    }

    public static string RandomPublicName()
    {
        return $"{RandomPersonName().ToLower()} {RandomPersonName().ToLower()}";
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
}