using Newtonsoft.Json;

namespace ApiTaller.DTO
{
    public class DispositivoDTO
    {
        public string codigo { get; set; } = "";

        public string descripcion { get; set; } = "";

        [JsonProperty(PropertyName = "NUMBER_BOX")]
        public string NumberBox { get; set; } = "";

        public string ACTIVACION { get; set; } = "";

        public string RESPUESTA { get; set; } = "";

        [JsonProperty(PropertyName = "ORDEN_TRABAJO")]
        public string OrdenTrabajo { get; set; } = "";

        public string TIPO { get; set; } = "";

        public string UBICACION { get; set; } = "";

        public string SERIE { get; set; } = "";

        public string VIP { get; set; } = "";

        public string IP { get; set; } = "";

        public string SIM { get; set; } = "";

        [JsonProperty(PropertyName = "NUMERO_CELULAR_SIM")]
        public string NumeroCelularSIM { get; set; } = "";

        public string UNIDAD { get; set; } = "";

        [JsonProperty(PropertyName = "MODELO_DISPOSITIVO")]
        public string ModeloDispositivo { get; set; } = "";

        [JsonProperty(PropertyName = "SERV")]
        public string Servidor { get; set; } = "";

        [JsonProperty(PropertyName = "FIRM")]
        public string Firmware { get; set; } = "";

        public string SCRIPT { get; set; } = "";

        public string APN { get; set; } = "";

        public string celular { get; set; } = "";

        [JsonProperty(PropertyName = "OPERAD")]
        public string operador { get; set; } = "";

    }
}
