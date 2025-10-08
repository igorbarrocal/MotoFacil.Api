using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoFacilAPI.Application.Dtos;
using MotoFacilAPI.Application.Services;

namespace MotoFacilAPI.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v{version:apiVersion}/ml")]
    [ApiVersion("1.0")]
    public class PredictionController : ControllerBase
    {
        private readonly PredictionService _service = new();

        /// <summary>
        /// Prediz se uma moto precisa de manutenção usando ML.NET (dummy)
        /// </summary>
        /// <param name="input">Dados de entrada</param>
        /// <returns>Se precisa manutenção (bool) e Score (confiança)</returns>
        [HttpPost("precisamanutencao")]
        [ProducesResponseType(typeof(PredictionResponseDto), 200)]
        public ActionResult<PredictionResponseDto> Predict([FromBody] PredictionRequestDto input)
        {
            var result = _service.Predict(input);
            return Ok(result);
        }
    }
}