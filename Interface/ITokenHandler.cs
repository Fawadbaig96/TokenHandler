namespace TokenHandler.Interface;

public interface ITokenHandler
{
    public string ReplaceTokens(string body, Dictionary<string, string> tokenValues, string? loopSource = null);
    public List<string> ExtractToken(string body);
}
