using Newtonsoft.Json;

namespace ApiTaller.DTO
{
    public class ServiciosDTO
    {
        [JsonProperty(PropertyName = "SERVICIO_COD")]
        public string ServicioCodigo { get; set; }


        [JsonProperty(PropertyName = "SERVICIO_NOMBRE")]
        public string Servicio { get; set; }


        [JsonProperty(PropertyName = "SERVICIO_AMI")]
        public string ServicioAMI { get; set; }


        [JsonProperty(PropertyName = "GRUPO_PRODUCTO_COD")]
        public string ProductoCodigo { get; set; }


        [JsonProperty(PropertyName = "GRUPO_PRODUCTO_NOMBRE")]
        public string Producto { get; set; }


        public string Comentario { get; set; } = "";


        public string NumPuertas { get; set; } = "";

    }
}
