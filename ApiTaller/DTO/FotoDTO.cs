using Newtonsoft.Json;

namespace ApiTaller.DTO
{
    public class FotoDTO
    {
        [JsonProperty(PropertyName = "SEC")]
        public string codigo { get; set; } = "";

        [JsonProperty(PropertyName = "IMA")]
        public string imagen { get; set; } = "";

        [JsonProperty(PropertyName = "ORI")]
        public string origen { get; set; } = "";
    }
}
