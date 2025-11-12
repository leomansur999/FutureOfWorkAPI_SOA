using System.ComponentModel.DataAnnotations;

namespace FutureOfWorkAPI.Models;

public class Curso
{
    public int Id { get; set; }

    [Required, MaxLength(120)]
    public string Titulo { get; set; } = string.Empty;

    [MaxLength(60)]
    public string? Categoria { get; set; }

    public int CargaHoraria { get; set; }

    public ICollection<ProfissionalCurso> Profissionais { get; set; } = new List<ProfissionalCurso>();
}
