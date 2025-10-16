using Newtonsoft.Json;

namespace ApiTaller.DTO
{
    public class ReasignacionDTO
    {

        [JsonProperty(PropertyName = "VEH")]
        public string VehiculoId { get; set; } = "";


        [JsonProperty(PropertyName = "TRA_ID")]
        public string OrdenTrabajo { get; set; } = "";


        [JsonProperty(PropertyName = "ORD_ID")]
        public string OrdenServicio { get; set; } = "";


        [JsonProperty(PropertyName = "CLI_ID")]
        public string ClienteId { get; set; } = "";


        [JsonProperty(PropertyName = "CLI_NOM")]
        public string ClienteNombre { get; set; } = "";


        [JsonProperty(PropertyName = "ATUR_ID")]
        public string TurnoId { get; set; } = "";


        public string Taller { get; set; } = "";

        public string Origen { get; set; } = "";


        [JsonProperty(PropertyName = "TIPO_TRABAJO_ID")]
        public string TrabajoId { get; set; } = "";


        [JsonProperty(PropertyName = "TRAB")]
        public string Trabajo { get; set; } = "";


        [JsonProperty(PropertyName = "TEC1")]
        public string Tecnico1 { get; set; } = "";


        [JsonProperty(PropertyName = "FEC1")]
        public string Fecha1 { get; set; } = "";


        [JsonProperty(PropertyName = "MOT1")]
        public string Motivo1 { get; set; } = "";


        [JsonProperty(PropertyName = "USU1")]
        public string Usuario1 { get; set; } = "";



        [JsonProperty(PropertyName = "TEC2")]
        public string Tecnico2 { get; set; } = "";


        [JsonProperty(PropertyName = "FEC2")]
        public string Fecha2 { get; set; } = "";


        [JsonProperty(PropertyName = "MOT2")]
        public string Motivo2 { get; set; } = "";


        [JsonProperty(PropertyName = "USU2")]
        public string Usuario2 { get; set; } = "";



        [JsonProperty(PropertyName = "TEC3")]
        public string Tecnico3 { get; set; } = "";


        [JsonProperty(PropertyName = "FEC3")]
        public string Fecha3 { get; set; } = "";


        [JsonProperty(PropertyName = "MOT3")]
        public string Motivo3 { get; set; } = "";


        [JsonProperty(PropertyName = "USU3")]
        public string Usuario3 { get; set; } = "";



        [JsonProperty(PropertyName = "TEC4")]
        public string Tecnico4 { get; set; } = "";


        [JsonProperty(PropertyName = "FEC4")]
        public string Fecha4 { get; set; } = "";


        [JsonProperty(PropertyName = "MOT4")]
        public string Motivo4 { get; set; } = "";


        [JsonProperty(PropertyName = "USU4")]
        public string Usuario4 { get; set; } = "";



        [JsonProperty(PropertyName = "EST_ID")]
        public string EstadoId { get; set; } = "";

        public string Estado { get; set; } = "";


        [JsonProperty(PropertyName = "IMG")]
        public string Imagen { get; set; } = "";



        //[JsonProperty(PropertyName = "CANT")]
        public string Cant { get; set; } = "";

    }
}
