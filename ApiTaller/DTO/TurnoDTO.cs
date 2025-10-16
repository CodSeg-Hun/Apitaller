using Newtonsoft.Json;

namespace ApiTaller.DTO
{
    public class TurnoDTO
    {
        [JsonProperty(PropertyName = "CODIGO")]
        public string Codigo { get; set; }

        [JsonProperty(PropertyName = "MENSAJE")]
        public string Mensaje { get; set; }

        [JsonProperty(PropertyName = "HORA")]
        public string Hora { get; set; }

        [JsonProperty(PropertyName = "TURNO")]
        public string Turno { get; set; }
    }
}
