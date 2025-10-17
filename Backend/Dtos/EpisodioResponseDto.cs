using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Dtos
{
    public class EpisodioResponseDto
    {
        [JsonPropertyName("info")]
        public InfoDto Info { get; set; }

        [JsonPropertyName("results")]
        public List<EpisodioDto> Results { get; set; }
    }

    public class InfoDto
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("pages")]
        public int Pages { get; set; }

        [JsonPropertyName("next")]
        public string Next { get; set; }

        [JsonPropertyName("prev")]
        public string Prev { get; set; }
    }

    public class EpisodioDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("air_date")]
        public string AirDate { get; set; }

        [JsonPropertyName("episode")]
        public string Episode { get; set; }

        [JsonPropertyName("characters")]
        public List<string> Characters { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("created")]
        public string Created { get; set; }
    }
}