using Microsoft.EntityFrameworkCore;
using FutureOfWorkAPI.Models;

namespace FutureOfWorkAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Profissional> Profissionais => Set<Profissional>();
    public DbSet<Curso> Cursos => Set<Curso>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProfissionalCurso>()
            .HasKey(pc => new { pc.ProfissionalId, pc.CursoId });

        modelBuilder.Entity<ProfissionalCurso>()
            .HasOne(pc => pc.Profissional)
            .WithMany(p => p.Cursos)
            .HasForeignKey(pc => pc.ProfissionalId);

        modelBuilder.Entity<ProfissionalCurso>()
            .HasOne(pc => pc.Curso)
            .WithMany(c => c.Profissionais)
            .HasForeignKey(pc => pc.CursoId);
    }
}

public static class SeedData
{
    public static void Seed(AppDbContext db)
    {
        if (!db.Cursos.Any())
        {
            db.Cursos.AddRange(
                new Curso { Titulo = "Fundamentos de IA", Categoria = "Tecnologia", CargaHoraria = 40 },
                new Curso { Titulo = "Análise de Dados", Categoria = "Dados", CargaHoraria = 50 },
                new Curso { Titulo = "Cibersegurança para Devs", Categoria = "Segurança", CargaHoraria = 30 }
            );
            db.SaveChanges();
        }

        if (!db.Profissionais.Any())
        {
            var p1 = new Profissional { Nome = "Ana Souza", AreaInteresse = "Dados", NivelHabilidade = "Júnior", EstaEmpregado = false };
            var p2 = new Profissional { Nome = "Carlos Lima", AreaInteresse = "IA", NivelHabilidade = "Pleno", EstaEmpregado = true };

            db.Profissionais.AddRange(p1, p2);
            db.SaveChanges();

            var ia = db.Cursos.First(c => c.Titulo.Contains("IA"));
            var dados = db.Cursos.First(c => c.Categoria == "Dados");

            db.Add(new ProfissionalCurso { ProfissionalId = p1.Id, CursoId = dados.Id });
            db.Add(new ProfissionalCurso { ProfissionalId = p2.Id, CursoId = ia.Id });

            db.SaveChanges();
        }
    }
}
