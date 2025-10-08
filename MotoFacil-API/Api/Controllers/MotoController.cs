using Microsoft.AspNetCore.Mvc;
using MotoFacilAPI.Application.Dtos;
using MotoFacilAPI.Application.Interfaces;

namespace MotoFacilAPI.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotosController : ControllerBase
    {
        private readonly IMotoService _service;
        public MotosController(IMotoService service) => _service = service;

        /// <summary>
        /// Lista todas as motos cadastradas (com paginação e HATEOAS)
        /// </summary>
        /// <param name="page">Número da página</param>
        /// <param name="pageSize">Tamanho da página</param>
        /// <response code="200">Retorna a lista paginada de motos</response>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResultDto<MotoDto>), 200)]
        public async Task<ActionResult<PagedResultDto<MotoDto>>> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var all = await _service.ListAsync() ?? new List<MotoDto>();
            var total = all.Count;
            var paged = all.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            foreach (var moto in paged)
            {
                moto.Links.Add(new LinkDto { Rel = "self", Href = Url.Action(nameof(GetById), new { id = moto.Id }) ?? string.Empty, Method = "GET" });
                moto.Links.Add(new LinkDto { Rel = "update", Href = Url.Action(nameof(Put), new { id = moto.Id }) ?? string.Empty, Method = "PUT" });
                moto.Links.Add(new LinkDto { Rel = "delete", Href = Url.Action(nameof(Delete), new { id = moto.Id }) ?? string.Empty, Method = "DELETE" });
            }

            return Ok(new PagedResultDto<MotoDto>
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = total,
                Items = paged
            });
        }

        /// <summary>
        /// Busca moto por ID
        /// </summary>
        /// <param name="id">ID da moto</param>
        /// <response code="200">Retorna a moto encontrada</response>
        /// <response code="404">Moto não encontrada</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(MotoDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<MotoDto>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            result.Links.Add(new LinkDto { Rel = "self", Href = Url.Action(nameof(GetById), new { id = result.Id }) ?? string.Empty, Method = "GET" });
            result.Links.Add(new LinkDto { Rel = "update", Href = Url.Action(nameof(Put), new { id = result.Id }) ?? string.Empty, Method = "PUT" });
            result.Links.Add(new LinkDto { Rel = "delete", Href = Url.Action(nameof(Delete), new { id = result.Id }) ?? string.Empty, Method = "DELETE" });
            return Ok(result);
        }

        /// <summary>
        /// Cria nova moto
        /// </summary>
        /// <param name="dto">Dados da moto</param>
        /// <response code="201">Moto criada com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost]
        [ProducesResponseType(typeof(MotoDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<MotoDto>> Post([FromBody] MotoDto dto)
        {
            if (dto == null) return BadRequest("Dados obrigatórios não enviados.");
            var created = await _service.CreateAsync(dto);
            created.Links.Add(new LinkDto { Rel = "self", Href = Url.Action(nameof(GetById), new { id = created.Id }) ?? string.Empty, Method = "GET" });
            created.Links.Add(new LinkDto { Rel = "update", Href = Url.Action(nameof(Put), new { id = created.Id }) ?? string.Empty, Method = "PUT" });
            created.Links.Add(new LinkDto { Rel = "delete", Href = Url.Action(nameof(Delete), new { id = created.Id }) ?? string.Empty, Method = "DELETE" });
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Atualiza uma moto
        /// </summary>
        /// <param name="id">ID da moto</param>
        /// <param name="dto">Dados atualizados</param>
        /// <response code="204">Moto atualizada com sucesso</response>
        /// <response code="404">Moto não encontrada</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Put(int id, [FromBody] MotoDto dto)
        {
            if (dto == null) return BadRequest("Dados obrigatórios não enviados.");
            var ok = await _service.UpdateAsync(id, dto);
            return ok ? NoContent() : NotFound();
        }

        /// <summary>
        /// Remove uma moto
        /// </summary>
        /// <param name="id">ID da moto</param>
        /// <response code="204">Moto removida com sucesso</response>
        /// <response code="404">Moto não encontrada</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            return ok ? NoContent() : NotFound();
        }
    }
}