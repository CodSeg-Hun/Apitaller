using ApiTaller.Clases;
using ApiTaller.DTO;
using ApiTaller.DTO.Resp;
using ApiTaller.Modelo;
using ApiTaller.Repositorio;
using ApiTaller.Repositorio.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.ServiceModel.Channels;
using System.Text.RegularExpressions;
using System.Threading.Tasks;



namespace ApiTaller.Controllers
{
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("Servicio")]
    public class ServicioController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<ServicioController> log;
        private readonly IServicio repositorio;
        protected RespuestAPI _respuestaApi;
        public ServicioController(IConfiguration configuration, ILogger<ServicioController> l, IServicio r)
        {
            this.configuration = configuration;
            this.log = l;
            this.repositorio = r;
            this._respuestaApi = new();
            //_roleManager = roleManager;
        }


        [HttpGet("/ConsultarDispositivoServicio")]
        //[Authorize]
        public ActionResult<ServicioResp> ConsultarDispositivoServicio(string usuario, string ordenServicio, string vehiculo, string opcion)
        {
            ServicioResp respuesta;
            bool bandera = true;
            string mensaje = "";
            if (usuario == null || string.IsNullOrEmpty(usuario.ToString()))
            {
                mensaje = "Valor de Usuario en blanco";
                bandera = false;
            }else if (ordenServicio == null || string.IsNullOrEmpty(ordenServicio.ToString()))
            {
                mensaje = "Valor de Orden Servicio en blanco";
                bandera = false;
            }else if (opcion == null || string.IsNullOrEmpty(opcion.ToString()))
            {
                mensaje = "Valor de Opcion en blanco";
                bandera = false;
            }
            if (bandera)
            {
                DataSet cnstGenrl = ServicioSQL.ConsultarDispositivo(ordenServicio, usuario, vehiculo, opcion);
                if (cnstGenrl.Tables.Count > 0)
                {
                    DataTable tabla = new DataTable();
                    tabla.Columns.Add("RESULTADO", typeof(string));
                    string jsonData = "";
                    if (cnstGenrl.Tables[0].Rows.Count > 0)
                    {
                        jsonData = (string)Utilidades.DataTableToJSON(cnstGenrl.Tables[0]);
                        jsonData = jsonData.ToUpper();
                    }else
                    {
                        _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                        _respuestaApi.IsSuccess = false;
                        _respuestaApi.Messages.Add("Error, No se existen datos, Verificar");
                        return BadRequest(_respuestaApi);
                    }
                   respuesta = JsonConvert.DeserializeObject<ServicioResp>("{\"respuesta\":" + jsonData + "}");
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


        [HttpGet("/ConsultarDatosControl")]
       // [Authorize]
        public ActionResult<ControlResp> ConsultarDatosControl(string usuario, string ordenServicio, string vehiculo, string empresa, string opcion)
        {
            ControlResp respuesta;
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
            }
            if (bandera)
            {
                DataSet cnstGenrl = ServicioSQL.ConsultandoDatosControl(ordenServicio, usuario, vehiculo, empresa, opcion);
                if (cnstGenrl.Tables.Count > 0)
                {
                    DataTable tabla = new DataTable();
                    tabla.Columns.Add("RESULTADO", typeof(string));
                    string jsonData = "";
                    if (cnstGenrl.Tables[0].Rows.Count > 0)
                    {
                        jsonData = (string)Utilidades.DataTableToJSON(cnstGenrl.Tables[0]);
                        jsonData = jsonData.ToUpper();
                        jsonData = jsonData.Replace("\"SEC\"", "\"COD\"");
                        jsonData = jsonData.Replace("\"OTR\"", "\"OTRA\"");
                        jsonData = jsonData.Replace("\"GPR\"", "\"GRU\"");
                        jsonData = jsonData.Replace("\"DESCR\"", "\"DES\"");
                    }else
                    {
                        _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                        _respuestaApi.IsSuccess = false;
                        _respuestaApi.Messages.Add("Error, No se existen datos, Verificar");
                        return BadRequest(_respuestaApi);
                    }
                    respuesta = JsonConvert.DeserializeObject<ControlResp>("{\"respuesta\":" + jsonData + "}");
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


        [HttpGet("/ConsultarCombo")]
        //[Authorize]
        public ActionResult<ComboResp> ConsultarDatosControl(string usuario, string opcion)
        {
            ComboResp respuesta;
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
            }
            if (bandera)
            {
                DataSet cnstGenrl = ServicioSQL.ConsultandoDatosControl("", usuario, "", "", opcion);
                if (cnstGenrl.Tables.Count > 0)
                {
                    DataTable tabla = new DataTable();
                    tabla.Columns.Add("RESULTADO", typeof(string));
                    string jsonData = "";
                    if (cnstGenrl.Tables[0].Rows.Count > 0)
                    {
                        jsonData = (string)Utilidades.DataTableToJSON(cnstGenrl.Tables[0]);
                        jsonData = jsonData.ToUpper();
                        jsonData = jsonData.Replace("\"COD\"", "\"CODIGO\"");
                        jsonData = jsonData.Replace("\"DES\"", "\"DESCRIPCION\"");
                    }else
                    {
                        _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                        _respuestaApi.IsSuccess = false;
                        _respuestaApi.Messages.Add("Error, No se existen datos, Verificar");
                        return BadRequest(_respuestaApi);
                    }
                    respuesta = JsonConvert.DeserializeObject<ComboResp>("{\"respuesta\":" + jsonData + "}");
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


        [HttpGet("/ConsultarFoto")]
        //[Authorize]
        public ActionResult<FotoResp> ConsultarFoto(string usuario, string ordenServicio, string vehiculo, string empresa, string opcion)
        {

            FotoResp respuesta;
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
            }
            if (bandera)
            {
                DataSet cnstGenrl = ServicioSQL.ConsultandoDatosControl(ordenServicio, usuario, vehiculo, empresa, opcion);
                if (cnstGenrl.Tables.Count > 0)
                {
                    DataTable tabla = new DataTable();
                    tabla.Columns.Add("RESULTADO", typeof(string));
                    string jsonData = "";
                    if (cnstGenrl.Tables[0].Rows.Count > 0)
                    {
                        jsonData = (string)Utilidades.DataTableToJSON(cnstGenrl.Tables[0]);
                        jsonData = jsonData.Replace("\"sEC\"", "\"SEC\"");
                        jsonData = jsonData.Replace("\"iMA\"", "\"IMA\"");
                        jsonData = jsonData.Replace("\"oRI\"", "\"ORI\"");
                    }else
                    {
                        _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                        _respuestaApi.IsSuccess = false;
                        _respuestaApi.Messages.Add("Error, No se existen datos, Verificar");
                        return BadRequest(_respuestaApi);
                    }
                    respuesta = JsonConvert.DeserializeObject<FotoResp>("{\"respuesta\":" + jsonData + "}");
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
