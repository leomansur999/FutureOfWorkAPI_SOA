using FutureOfWorkAPI.Models.Dtos;

namespace FutureOfWorkAPI.Services;

public interface IRecomendacaoService
{
    Task<IEnumerable<CursoDto>> RecomendarCursosParaProfissionalAsync(int profissionalId);
}
