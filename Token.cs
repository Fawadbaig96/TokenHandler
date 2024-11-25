using System.Text.Json;
using System.Text.RegularExpressions;

namespace TokenHandler
{
    public static class Token
    {
        public static string ReplaceTokens(string body, Dictionary<string, string> tokenValues, string? loopSource = null)
        {
            // Iterate through each token-value pair in the dictionary
            foreach (var token in tokenValues)
            {
                // Replace the token in the HTML body with its corresponding value
                if (token.Value.Split(',').ToList().Count > 1)
                {
                    var listValues = token.Value.Split(',').ToList();
                    string placeholder = token.Key;
                    string rows = "";

                    var source = JsonSerializer.Deserialize<List<string>>(loopSource);
                    var sourceName = source!.FirstOrDefault(x => x.Contains(placeholder));

                    foreach (var value in listValues)
                    {
                        var actualValue = sourceName!.Replace(placeholder, value);
                        var newRow = actualValue;
                        rows += $"{newRow}";
                    }
                    body = body.Replace(sourceName!, rows);

                }
                else if (token.Value is string singleValue)
                {
                    // Replace the token in the HTML body with its corresponding value
                    body = body.Replace(token.Key, singleValue);
                }

            }

            return body;
        }
        public static List<string> ExtractToken(string body)
        {
            List<string> tokens = new List<string>();

            string pattern = @"\{\{([^\}]+)\}\}";
            Regex regex = new Regex(pattern);

            MatchCollection matches = regex.Matches(body);

            foreach (Match match in matches)
            {
                // Extract the token without the curly braces
                string token = match.Value;
                tokens.Add(token);
            }

            return tokens;
        }
    }
}
