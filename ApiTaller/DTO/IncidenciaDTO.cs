using Newtonsoft.Json;

namespace ApiTaller.DTO
{
    public class IncidenciaDTO
    {

        [JsonProperty(PropertyName = "SEC")]
        public string secuencia { get; set; }


        [JsonProperty(PropertyName = "COM")]
        public string comentario { get; set; }


        [JsonProperty(PropertyName = "FEC")]
        public string fecha { get; set; }

        [JsonProperty(PropertyName = "USU")]
        public string usuario { get; set; }


        [JsonProperty(PropertyName = "INC")]
        public string incidencia { get; set; }


        [JsonProperty(PropertyName = "EMAIL")]
        public string correo { get; set; }
    }
}
