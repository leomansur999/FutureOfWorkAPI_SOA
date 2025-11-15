using FutureOfWorkAPI.Models;
using FutureOfWorkAPI.Models.Dtos;
using FutureOfWorkAPI.Models.Enums;
using FutureOfWorkAPI.Models.Shared;
using FutureOfWorkAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FutureOfWorkAPI.Controllers;

[ApiController]
[Route("api/" + ApiVersions.V1 + "/[controller]")]
public class ProfissionaisController : ControllerBase
{
    private readonly IProfissionalService _service;

    public ProfissionaisController(IProfissionalService service)
    {
        _service = service;
    }

    // ============================
    // GET /api/v1/Profissionais
    // ============================
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ProfissionalDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IEnumerable<ProfissionalDto>>>> GetAll()
    {
        var list = await _service.GetAllAsync();

        var dtos = list.Select(p => new ProfissionalDto
        {
            Id = p.Id,
            Nome = p.Nome,
            AreaInteresse = p.AreaInteresse,
            NivelHabilidade = Enum.TryParse<NivelHabilidade>(
                p.NivelHabilidade ?? "Junior",
                true,
                out var nivel
            ) ? nivel : NivelHabilidade.Junior,
            EstaEmpregado = p.EstaEmpregado
        });

        return Ok(ApiResponse<IEnumerable<ProfissionalDto>>.Ok(dtos));
    }

    // ==================================
    // GET /api/v1/Profissionais/{id}
    // ==================================
    [HttpGet("{id:int}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<ProfissionalDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ProfissionalDto>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<ProfissionalDto>>> GetById(int id)
    {
        var p = await _service.GetByIdAsync(id);
        if (p is null)
            return NotFound(ApiResponse<ProfissionalDto>.Falha("Profissional não encontrado."));

        var dto = new ProfissionalDto
        {
            Id = p.Id,
            Nome = p.Nome,
            AreaInteresse = p.AreaInteresse,
            NivelHabilidade = Enum.TryParse<NivelHabilidade>(
                p.NivelHabilidade ?? "Junior",
                true,
                out var nivel
            ) ? nivel : NivelHabilidade.Junior,
            EstaEmpregado = p.EstaEmpregado
        };

        return Ok(ApiResponse<ProfissionalDto>.Ok(dto));
    }

    // ==================================
    // POST /api/v1/Profissionais
    // (apenas ADMIN)
    // ==================================
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ApiResponse<ProfissionalDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<ProfissionalDto>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<ProfissionalDto>>> Create([FromBody] ProfissionalCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<ProfissionalDto>.Falha("Dados inválidos."));

        var entity = new Profissional
        {
            Nome = dto.Nome,
            AreaInteresse = dto.AreaInteresse,
            NivelHabilidade = dto.NivelHabilidade.ToString(),
            EstaEmpregado = dto.EstaEmpregado
        };

        var created = await _service.CreateAsync(entity);

        var result = new ProfissionalDto
        {
            Id = created.Id,
            Nome = created.Nome,
            AreaInteresse = created.AreaInteresse,
            NivelHabilidade = dto.NivelHabilidade,
            EstaEmpregado = created.EstaEmpregado
        };

        return CreatedAtAction(nameof(GetById),
            new { id = result.Id },
            ApiResponse<ProfissionalDto>.Ok(result));
    }

    // ==================================
    // PUT /api/v1/Profissionais/{id}
    // (apenas ADMIN)
    // ==================================
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<object>>> Update(int id, [FromBody] ProfissionalCreateDto dto)
    {
        var entity = new Profissional
        {
            Id = id,
            Nome = dto.Nome,
            AreaInteresse = dto.AreaInteresse,
            NivelHabilidade = dto.NivelHabilidade.ToString(),
            EstaEmpregado = dto.EstaEmpregado
        };

        var ok = await _service.UpdateAsync(id, entity); // 👈 aqui usa os 2 parâmetros

        if (!ok)
            return NotFound(ApiResponse<object>.Falha("Profissional não encontrado."));

        return StatusCode(StatusCodes.Status204NoContent);
    }

    // ==================================
    // DELETE /api/v1/Profissionais/{id}
    // (apenas ADMIN)
    // ==================================
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<object>>> Delete(int id)
    {
        var ok = await _service.DeleteAsync(id);
        if (!ok)
            return NotFound(ApiResponse<object>.Falha("Profissional não encontrado."));

        return StatusCode(StatusCodes.Status204NoContent);
    }
}
