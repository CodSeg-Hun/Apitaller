using Newtonsoft.Json;

namespace ApiTaller.DTO
{
  
    public class AccesorioDTO
    {
        [JsonProperty(PropertyName = "A1")]
        public string codigo { get; set; }

        [JsonProperty(PropertyName = "A2")]
        public string descripcion { get; set; }

        [JsonProperty(PropertyName = "A3")]
        public string codigo2 { get; set; }

        [JsonProperty(PropertyName = "A4")]
        public string descripcion2 { get; set; }
    }
}
