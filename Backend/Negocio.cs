using System.Threading.Tasks;
using Backend.Dtos;

namespace Backend
{
    public class Negocio
    {
        private readonly ServicioAPI _servicioApi;

        public Negocio(ServicioAPI servicioApi)
        {
            _servicioApi = servicioApi;
        }

        public async Task<EpisodioResponseDto> ObtenerEpisodiosPaginadosAsync(int page)
        {
            var result = await _servicioApi.ObtenerEpisodiosPaginadosAsync(page);
            if (result == null)
            {
                throw new Exception("No se pudieron obtener los episodios.");
            }
            return result;
        }

        public async Task<EpisodioDto?> ObtenerEpisodioAsync(int id)
        {
            return await _servicioApi.ObtenerEpisodioAsync(id);
        }

        public async Task<List<PersonajeDto>> ObtenerPersonajesAsync(List<int> ids)
        {
            return await _servicioApi.ObtenerPersonajesAsync(ids);
        }
    }
}
