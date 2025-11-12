using System.ComponentModel.DataAnnotations;

namespace FutureOfWorkAPI.Models;

public class Profissional
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    [MaxLength(80)]
    public string? AreaInteresse { get; set; }

    [MaxLength(40)]
    public string? NivelHabilidade { get; set; }

    public bool EstaEmpregado { get; set; }

    public ICollection<ProfissionalCurso> Cursos { get; set; } = new List<ProfissionalCurso>();
}
