using Microsoft.AspNetCore.Mvc;
using MotoFacilAPI.Application.Dtos;
using MotoFacilAPI.Application.Interfaces;

namespace MotoFacilAPI.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicosController : ControllerBase
    {
        private readonly IServicoService _service;
        public ServicosController(IServicoService service) => _service = service;

        /// <summary>
        /// Lista todos os serviços (com paginação e HATEOAS)
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResultDto<ServicoDto>), 200)]
        public async Task<ActionResult<PagedResultDto<ServicoDto>>> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var all = await _service.ListAsync() ?? new List<ServicoDto>();
            var total = all.Count;
            var paged = all.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            foreach (var servico in paged)
            {
                servico.Links.Add(new LinkDto { Rel = "self", Href = Url.Action(nameof(GetById), new { id = servico.Id }) ?? string.Empty, Method = "GET" });
                servico.Links.Add(new LinkDto { Rel = "update", Href = Url.Action(nameof(Put), new { id = servico.Id }) ?? string.Empty, Method = "PUT" });
                servico.Links.Add(new LinkDto { Rel = "delete", Href = Url.Action(nameof(Delete), new { id = servico.Id }) ?? string.Empty, Method = "DELETE" });
            }

            return Ok(new PagedResultDto<ServicoDto>
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = total,
                Items = paged
            });
        }

        /// <summary>
        /// Busca serviço por ID
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ServicoDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ServicoDto>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result is null) return NotFound();

            result.Links.Add(new LinkDto { Rel = "self", Href = Url.Action(nameof(GetById), new { id = result.Id }) ?? string.Empty, Method = "GET" });
            result.Links.Add(new LinkDto { Rel = "update", Href = Url.Action(nameof(Put), new { id = result.Id }) ?? string.Empty, Method = "PUT" });
            result.Links.Add(new LinkDto { Rel = "delete", Href = Url.Action(nameof(Delete), new { id = result.Id }) ?? string.Empty, Method = "DELETE" });

            return Ok(result);
        }

        /// <summary>
        /// Cria novo serviço
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ServicoDto), 201)]
        public async Task<ActionResult<ServicoDto>> Post([FromBody] ServicoDto dto)
        {
            if (!ModelState.IsValid || dto is null)
                return BadRequest(ModelState);

            var created = await _service.CreateAsync(dto);

            created.Links.Add(new LinkDto { Rel = "self", Href = Url.Action(nameof(GetById), new { id = created.Id }) ?? string.Empty, Method = "GET" });
            created.Links.Add(new LinkDto { Rel = "update", Href = Url.Action(nameof(Put), new { id = created.Id }) ?? string.Empty, Method = "PUT" });
            created.Links.Add(new LinkDto { Rel = "delete", Href = Url.Action(nameof(Delete), new { id = created.Id }) ?? string.Empty, Method = "DELETE" });

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Atualiza serviço
        /// </summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Put(int id, [FromBody] ServicoDto dto)
        {
            if (!ModelState.IsValid || dto is null)
                return BadRequest(ModelState);

            var ok = await _service.UpdateAsync(id, dto);
            return ok ? NoContent() : NotFound();
        }

        /// <summary>
        /// Remove serviço
        /// </summary>
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