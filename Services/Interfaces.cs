using FutureOfWorkAPI.Models;

namespace FutureOfWorkAPI.Services;

public interface IProfissionalService
{
    Task<IEnumerable<Profissional>> GetAllAsync();
    Task<Profissional?> GetByIdAsync(int id);
    Task<Profissional> CreateAsync(Profissional p);
    Task<bool> UpdateAsync(int id, Profissional p);
    Task<bool> DeleteAsync(int id);
}

public interface ICursoService
{
    Task<IEnumerable<Curso>> GetAllAsync();
    Task<Curso?> GetByIdAsync(int id);
    Task<Curso> CreateAsync(Curso c);
    Task<bool> UpdateAsync(int id, Curso c);
    Task<bool> DeleteAsync(int id);
}
