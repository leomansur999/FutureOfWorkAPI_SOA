using FutureOfWorkAPI.Models;
using FutureOfWorkAPI.Models.Dtos;
using FutureOfWorkAPI.Models.Shared;
using FutureOfWorkAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FutureOfWorkAPI.Controllers;

[ApiController]
[Route("api/" + ApiVersions.V1 + "/[controller]")]
public class CursosController : ControllerBase
{
    private readonly ICursoService _service;

    public CursosController(ICursoService service)
    {
        _service = service;
    }

    // GET público
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<CursoDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IEnumerable<CursoDto>>>> GetAll()
    {
        var list = await _service.GetAllAsync();
        var dtos = list.Select(c => new CursoDto
        {
            Id = c.Id,
            Titulo = c.Titulo,
            Categoria = c.Categoria,
            CargaHoraria = c.CargaHoraria
        });

        return Ok(ApiResponse<IEnumerable<CursoDto>>.Ok(dtos));
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<CursoDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<CursoDto>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<CursoDto>>> GetById(int id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item is null)
            return NotFound(ApiResponse<CursoDto>.Falha("Curso não encontrado."));

        var dto = new CursoDto
        {
            Id = item.Id,
            Titulo = item.Titulo,
            Categoria = item.Categoria,
            CargaHoraria = item.CargaHoraria
        };

        return Ok(ApiResponse<CursoDto>.Ok(dto));
    }

    // CRUD protegido – precisa de token com role Admin

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ApiResponse<CursoDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<CursoDto>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<CursoDto>>> Create([FromBody] CursoCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<CursoDto>.Falha("Dados inválidos."));

        var entity = new Curso
        {
            Titulo = dto.Titulo,
            Categoria = dto.Categoria,
            CargaHoraria = dto.CargaHoraria
        };

        var created = await _service.CreateAsync(entity);

        var result = new CursoDto
        {
            Id = created.Id,
            Titulo = created.Titulo,
            Categoria = created.Categoria,
            CargaHoraria = created.CargaHoraria
        };

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, ApiResponse<CursoDto>.Ok(result));
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<object>>> Update(int id, [FromBody] CursoCreateDto dto)
    {
        var entity = new Curso
        {
            Id = id,
            Titulo = dto.Titulo,
            Categoria = dto.Categoria,
            CargaHoraria = dto.CargaHoraria
        };

        var ok = await _service.UpdateAsync(id, entity);
        if (!ok)
            return NotFound(ApiResponse<object>.Falha("Curso não encontrado."));

        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<object>>> Delete(int id)
    {
        var ok = await _service.DeleteAsync(id);
        if (!ok)
            return NotFound(ApiResponse<object>.Falha("Curso não encontrado."));

        return StatusCode(StatusCodes.Status204NoContent);
    }
}
