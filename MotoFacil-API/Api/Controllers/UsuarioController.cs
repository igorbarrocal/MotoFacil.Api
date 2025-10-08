using Microsoft.AspNetCore.Mvc;
using MotoFacilAPI.Application.Dtos;
using MotoFacilAPI.Application.Interfaces;

namespace MotoFacilAPI.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _service;
        public UsuariosController(IUsuarioService service) => _service = service;

        /// <summary>
        /// Lista todos os usuários (com paginação e HATEOAS)
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResultDto<UsuarioDto>), 200)]
        public async Task<ActionResult<PagedResultDto<UsuarioDto>>> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var all = await _service.ListAsync() ?? new List<UsuarioDto>();
            var total = all.Count;
            var paged = all.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            foreach (var usuario in paged)
            {
                usuario.Links.Add(new LinkDto { Rel = "self", Href = Url.Action(nameof(GetById), new { id = usuario.Id }) ?? string.Empty, Method = "GET" });
                usuario.Links.Add(new LinkDto { Rel = "update", Href = Url.Action(nameof(Put), new { id = usuario.Id }) ?? string.Empty, Method = "PUT" });
                usuario.Links.Add(new LinkDto { Rel = "delete", Href = Url.Action(nameof(Delete), new { id = usuario.Id }) ?? string.Empty, Method = "DELETE" });
            }

            return Ok(new PagedResultDto<UsuarioDto>
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = total,
                Items = paged
            });
        }

        /// <summary>
        /// Busca usuário por ID
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(UsuarioDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UsuarioDto>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result is null) return NotFound();

            result.Links.Add(new LinkDto { Rel = "self", Href = Url.Action(nameof(GetById), new { id = result.Id }) ?? string.Empty, Method = "GET" });
            result.Links.Add(new LinkDto { Rel = "update", Href = Url.Action(nameof(Put), new { id = result.Id }) ?? string.Empty, Method = "PUT" });
            result.Links.Add(new LinkDto { Rel = "delete", Href = Url.Action(nameof(Delete), new { id = result.Id }) ?? string.Empty, Method = "DELETE" });

            return Ok(result);
        }

        /// <summary>
        /// Cria novo usuário
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(UsuarioDto), 201)]
        public async Task<ActionResult<UsuarioDto>> Post([FromBody] UsuarioDto dto)
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
        /// Atualiza usuário
        /// </summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Put(int id, [FromBody] UsuarioDto dto)
        {
            if (!ModelState.IsValid || dto is null)
                return BadRequest(ModelState);

            var ok = await _service.UpdateAsync(id, dto);
            return ok ? NoContent() : NotFound();
        }

        /// <summary>
        /// Remove usuário
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