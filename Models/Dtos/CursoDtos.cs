namespace FutureOfWorkAPI.Models.Dtos;

public class CursoCreateDto
{
    public string Titulo { get; set; } = string.Empty;
    public string? Categoria { get; set; }
    public int CargaHoraria { get; set; }
}

public class CursoDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Categoria { get; set; }
    public int CargaHoraria { get; set; }
}
