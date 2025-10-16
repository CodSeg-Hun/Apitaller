using Newtonsoft.Json;

namespace ApiTaller.DTO
{
    public class ActaEntregaDTO
    {

        [JsonProperty(PropertyName = "CD_SU")]
        public string SucursalId { get; set; } = "";

        [JsonProperty(PropertyName = "NO_SU")]
        public string Sucursal { get; set; } = "";

        [JsonProperty(PropertyName = "CD_CU")]
        public string CustodiaId { get; set; } = "";

        [JsonProperty(PropertyName = "CD_CL")]
        public string ClienteId { get; set; } = "";

        [JsonProperty(PropertyName = "NO_CO")]
        public string Cliente { get; set; } = "";

        [JsonProperty(PropertyName = "GR_PR")]
        public string GrupoCodigo { get; set; } = "";

        [JsonProperty(PropertyName = "NO_GP")]
        public string Grupo { get; set; } = "";

        [JsonProperty(PropertyName = "CD_VH")]
        public string VehiculoId { get; set; } = "";

        [JsonProperty(PropertyName = "GR_PE")]
        public string PermitidoEntrega { get; set; } = "";

        [JsonProperty(PropertyName = "ORG")]
        public string Origen { get; set; } = "";

        [JsonProperty(PropertyName = "AP_EN")]
        public string AprobadoEntrega { get; set; } = "";

        [JsonProperty(PropertyName = "BA_AP")]
        public string BajaAprobada { get; set; } = "";

        [JsonProperty(PropertyName = "CD_ES")]
        public string EstadoCodigo { get; set; } = "";

        [JsonProperty(PropertyName = "NO_ES")]
        public string Estado { get; set; } = "";

        [JsonProperty(PropertyName = "TI_CU")]
        public string TipoCodigo { get; set; } = "";

        [JsonProperty(PropertyName = "NO_TI")]
        public string TipoCustodia { get; set; } = "";

        [JsonProperty(PropertyName = "CD_PR")]
        public string ProductoCustodiaId { get; set; } = "";

        [JsonProperty(PropertyName = "NO_PR")]
        public string ProductoCustodia { get; set; } = "";

        [JsonProperty(PropertyName = "TI_SE")]
        public string TipoSerie { get; set; } = "";

        [JsonProperty(PropertyName = "NO_SE")]
        public string Serie { get; set; } = "";

        [JsonProperty(PropertyName = "SE_DP")]
        public string SerieDispositivo { get; set; } = "";

        [JsonProperty(PropertyName = "CD_DE")]
        public string CustodiaDetalle { get; set; } = "";





    }
}
