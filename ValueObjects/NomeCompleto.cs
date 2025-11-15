namespace FutureOfWorkAPI.ValueObjects;

/// <summary>
/// Value Object para representar o nome completo de um profissional.
/// Ele encapsula regras de negócio relacionadas ao nome.
/// </summary>
public class NomeCompleto
{
    public string Valor { get; }

    public NomeCompleto(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new ArgumentException("Nome não pode ser vazio.", nameof(valor));

        if (valor.Length < 3)
            throw new ArgumentException("Nome deve ter pelo menos 3 caracteres.", nameof(valor));

        Valor = valor.Trim();
    }

    public override string ToString() => Valor;
}
