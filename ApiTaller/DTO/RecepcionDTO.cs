using Newtonsoft.Json;

namespace ApiTaller.DTO
{
    public class RecepcionDTO
    {

        [JsonProperty(PropertyName = "CHA")]
        public string Chasis { get; set; } = "";


        [JsonProperty(PropertyName = "MOT")]
        public string Motor { get; set; } = "";


        [JsonProperty(PropertyName = "DAT")]
        public string Vehiculo { get; set; } = "";


        [JsonProperty(PropertyName = "VEH")]
        public string VehiculoId { get; set; } = "";


        [JsonProperty(PropertyName = "ORD")]
        public string OrdenServicio { get; set; } = "";


        [JsonProperty(PropertyName = "CAT")]
        public string Categoria { get; set; } = "";


        [JsonProperty(PropertyName = "TRA")]
        public string producto { get; set; } = "";


        [JsonProperty(PropertyName = "CODID")]
        public string ClienteId { get; set; } = "";


        //[JsonProperty(PropertyName = "CLI")]
        //public string Cliente { get; set; } = "";


        [JsonProperty(PropertyName = "CLIE")]
        public string ClienteNombre { get; set; } = "";


        [JsonProperty(PropertyName = "EMP")]
        public string Empresa { get; set; } = "";


        [JsonProperty(PropertyName = "SUC")]
        public string Sucursal { get; set; } = "";


        [JsonProperty(PropertyName = "OBS")]
        public string Observacion { get; set; } = "";


        [JsonProperty(PropertyName = "D2")]
        public string Direccion { get; set; } = "";


        [JsonProperty(PropertyName = "D3")]
        public string Email { get; set; } = "";


        [JsonProperty(PropertyName = "D4")]
        public string Telefono { get; set; } = "";


        [JsonProperty(PropertyName = "D5")]
        public string Taller { get; set; } = "";


        [JsonProperty(PropertyName = "RECE")]
        public string RecepcionId { get; set; } = "";


        [JsonProperty(PropertyName = "ENTR")]
        public string Entrega { get; set; } = "";


        [JsonProperty(PropertyName = "RECI")]
        public string Recibe { get; set; } = "";


        [JsonProperty(PropertyName = "APRO")]
        public string Aprobado { get; set; } = "";


        [JsonProperty(PropertyName = "TURID")]
        public string TurnoId { get; set; } = "";


        [JsonProperty(PropertyName = "TIPO")]
        public string Tipo { get; set; } = "";


        [JsonProperty(PropertyName = "CODTP")]
        public string Codtp { get; set; } = "";


        [JsonProperty(PropertyName = "COD")]
        public string Codigo { get; set; } = "";


        [JsonProperty(PropertyName = "CANT")]
        public string Cantidad { get; set; } = "";


        [JsonProperty(PropertyName = "PLACA")]
        public string Placa { get; set; } = "";


        [JsonProperty(PropertyName = "AUDITORIA")]
        public string Auditoria { get; set; } = "";


        [JsonProperty(PropertyName = "RUTA_FILE")]
        public string Ruta { get; set; } = "";


        [JsonProperty(PropertyName = "TECNICO")]
        public string Tecnico { get; set; } = "";
    }

}
