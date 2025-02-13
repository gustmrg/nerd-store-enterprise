namespace NSE.Core.DomainObjects;

public class Cpf
{
    public Cpf() { }

    public Cpf(string number)
    {
        if (!Validate(number)) throw new DomainException("CPF invalid");
        
        Number = number;
    }
    
    public const int MaxLength = 11;
    public string Number { get; private set; }

    public static bool Validate(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf)) return false;

        cpf = new string(cpf.Where(char.IsDigit).ToArray());

        if (cpf.Length != 11 || cpf.Distinct().Count() == 1) return false;

        int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCpf = cpf.Substring(0, 9);
        int soma = tempCpf.Select((t, i) => (t - '0') * multiplicador1[i]).Sum();
        int resto = soma % 11;
        int digito1 = resto < 2 ? 0 : 11 - resto;

        tempCpf += digito1;
        soma = tempCpf.Select((t, i) => (t - '0') * multiplicador2[i]).Sum();
        resto = soma % 11;
        int digito2 = resto < 2 ? 0 : 11 - resto;

        return cpf.EndsWith(digito1.ToString() + digito2.ToString());
    }
}