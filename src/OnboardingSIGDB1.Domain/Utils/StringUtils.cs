namespace OnboardingSIGDB1.Domain.Utils;

public static class StringUtils
{
    public static string removemask(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
            return string.Empty;

        return new string(valor.Where(char.IsDigit).ToArray());
    }
}