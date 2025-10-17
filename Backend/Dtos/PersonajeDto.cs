using System.Text.Json.Serialization;

namespace Backend.Dtos
{
    public class PersonajeDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("species")]
        public string Species { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }
    }
}