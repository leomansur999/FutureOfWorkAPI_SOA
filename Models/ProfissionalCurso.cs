namespace FutureOfWorkAPI.Models;

public class ProfissionalCurso
{
    public int ProfissionalId { get; set; }
    public Profissional Profissional { get; set; } = default!;

    public int CursoId { get; set; }
    public Curso Curso { get; set; } = default!;
}
