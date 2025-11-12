using FutureOfWorkAPI.Data;
using FutureOfWorkAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FutureOfWorkAPI.Services;

public class CursoService : ICursoService
{
    private readonly AppDbContext _db;
    public CursoService(AppDbContext db) => _db = db;

    public async Task<IEnumerable<Curso>> GetAllAsync()
    {
        return await _db.Cursos
            .Include(c => c.Profissionais)
            .ThenInclude(pc => pc.Profissional)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Curso?> GetByIdAsync(int id)
    {
        return await _db.Cursos
            .Include(c => c.Profissionais)
            .ThenInclude(pc => pc.Profissional)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Curso> CreateAsync(Curso c)
    {
        _db.Cursos.Add(c);
        await _db.SaveChangesAsync();
        return c;
    }

    public async Task<bool> UpdateAsync(int id, Curso c)
    {
        var existing = await _db.Cursos.FindAsync(id);
        if (existing is null) return false;

        existing.Titulo = c.Titulo;
        existing.Categoria = c.Categoria;
        existing.CargaHoraria = c.CargaHoraria;

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _db.Cursos.FindAsync(id);
        if (existing is null) return false;
        _db.Cursos.Remove(existing);
        await _db.SaveChangesAsync();
        return true;
    }
}
