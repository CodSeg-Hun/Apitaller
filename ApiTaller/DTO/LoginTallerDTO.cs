using Newtonsoft.Json;

namespace ApiTaller.DTO
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class LoginTallerDTO
    {

        [JsonProperty(PropertyName = "CODIGO")]
        public string  Codigo { get; set; }

        [JsonProperty(PropertyName = "NOMBRE")]
        public string Nombre { get; set; }

        [JsonProperty(PropertyName = "USUARIOID")]
        public string UsuarioId { get; set; }

        [JsonProperty(PropertyName = "MENSAJE")]
        public string Mensaje { get; set; }

        [JsonProperty(PropertyName = "SUPER")]
        public string Super { get; set; }

        [JsonProperty(PropertyName = "TALLER")]
        public string Taller { get; set; }

        [JsonProperty(PropertyName = "CONVENIO")]
        public string Convenio { get; set; }
    }
}
