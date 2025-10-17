using Backend.Controllers;
using Backend.Dtos;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Backend
{
    public class ServicioAPI
    {
        private readonly string _baseUrl;
        private readonly ILogger<ServicioAPI> _logger;

        public ServicioAPI(IOptions<ApiSettings> options, ILogger<ServicioAPI> logger)
        {
            _baseUrl = options.Value.BaseUrl;
            _logger = logger;
        }

        public async Task<EpisodioResponseDto?> ObtenerEpisodiosPaginadosAsync(int page)
        {
            string url = $"{_baseUrl}/episode?page={page}";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    var json = await response.Content.ReadAsStringAsync();

                    return JsonSerializer.Deserialize<EpisodioResponseDto>(json);
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error al obtener episodios.");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado.");
                return null;
            }
        }

        public async Task<EpisodioDto?> ObtenerEpisodioAsync(int id)
        {
            string url = $"{_baseUrl}/episode/{id}";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    var json = await response.Content.ReadAsStringAsync();

                    return JsonSerializer.Deserialize<EpisodioDto>(json);
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error al obtener el episodio con ID {Id}.", id);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener episodio con ID {Id}.", id);
                return null;
            }
        }

        public async Task<List<PersonajeDto>> ObtenerPersonajesAsync(List<int> ids)
        {
            var personajes = new List<PersonajeDto>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    foreach (var id in ids)
                    {
                        string url = $"{_baseUrl}/character/{id}";
                        var response = await client.GetAsync(url);

                        if (response.IsSuccessStatusCode)
                        {
                            var json = await response.Content.ReadAsStringAsync();
                            var personaje = JsonSerializer.Deserialize<PersonajeDto>(json);

                            if (personaje != null)
                            {
                                personajes.Add(personaje);
                            }
                        }
                        else
                        {
                            _logger.LogWarning("No se pudo obtener el personaje con ID {Id}.", id);
                        }
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error al obtener personajes.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener personajes.");
            }

            return personajes;
        }
    }
}
