using FutureOfWorkAPI.Models.Enums;

namespace FutureOfWorkAPI.Models.Dtos;

public class ProfissionalCreateDto
{
    public string Nome { get; set; } = string.Empty;
    public string? AreaInteresse { get; set; }
    public NivelHabilidade NivelHabilidade { get; set; }
    public bool EstaEmpregado { get; set; }
}

public class ProfissionalDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? AreaInteresse { get; set; }
    public NivelHabilidade NivelHabilidade { get; set; }
    public bool EstaEmpregado { get; set; }
}
