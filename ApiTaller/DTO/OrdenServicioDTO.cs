using Newtonsoft.Json;

namespace ApiTaller.DTO
{
    public class OrdenServicioDTO
    {

        public string orden { get; set; }

        [JsonProperty(PropertyName = "COMENTARIO_ORDEN")]
        public string comentario { get; set; }

        [JsonProperty(PropertyName = "UBICACION_TURNO")]
        public string ubicacion { get; set; }

        [JsonProperty(PropertyName = "CONTACTO_TURNO")]
        public string contacto { get; set; }

        [JsonProperty(PropertyName = "ORIGEN_TURNO")]
        public string origen { get; set; }

        [JsonProperty(PropertyName = "TRABAJO_FUERA_TALLER")]
        public string trabajoFuera { get; set; }

        [JsonProperty(PropertyName = "CODIGO_VEHICULO")]
        public string vehiculoId { get; set; }

        public string motor { get; set; }

        public string placa { get; set; }

        public string chasis { get; set; }

        public string marca { get; set; }

        public string modelo { get; set; }

        public string color { get; set; }

        public string anio { get; set; }

        public string cbu { get; set; }

        public string cliente { get; set; }

        [JsonProperty(PropertyName = "CEDULA")]
        public string clienteId { get; set; }

        public string direccion { get; set; }

        public string telefono { get; set; }

        [JsonProperty(PropertyName = "SWITCH_1")]
        public string switch1 { get; set; }

        [JsonProperty(PropertyName = "SWITCH_2")]
        public string switch2 { get; set; }

        [JsonProperty(PropertyName = "PUNTO_VENTA_ID")]
        public string PuntoVentaId { get; set; }

        [JsonProperty(PropertyName = "TURNO_PROGRAMADO")]
        public string TurnoProgramado { get; set; }

        public string atendido { get; set; }

        public string strimagen { get; set; }



    }
}
