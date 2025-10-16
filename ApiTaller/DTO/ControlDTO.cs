using Newtonsoft.Json;

namespace ApiTaller.DTO
{
    public class ControlDTO
    {
        [JsonProperty(PropertyName = "COD")]
        public string codigo { get; set; }

        [JsonProperty(PropertyName = "DES")]
        public string descripcion { get; set; }

        [JsonProperty(PropertyName = "CAL")]
        public string calificacion { get; set; } = "";

        [JsonProperty(PropertyName = "TPA")]
        public string tipoParametro { get; set; } = "";

        [JsonProperty(PropertyName = "ITEM")]
        public string item { get; set; } = "";

        [JsonProperty(PropertyName = "TRA")]
        public string transaccion { get; set; } = "";

        [JsonProperty(PropertyName = "OTRA")]
        public string OrdenTrabajo { get; set; } = "";


        [JsonProperty(PropertyName = "CHE")]
        public string fechaChequeo { get; set; } = "";


        [JsonProperty(PropertyName = "SUC")]
        public string sucursalId { get; set; } = "";


        [JsonProperty(PropertyName = "PAR")]
        public string parametro { get; set; } = "";

        [JsonProperty(PropertyName = "GRU")]
        public string grupo { get; set; } = "";


        [JsonProperty(PropertyName = "EST")]
        public string estadoId { get; set; } = "";

        [JsonProperty(PropertyName = "ACC")]
        public string accion { get; set; } = "";

        [JsonProperty(PropertyName = "UCH")]
        public string usuarioChequeo { get; set; } = "";


        [JsonProperty(PropertyName = "INS")]
        public string instalador { get; set; } = "";

    }
}
