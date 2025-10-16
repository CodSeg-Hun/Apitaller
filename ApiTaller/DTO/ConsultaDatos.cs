using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace ApiTaller.DTO
{
    public class ConsultaDatos
    {
        [JsonProperty(PropertyName = "VEHICULO_ID")]
        public string VehiculoId  { get; set; }

        [JsonProperty(PropertyName = "ORDEN_SERVICIO")]
        public string OrdenServicio { get; set; } = "";

        //[JsonProperty(PropertyName = "SERVICIO_ID")]
        //public string ServicioId { get; set; } = "";

        [JsonProperty(PropertyName = "CLIENTE_ID")]
        public string ClienteId { get; set; }

        [JsonProperty(PropertyName="CLIENTE_NOMBRE")]
        //[JsonProperty(PropertyName = "name1", PropertyName = "name2")]
        public string clienteNombre { get; set; } = "";

        //[JsonProperty(PropertyName = "CLIENTE")]
        //public string cliente { get; set; } = "";


        [JsonProperty(PropertyName = "TIPO_TRABAJO")]
        public string TipoTrabajo { get; set; }

        [JsonProperty(PropertyName = "VEHICULO_NOMBRE")]
        public string VehiculoNombre { get; set; } = "";

        //[JsonProperty(PropertyName = "VEHICULO")]
        //public string Vehiculo { get; set; } = "";

        [JsonProperty(PropertyName = "TURNO_ID")]
        public string TurnoId { get; set; }

        [JsonProperty(PropertyName = "FECHA")]
        public string Fecha  { get; set; }

        [JsonProperty(PropertyName = "TALLER")]
        public string Taller { get; set; }

        [JsonProperty(PropertyName = "TALLER_ID")]
        public string TallerId { get; set; } = "";

        [JsonProperty(PropertyName = "MOTIVO")]
        public string Motivo { get; set; }

        [JsonProperty(PropertyName = "HORA")]
        public string Hora { get; set; }

        [JsonProperty(PropertyName = "COMENTARIO")]
        public string Comentario { get; set; }

        [JsonProperty(PropertyName = "CATEGORIA")]
        public string Categoria { get; set; }

        [JsonProperty(PropertyName = "CONCESIONARIO")]
        public string Concesionario { get; set; }

        [JsonProperty(PropertyName = "ORDEN_TRABAJO")]
        public string OrdenTrabajo { get; set; } = "";


        //[JsonProperty(PropertyName = "TRABAJO_ID")]
        //public string TrabajoId { get; set; } = "";


        [JsonProperty(PropertyName = "TECNICO_ID")]
        public string TecnicoId { get; set; }

        [JsonProperty(PropertyName = "RECEP")]
        public string Recepcion { get; set; } = "";

        //[JsonProperty(PropertyName = "TECNICO")]
        //public string Tecnico { get; set; } = "";

        [JsonProperty(PropertyName = "GRUPO")]
        public string Grupo { get; set; } = "";


        [JsonProperty(PropertyName = "FUE_TAL")]
        public string FueTal { get; set; } = "";


        [JsonProperty(PropertyName = "FUE_CIU")]
        public string FueCiu { get; set; } = "";

        [JsonProperty(PropertyName = "OBTEC")]
        public string ObTec { get; set; } = "";
    }
}
