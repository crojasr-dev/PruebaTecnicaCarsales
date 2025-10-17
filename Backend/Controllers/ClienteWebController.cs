using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Backend.Dtos;
using Microsoft.Extensions.Logging;

namespace Backend.Controllers
{
   
    [ApiController]
    [Route("[controller]")]
    public class ClienteWebController : ControllerBase
    {
        private readonly Negocio _negocio;
        private readonly ILogger<ClienteWebController> _logger;

        public ClienteWebController(Negocio negocio, ILogger<ClienteWebController> logger)
        {
            _negocio = negocio;
            _logger = logger;
        }

        [HttpGet("ObtenerEpisodios")]
        public async Task<ActionResult<EpisodioResponseDto>> ObtenerEpisodios([FromQuery] int page = 1)
        {
            try
            {
                var result = await _negocio.ObtenerEpisodiosPaginadosAsync(page);
                _logger.LogInformation("Se obtuvo la página {Page} de episodios.", page);
                if (result == null)
                {
                    return StatusCode(502, "Error al obtener los episodios desde la API externa.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener episodios.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        
        [HttpGet("ObtenerEpisodio")]
        public async Task<ActionResult<EpisodioDto>> ObtenerEpisodio([FromQuery] int id)
        {
            try
            {
                var result = await _negocio.ObtenerEpisodioAsync(id);
                _logger.LogInformation("Se obtuvo el episodio con ID {Id}.", id);
                
                if (result == null)
                {
                    return NotFound($"No se encontró el episodio con ID {id}.");
                }
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el episodio con ID {Id}.", id);
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

       
        [HttpGet("ObtenerPersonajes")]
        public async Task<ActionResult<List<PersonajeDto>>> ObtenerPersonajes([FromQuery] string ids)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ids))
                {
                    return BadRequest("Debe proporcionar al menos un ID de personaje.");
                }

                var idsList = ids.Split(',')
                                 .Select(id => int.TryParse(id.Trim(), out var parsedId) ? parsedId : 0)
                                 .Where(id => id > 0)
                                 .ToList();

                if (idsList.Count == 0)
                {
                    return BadRequest("No se proporcionaron IDs válidos.");
                }

                var result = await _negocio.ObtenerPersonajesAsync(idsList);
                _logger.LogInformation("Se obtuvieron {Count} personajes.", result.Count);

                if (result == null || result.Count == 0)
                {
                    return StatusCode(502, "Error al obtener los personajes desde la API externa.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener personajes.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }
    }
}
