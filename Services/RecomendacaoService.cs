using FutureOfWorkAPI.Data;
using FutureOfWorkAPI.Models.Dtos;
using FutureOfWorkAPI.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace FutureOfWorkAPI.Services;

public class RecomendacaoService : IRecomendacaoService
{
    private readonly AppDbContext _db;

    public RecomendacaoService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<CursoDto>> RecomendarCursosParaProfissionalAsync(int profissionalId)
    {
        // Carrega o profissional
        var profissional = await _db.Profissionais
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == profissionalId);

        if (profissional is null)
        {
            // Sem profissional -> nada a recomendar
            return Enumerable.Empty<CursoDto>();
        }

        // VO com a área de interesse
        var areaVO = new AreaInteresseVO(profissional.AreaInteresse);

        // Base da consulta
        IQueryable<Models.Curso> query = _db.Cursos.AsNoTracking();

        // Regras simples usando o VO
        if (areaVO.EhDados)
        {
            query = query.Where(c =>
                c.Categoria != null &&
                EF.Functions.Like(c.Categoria.ToLower(), "%dado%"));
        }
        else if (areaVO.EhIA)
        {
            query = query.Where(c =>
                c.Categoria != null &&
                (EF.Functions.Like(c.Categoria.ToLower(), "%ia%") ||
                 EF.Functions.Like(c.Categoria.ToLower(), "%intelig%")));
        }
        else if (areaVO.EhTecnologia)
        {
            query = query.Where(c =>
                c.Categoria != null &&
                EF.Functions.Like(c.Categoria.ToLower(), "%tecnologia%"));
        }
        // Se não bater em nenhuma regra, recomenda todos os cursos

        var cursos = await query.ToListAsync();

        return cursos.Select(c => new CursoDto
        {
            Id = c.Id,
            Titulo = c.Titulo,
            Categoria = c.Categoria,
            CargaHoraria = c.CargaHoraria
        });
    }
}
