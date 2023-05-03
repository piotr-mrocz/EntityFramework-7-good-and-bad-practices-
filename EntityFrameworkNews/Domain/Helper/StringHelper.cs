namespace EntityFrameworkNews.Helper;

public static class StringHelper
{
    private static Dictionary<string, string> _normalizeDictionary = new Dictionary<string, string>()
    {
        {"ą", "a"},
        {"ć", "c"},
        {"ę", "e"},
        {"ł", "l"},
        {"ń", "n"},
        {"ó", "o"},
        {"ś", "s"},
        {"ź", "z"},
        {"ż", "z"}, 
        {"Ą", "A"},
        {"Ć", "C"},
        {"Ę", "E"},
        {"Ł", "L"},
        {"Ń", "N"},
        {"Ó", "O"},
        {"Ś", "S"},
        {"Ź", "Z"},
        {"Ż", "Z"}
    };

    public static string ChangePolishLettersToEnglish(string text)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;

        foreach (var (key, value) in _normalizeDictionary)
        {
            text = text.Replace(key, value);
        }

        return text;
    }
}
