using ApiTaller.DTO.Resp;
using ApiTaller.Modelo;
using ApiTaller.Repositorio;
using ApiTaller.Repositorio.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Net;

namespace ApiTaller.Controllers
{
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("ActaEntrega")]
    public class ActaEntregaController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<ActaEntregaController> log;
        private readonly IActaEntrega repositorio;
        protected RespuestAPI _respuestaApi;
        public ActaEntregaController(IConfiguration configuration, ILogger<ActaEntregaController> l, IActaEntrega r)
        {
            this.configuration = configuration;
            this.log = l;
            this.repositorio = r;
            this._respuestaApi = new();
            //_roleManager = roleManager;
        }


        [HttpGet("/ConsultaActaEntrega")]
        //[Authorize]
        public ActionResult<ActaEntregaResp> ConsultaActaEntrega(string ordenServicio, string vehiculo, string idcliente, string usuario, string opcion)
        {
            ActaEntregaResp respuesta;
            bool bandera = true;
            string mensaje = "";
            if (usuario == null || string.IsNullOrEmpty(usuario.ToString()))
            {
                mensaje = "Valor de Usuario en blanco";
                bandera = false;
            }else if (opcion == null || string.IsNullOrEmpty(opcion.ToString()))
            {
                mensaje = "Valor de Opcion en blanco";
                bandera = false;
            }else if (ordenServicio == null || string.IsNullOrEmpty(ordenServicio.ToString()))
            {
                mensaje = "Valor de orden Servicio en blanco";
                bandera = false;
            }else if (vehiculo == null || string.IsNullOrEmpty(vehiculo.ToString()))
            {
                mensaje = "Valor de vehiculo en blanco";
                bandera = false;
            }else if (idcliente == null || string.IsNullOrEmpty(idcliente.ToString()))
            {
                mensaje = "Valor de Cliente en blanco";
                bandera = false;
            }
            if (bandera)
            {
                DataSet cnstGenrl = ActaEntregaSQL.ConsultarActaEntrega(ordenServicio, vehiculo, idcliente, usuario, opcion);
                if (cnstGenrl.Tables.Count > 0)
                {
                    DataTable tabla = new DataTable();
                    tabla.Columns.Add("RESULTADO", typeof(string));
                    string jsonData = "";
                    if (cnstGenrl.Tables[0].Rows.Count > 0)
                    {
                        jsonData = (string)Utilidades.DataTableToJSON(cnstGenrl.Tables[0]);
                    }
                    respuesta = JsonConvert.DeserializeObject<ActaEntregaResp>("{\"respuesta\":" + jsonData + "}");
                }else
                {
                    _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                    _respuestaApi.IsSuccess = false;
                    _respuestaApi.Messages.Add("Error, No se encuentra datos, Verificar");
                    return BadRequest(_respuestaApi);
                }
            }else
            {
                _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                _respuestaApi.IsSuccess = false;
                _respuestaApi.Messages.Add(mensaje);
                return BadRequest(_respuestaApi);
            }
            return respuesta;
        }




    }
}
