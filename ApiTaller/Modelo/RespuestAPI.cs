using System.Collections.Generic;
using System.Net;

namespace ApiTaller.Modelo
{
    public class RespuestAPI
    {

        public RespuestAPI()
        {
            Messages = new List<string>();
        }

        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> Messages { get; set; }
        public object Result { get; set; }
    }
}
