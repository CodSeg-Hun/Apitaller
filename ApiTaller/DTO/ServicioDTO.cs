using Newtonsoft.Json;

namespace ApiTaller.DTO
{
    public class ServicioDTO
    {
        [JsonProperty(PropertyName = "CLI")]
        public string ClienteId { get; set; }


        [JsonProperty(PropertyName = "VEH")]
        public string VehiculoId { get; set; }


        [JsonProperty(PropertyName = "PRO")]
        public string Producto { get; set; }


        [JsonProperty(PropertyName = "TTRA")]
        public string TipoTransaccion { get; set; }


        [JsonProperty(PropertyName = "TRA")]
        public string Transaccion { get; set; }


        [JsonProperty(PropertyName = "SER")]
        public string Serie { get; set; }


        [JsonProperty(PropertyName = "MOL")]
        public string ModeloId { get; set; }


        [JsonProperty(PropertyName = "MAR")]
        public string MarcaId { get; set; }


        [JsonProperty(PropertyName = "VID")]
        public string Vid { get; set; }


        [JsonProperty(PropertyName = "NCLI")]
        public string Cliente { get; set; }


        [JsonProperty(PropertyName = "NVEH")]
        public string Vehiculo { get; set; }


        [JsonProperty(PropertyName = "AMI")]
        public string Ami { get; set; }



    }
}
