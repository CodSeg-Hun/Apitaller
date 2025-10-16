using Newtonsoft.Json;

namespace ApiTaller.DTO
{
    public class OrdenTrabajoDTO
    {

        [JsonProperty(PropertyName = "NUMERO_GENERAL")]
        public string ordenTrabajo { get; set; }


        [JsonProperty(PropertyName = "CODIGO_CLIENTE")]
        public string ClienteId { get; set; }


        [JsonProperty(PropertyName = "CODIGO_VEHICULO")]
        public string vehiculoId { get; set; }


        public string estado { get; set; }


        public string realizado { get; set; }


        [JsonProperty(PropertyName = "FECHA_CHEQUEO")]
        public string FechaChequeo { get; set; }


        [JsonProperty(PropertyName = "NUMERO_ORDEN_SERVICIO")]
        public string OrdenServicio { get; set; }


        [JsonProperty(PropertyName = "HORA_EJECUTA_TRABAJO")]
        public string HoraTrabajo { get; set; }


        [JsonProperty(PropertyName = "CON_NOVEDAD")]
        public string Novedad { get; set; }


        [JsonProperty(PropertyName = "UBICACION_VEHICULO")]
        public string UbicacionVehiculo { get; set; }


        public string observacion { get; set; }


        public string item { get; set; }


        public string ubicacion { get; set; }


        public string comentarios { get; set; }


        [JsonProperty(PropertyName = "ESTADO_DETALLE")]
        public string EstadoId { get; set; }


        [JsonProperty(PropertyName = "TIPO_TRANSACCION")]
        public string TipoTransaccion { get; set; }


        public string producto { get; set; }


        public string dispositivo { get; set; }


        [JsonProperty(PropertyName = "TIPO_DISPOSITIVO")]
        public string TipoDispositivo { get; set; }


        [JsonProperty(PropertyName = "DISP")]
        public string ObtieneDispositivo { get; set; }


        [JsonProperty(PropertyName = "TUR_ID")]
        public string TurnoId { get; set; }


        [JsonProperty(PropertyName = "ACT_ENTR")]
        public string actaEntrega { get; set; }


        public string accion { get; set; }

    }
}
