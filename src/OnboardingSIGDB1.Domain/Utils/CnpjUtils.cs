namespace OnboardingSIGDB1.Domain.Utils;

public class CnpjUtils
{
    public static bool IsValid(string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj))
            return false;

        // 1. Limpa a máscara
        cnpj = new string(cnpj.Where(char.IsDigit).ToArray());

        // 2. CNPJ precisa ter exatamente 14 dígitos
        if (cnpj.Length != 14)
            return false;

        // 3. Elimina sequências repetidas (00000000000000, etc)
        if (cnpj.All(c => c == cnpj[0]))
            return false;

        // Pesos oficiais para o cálculo do CNPJ
        int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        var tempCnpj = cnpj.Substring(0, 12);
        var soma = 0;

        // Cálculo do primeiro dígito verificador
        for (int i = 0; i < 12; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

        var resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        var digito = resto.ToString();
        tempCnpj += digito;

        soma = 0;

        // Cálculo do segundo dígito verificador
        for (int i = 0; i < 13; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        digito += resto.ToString();

        // Verifica se o CNPJ termina com os dois dígitos calculados
        return cnpj.EndsWith(digito);
    }
}