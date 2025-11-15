using FutureOfWorkAPI.Data;
using FutureOfWorkAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FutureOfWorkAPI.Services;

public class ProfissionalService : IProfissionalService
{
    private readonly AppDbContext _db;

    public ProfissionalService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Profissional>> GetAllAsync()
    {
        return await _db.Profissionais.AsNoTracking().ToListAsync();
    }

    public async Task<Profissional?> GetByIdAsync(int id)
    {
        return await _db.Profissionais.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Profissional> CreateAsync(Profissional p)
    {
        _db.Profissionais.Add(p);
        await _db.SaveChangesAsync();
        return p;
    }

    public async Task<bool> UpdateAsync(int id, Profissional p)
    {
        var existing = await _db.Profissionais.FindAsync(id);
        if (existing is null)
        {
            return false;
        }

        existing.Nome = p.Nome;
        existing.AreaInteresse = p.AreaInteresse;
        existing.NivelHabilidade = p.NivelHabilidade;
        existing.EstaEmpregado = p.EstaEmpregado;

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _db.Profissionais.FindAsync(id);
        if (existing is null)
        {
            return false;
        }

        _db.Profissionais.Remove(existing);
        await _db.SaveChangesAsync();
        return true;
    }
}
