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
using System;
using System.Data;
using System.Net;

namespace ApiTaller.Controllers
{
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("Trabajo")]
    public class TrabajoController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<TrabajoController> log;
        private readonly ITrabajo repositorio;
        protected RespuestAPI _respuestaApi;

        public TrabajoController(IConfiguration configuration, ILogger<TrabajoController> l, ITrabajo r)
        {
            this.configuration = configuration;
            this.log = l;
            this.repositorio = r;
            this._respuestaApi = new();
            //_roleManager = roleManager;
        }


        [HttpGet("/ConsultasGenerales")]
        //[Authorize]
        public ActionResult<ComboResp> ConsultasGenerales(string usuario, string opcion)
        {
            ComboResp respuesta;
            bool bandera = true;
            string mensaje = "";
            if (opcion == null || string.IsNullOrEmpty(opcion.ToString()))
            {
                mensaje = "Valor de Opcion en blanco";
                bandera = false;
            }            
            if (bandera)
            {
                DataSet cnstGenrl = TrabajoSQL.ConsultarGenerales("0", "0", "", "",usuario, opcion);
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


        [HttpGet("/ConsultarDispositivo")]
        //[Authorize]
        public ActionResult<DispositivoResp> ConsultarDispositivo(string orden, string dispositivo, string tipoDispositivo, string usuario, string opcion)
        {

            DispositivoResp respuesta;
            bool bandera = true;
            string mensaje = "";
            if (opcion == null || string.IsNullOrEmpty(opcion.ToString()))
            {
                mensaje = "Valor de Opcion en blanco";
                bandera = false;
            }
            if (opcion == "9")
            {
                dispositivo = orden;
                tipoDispositivo = "";
            }else
            {
                if (dispositivo == null || string.IsNullOrEmpty(dispositivo.ToString()))
                {
                    mensaje = "Valor de dispositivo en blanco";
                    bandera = false;
                }
                if (tipoDispositivo == null || string.IsNullOrEmpty(tipoDispositivo.ToString()))
                {
                    mensaje = "Valor de tipoDispositivo en blanco";
                    bandera = false;
                }
            }
            if (bandera)
            {
                DataSet cnstGenrl = TrabajoSQL.ConsultarGenerales(orden, "0", dispositivo, tipoDispositivo, usuario, opcion);
                if (cnstGenrl.Tables.Count > 0)
                {
                    DataTable tabla = new DataTable();
                    tabla.Columns.Add("RESULTADO", typeof(string));
                    string jsonData = "";
                    if (cnstGenrl.Tables[0].Rows.Count > 0)
                    {
                        jsonData = (string)Utilidades.DataTableToJSON(cnstGenrl.Tables[0]);
                        jsonData = jsonData.ToUpper();
                        jsonData = jsonData.Replace("\"RESP\"", "\"RESPUESTA\"");
                    }else
                    {
                        _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                        _respuestaApi.IsSuccess = false;
                        _respuestaApi.Messages.Add("Error, No se existen datos, Verificar");
                        return BadRequest(_respuestaApi);
                    }
                    respuesta = JsonConvert.DeserializeObject<DispositivoResp>("{\"respuesta\":" + jsonData + "}");
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


        [HttpGet("/ConsultarIncidencias")]
        //[Authorize]
        public ActionResult<IncidenciaResp> ConsultarIncidencias(string orden, string opcion)
        {
            IncidenciaResp respuesta;
            bool bandera = true;
            string mensaje = "";
            if (opcion == null || string.IsNullOrEmpty(opcion.ToString()))
            {
                mensaje = "Valor de Opcion en blanco";
                bandera = false;
            }else if (orden == null || string.IsNullOrEmpty(orden.ToString()))
            {
                mensaje = "Valor de orden en blanco";
                bandera = false;
            }
            if (bandera)
            {
                DataSet cnstGenrl = TrabajoSQL.ConsultarGenerales(orden, "0", "", "", "0", opcion);
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
                    respuesta = JsonConvert.DeserializeObject<IncidenciaResp>("{\"respuesta\":" + jsonData + "}");
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


        [HttpGet("/ConsultarServiciosDisp")]
        //[Authorize]
        public ActionResult<ServiciosResp> ConsultarServiciosDisp(string orden, string opcion)
        {
            ServiciosResp respuesta;
            bool bandera = true;
            string mensaje = "";
            if (opcion == null || string.IsNullOrEmpty(opcion.ToString()))
            {
                mensaje = "Valor de Opcion en blanco";
                bandera = false;
            }else if (orden == null || string.IsNullOrEmpty(orden.ToString()))
            {
                mensaje = "Valor de orden en blanco";
                bandera = false;
            }
            if (bandera)
            {
                DataSet cnstGenrl = TrabajoSQL.ConsultaServicios(orden, opcion);
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
                    respuesta = JsonConvert.DeserializeObject<ServiciosResp>("{\"respuesta\":" + jsonData + "}");
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


        [HttpGet("/ConsultarOrdenServicio")]
        //[Authorize]
        public ActionResult<OrdenServicioResp> ConsultarOrdenServicio(string orden, string opcion)
        {
            OrdenServicioResp respuesta;
            bool bandera = true;
            string mensaje = "";
            if (opcion == null || string.IsNullOrEmpty(opcion.ToString()))
            {
                mensaje = "Valor de Opcion en blanco";
                bandera = false;
            }else if (orden == null || string.IsNullOrEmpty(orden.ToString()))
            {
                mensaje = "Valor de orden en blanco";
                bandera = false;
            }
            if (bandera)
            {
                DataSet cnstGenrl = TrabajoSQL.ConsultarGenerales(orden,"0", "", "", "0", opcion);
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
                    respuesta = JsonConvert.DeserializeObject<OrdenServicioResp>("{\"respuesta\":" + jsonData + "}");
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


        [HttpGet("/ConsultarOrdenTrabajo")]
        //[Authorize]
        public ActionResult<OrdenTrabajoResp> ConsultarOrdenTrabajo(string orden, string tecnico, string opcion)
        {
            OrdenTrabajoResp respuesta;
            bool bandera = true;
            string mensaje = "";
            if (opcion == null || string.IsNullOrEmpty(opcion.ToString()))
            {
                mensaje = "Valor de Opcion en blanco";
                bandera = false;
            }else if (orden == null || string.IsNullOrEmpty(orden.ToString()))
            {
                mensaje = "Valor de orden en blanco";
                bandera = false;
            }else if (tecnico == null || string.IsNullOrEmpty(tecnico.ToString()))
            {
                mensaje = "Valor de tecnico en blanco";
                bandera = false;
            }
            if (bandera)
            {
                DataSet cnstGenrl = TrabajoSQL.ConsultarGenerales("0", orden, "", "", tecnico, opcion);
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
                    respuesta = JsonConvert.DeserializeObject<OrdenTrabajoResp>("{\"respuesta\":" + jsonData + "}");
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
