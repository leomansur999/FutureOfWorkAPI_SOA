namespace FutureOfWorkAPI.Models.ValueObjects;

public class AreaInteresseVO
{
    public string Valor { get; }

    public AreaInteresseVO(string? valorBruto)
    {
        Valor = valorBruto?.Trim() ?? string.Empty;
    }

    public bool EhDados =>
        Valor.Contains("dado", StringComparison.OrdinalIgnoreCase) ||
        Valor.Contains("data", StringComparison.OrdinalIgnoreCase) ||
        Valor.Contains("analytics", StringComparison.OrdinalIgnoreCase);

    public bool EhTecnologia =>
        Valor.Contains("tecnologia", StringComparison.OrdinalIgnoreCase) ||
        Valor.Contains("dev", StringComparison.OrdinalIgnoreCase) ||
        Valor.Contains("programa", StringComparison.OrdinalIgnoreCase);

    public bool EhIA =>
        Valor.Contains("ia", StringComparison.OrdinalIgnoreCase) ||
        Valor.Contains("inteligência artificial", StringComparison.OrdinalIgnoreCase) ||
        Valor.Contains("inteligencia artificial", StringComparison.OrdinalIgnoreCase);

    public override string ToString() => Valor;
}
