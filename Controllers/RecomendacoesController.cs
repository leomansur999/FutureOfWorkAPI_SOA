using FutureOfWorkAPI.Models.Dtos;
using FutureOfWorkAPI.Models.Shared;
using FutureOfWorkAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FutureOfWorkAPI.Controllers;

[ApiController]
[Route("api/" + ApiVersions.V1 + "/[controller]")]
public class RecomendacoesController : ControllerBase
{
    private readonly IRecomendacaoService _service;

    public RecomendacoesController(IRecomendacaoService service)
    {
        _service = service;
    }

    /// <summary>
    /// Recomenda cursos para um profissional com base na sua área de interesse.
    /// </summary>
    [HttpGet("{profissionalId:int}")]
    [Authorize] // qualquer usuário autenticado pode pedir recomendação
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<CursoDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<CursoDto>>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<IEnumerable<CursoDto>>>> Recomendar(int profissionalId)
    {
        var cursos = await _service.RecomendarCursosParaProfissionalAsync(profissionalId);

        if (!cursos.Any())
        {
            return NotFound(ApiResponse<IEnumerable<CursoDto>>.Falha(
                "Nenhuma recomendação encontrada para esse profissional."));
        }

        return Ok(ApiResponse<IEnumerable<CursoDto>>.Ok(
            cursos, "Cursos recomendados com sucesso."));
    }
}
