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
    [Route("Chequeo")]
    public class ChequeoController : Controller
    {


        private readonly IConfiguration configuration;
        private readonly ILogger<ChequeoController> log;
        private readonly IChequeo repositorio;
        protected RespuestAPI _respuestaApi;
        public ChequeoController(IConfiguration configuration, ILogger<ChequeoController> l, IChequeo r)
        {
            this.configuration = configuration;
            this.log = l;
            this.repositorio = r;
            this._respuestaApi = new();
            //_roleManager = roleManager;
        }


        [HttpGet("/ConsultatecnicoAsignar")]
        //[Authorize]
        public ActionResult<ComboResp> ConsultatecnicoAsignar(string opcion, string usuario)
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
                DataSet cnstGenrl = ChequeoSQL.ConsultarTecnicoAsignar(opcion, usuario);
                if (cnstGenrl.Tables.Count > 0)
                {
                    DataTable tabla = new DataTable();
                    tabla.Columns.Add("RESULTADO", typeof(string));
                    string jsonData = "";
                    if (cnstGenrl.Tables[0].Rows.Count > 0)
                    {
                        jsonData = (string) Utilidades.DataTableToJSON(cnstGenrl.Tables[0]);
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


        [HttpGet("/ConsultaData")]
        //[Authorize]
        public ActionResult<ConsultaDatosResp> ConsultaData(string fechaini, string fechafin, string tecnico, string ordenServicio, string usuario, string opcion)
        {
            ConsultaDatosResp respuesta = null;
            bool bandera = true;
            string mensaje = "";
            if (ordenServicio == null)
                ordenServicio = "0";

            if (fechaini == null || string.IsNullOrEmpty(fechaini.ToString()))
            {
                mensaje = "Valor de fecha Inicio en blanco";
                bandera = false;
            }else if (fechafin == null || string.IsNullOrEmpty(fechafin.ToString()))
            {
                mensaje = "Valor de fecha Fin en blanco";
                bandera = false;
            }else if (usuario == null || string.IsNullOrEmpty(usuario.ToString()))
            {
                mensaje = "Valor de Usuario en blanco";
                bandera = false;
            }else if (tecnico == null || string.IsNullOrEmpty(tecnico.ToString()))
            {
                mensaje = "Valor de Tecnico en blanco";
                bandera = false;
            }else if (opcion == null || string.IsNullOrEmpty(opcion.ToString()))
            {
                mensaje = "Valor de Opcion en blanco";
                bandera = false;
            }
            if (bandera)
            {
                DataSet cnstGenrl = ChequeoSQL.ConsultarDatos(fechaini, fechafin, tecnico,"0", ordenServicio, usuario,"", "", opcion);
                if (cnstGenrl.Tables.Count > 0)
                {
                    DataTable tabla = new DataTable();
                    tabla.Columns.Add("RESULTADO", typeof(string));
                    string jsonData = "";
                    if (cnstGenrl.Tables[0].Rows.Count > 0)
                    {
                        jsonData = (string)Utilidades.DataTableToJSON(cnstGenrl.Tables[0]);
                    }else
                    {
                        _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                        _respuestaApi.IsSuccess = false;
                        _respuestaApi.Messages.Add("Error, No se existen datos, Verificar");
                        return BadRequest(_respuestaApi);
                    }
                    respuesta = JsonConvert.DeserializeObject<ConsultaDatosResp>("{\"respuesta\":" + jsonData + "}");
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
            return  respuesta;
        }

  
        [HttpGet("/ConsultaSupervisor")]
        //[Authorize]
        public ActionResult<ConsultaDatosResp> ConsultaSupervisor(string fechaini, string fechafin, string tecnico, string taller, string ordenServicio, string usuario, string opcion)
        {
            ConsultaDatosResp respuesta = null;
            bool bandera = true;
            string mensaje = "";
            ordenServicio ??= "0";
            if (fechaini == null || string.IsNullOrEmpty(fechaini.ToString()))
            {
                mensaje = "Valor de Fecha Inicio en blanco";
                bandera = false;
            }else if (fechafin == null || string.IsNullOrEmpty(fechafin.ToString()))
            {
                mensaje = "Valor de Fecha Fin en blanco";
                bandera = false;
            }else if (usuario == null || string.IsNullOrEmpty(usuario.ToString()))
            {
                mensaje = "Valor de Usuario en blanco";
                bandera = false;
            }else if (opcion == null || string.IsNullOrEmpty(opcion.ToString()))
            {
                mensaje = "Valor de Opcion en blanco";
                bandera = false;
            }else if (taller == null || string.IsNullOrEmpty(taller.ToString()))
            {
                mensaje = "Valor de Taller en blanco";
                bandera = false;
            }
            if (bandera)
            {
                DataSet cnstGenrl = ChequeoSQL.ConsultarDatos(fechaini, fechafin, tecnico,taller, ordenServicio, usuario,"", "", opcion);
                if (cnstGenrl.Tables.Count > 0)
                {
                    DataTable tabla = new DataTable();
                    tabla.Columns.Add("RESULTADO", typeof(string));
                    string jsonData = "";
                    if (cnstGenrl.Tables[0].Rows.Count > 0)
                    {
                        jsonData = (string)Utilidades.DataTableToJSON(cnstGenrl.Tables[0]);
                        jsonData = jsonData.ToUpper();
                        jsonData =  jsonData.Replace("\"SERVICIO_ID\"", "\"ORDEN_SERVICIO\"");
                        jsonData = jsonData.Replace("\"VEHICULO\"", "\"VEHICULO_NOMBRE\"");
                        jsonData = jsonData.Replace("\"CLIENTE\"", "\"CLIENTE_NOMBRE\"");
                        jsonData = jsonData.Replace("\"TRABAJO_ID\"", "\"ORDEN_TRABAJO\"");
                        jsonData = jsonData.Replace("\"TECNICO\"", "\"TECNICO_ID\"");
                    }else
                    {
                        _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                        _respuestaApi.IsSuccess = false;
                        _respuestaApi.Messages.Add("Error, No se existen datos, Verificar");
                        return BadRequest(_respuestaApi);
                    }
                    respuesta = JsonConvert.DeserializeObject<ConsultaDatosResp>("{\"respuesta\":" + jsonData + "}");
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


        [HttpGet("/ConsultarConvenio")]
        //[Authorize]
        public ActionResult<ConsultaDatosResp> ConsultarConvenio(string fechaini, string fechafin,  string ordenServicio, string usuario,string convenio, string opcion)
        {
            ConsultaDatosResp respuesta = null;
            bool bandera = true;
            string mensaje = "";
            ordenServicio ??= "0";
            if (fechaini == null || string.IsNullOrEmpty(fechaini.ToString()))
            {
                mensaje = "Valor de Fecha Inicio en blanco";
                bandera = false;
            }else if (fechafin == null || string.IsNullOrEmpty(fechafin.ToString()))
            {
                mensaje = "Valor de Fecha Fin en blanco";
                bandera = false;
            }else if (usuario == null || string.IsNullOrEmpty(usuario.ToString()))
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
                DataSet cnstGenrl = ChequeoSQL.ConsultarDatos(fechaini, fechafin, "0", "0", ordenServicio, usuario, convenio, "", opcion);
                if (cnstGenrl.Tables.Count > 0)
                {
                    DataTable tabla = new DataTable();
                    tabla.Columns.Add("RESULTADO", typeof(string));
                    string jsonData = "";
                    if (cnstGenrl.Tables[0].Rows.Count > 0)
                    {
                        jsonData = (string)Utilidades.DataTableToJSON(cnstGenrl.Tables[0]);
                        jsonData = jsonData.ToUpper();
                        jsonData = jsonData.Replace("\"SERVICIO_ID\"", "\"ORDEN_SERVICIO\"");
                        jsonData = jsonData.Replace("\"TRABAJO_ID\"", "\"ORDEN_TRABAJO\"");
                        jsonData = jsonData.Replace("\"CLIENTE\"", "\"CLIENTE_NOMBRE\"");
                        jsonData = jsonData.Replace("\"VEHICULO\"", "\"VEHICULO_NOMBRE\"");
                        jsonData = jsonData.Replace("\"TECNICO\"", "\"TECNICO_ID\"");
                    }else
                    {
                        _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                        _respuestaApi.IsSuccess = false;
                        _respuestaApi.Messages.Add("Error, No se existen datos, Verificar");
                        return BadRequest(_respuestaApi);
                    }
                    respuesta = JsonConvert.DeserializeObject<ConsultaDatosResp>("{\"respuesta\":" + jsonData + "}");
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


        [HttpGet("/ConsultaImagen")]
       // [Authorize]
        public ActionResult<FotoResp> ConsultaImagen(string ordenServicio, string usuario, string origen, string opcion)
        {
            FotoResp respuesta = null;
            bool bandera = true;
            string mensaje = "";
            if (ordenServicio == null || string.IsNullOrEmpty(ordenServicio.ToString()))
            {
                mensaje = "Valor de Orden Servicio en blanco";
                bandera = false;
            }
            else if (origen == null || string.IsNullOrEmpty(origen.ToString()))
            {
                mensaje = "Valor de origen en blanco";
                bandera = false;
            }
            else if (usuario == null || string.IsNullOrEmpty(usuario.ToString()))
            {
                mensaje = "Valor de Usuario en blanco";
                bandera = false;
            }
            else if (opcion == null || string.IsNullOrEmpty(opcion.ToString()))
            {
                mensaje = "Valor de Opcion en blanco";
                bandera = false;
            }
            if (bandera)
            {
                DataSet cnstGenrl = ChequeoSQL.ConsultarDatos("", "", "0", "0", ordenServicio, usuario, "", origen, opcion);
                if (cnstGenrl.Tables.Count > 0)
                {
                    DataTable tabla = new DataTable();
                    tabla.Columns.Add("RESULTADO", typeof(string));
                    string jsonData = "";
                    if (cnstGenrl.Tables[0].Rows.Count > 0)
                    {
                        jsonData = (string)Utilidades.DataTableToJSON(cnstGenrl.Tables[0]);
                        jsonData = jsonData.Replace("\"sECUENCIAL\"", "\"SEC\"");
                        jsonData = jsonData.Replace("\"sTRIMAGEN\"", "\"IMA\"");
                    }
                    else
                    {
                        _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                        _respuestaApi.IsSuccess = false;
                        _respuestaApi.Messages.Add("Error, No se existen datos, Verificar");
                        return BadRequest(_respuestaApi);
                    }
                    respuesta = JsonConvert.DeserializeObject<FotoResp>("{\"respuesta\":" + jsonData + "}");
                }
                else
                {
                    _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                    _respuestaApi.IsSuccess = false;
                    _respuestaApi.Messages.Add("Error, No se encuentra datos, Verificar");
                    return BadRequest(_respuestaApi);
                }
            }
            else
            {
                _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                _respuestaApi.IsSuccess = false;
                _respuestaApi.Messages.Add(mensaje);
                return BadRequest(_respuestaApi);
            }
            return respuesta;
        }


        [HttpGet("/ConsultaReasignacion")]
        //[Authorize]
        public ActionResult<ReasignacionResp> ConsultaReasignacion(string fechaini, string fechafin, string ordenServicio, string vehiculo, string taller, string tecnico, string usuario, string opcion)
        {
            ReasignacionResp respuesta = null;
            bool bandera = true;
            string mensaje = ""; 
            if (ordenServicio == null)
                ordenServicio = "0";
            if (taller == null)
                taller = "0";
            if (tecnico == null)
                tecnico = "0";
            if (vehiculo == null)
                vehiculo = "0";
            if (fechaini == null || string.IsNullOrEmpty(fechaini.ToString()))
            {
                mensaje = "Valor de Fecha Inicio en blanco";
                bandera = false;
            }else if (fechafin == null || string.IsNullOrEmpty(fechafin.ToString()))
            {
                mensaje = "Valor de Fecha Fin en blanco";
                bandera = false;
            }else if (usuario == null || string.IsNullOrEmpty(usuario.ToString()))
            {
                mensaje = "Valor de Usuario en blanco";
                bandera = false;
            }else if (opcion == null || string.IsNullOrEmpty(opcion.ToString()))
            {
                mensaje = "Valor de Opcion en blanco";
                bandera = false;
            }else if (tecnico == null || string.IsNullOrEmpty(tecnico.ToString()))
            {
                mensaje = "Valor de Tecnico en blanco";
                bandera = false;
            }else if (taller == null || string.IsNullOrEmpty(taller.ToString()))
            {
                mensaje = "Valor de Taller en blanco";
                bandera = false;
            }else if (ordenServicio == null || string.IsNullOrEmpty(ordenServicio.ToString()))
            {
                mensaje = "Valor de Orden Servicio en blanco";
                bandera = false;
            }else if (vehiculo == null || string.IsNullOrEmpty(vehiculo.ToString()))
            {
                mensaje = "Valor de Vehiculo en blanco";
                bandera = false;
            }
            if (bandera)
            {
                DataSet cnstGenrl = ChequeoSQL.ConsultarDatosReasignacion(fechaini, fechafin, ordenServicio, vehiculo, taller, tecnico, usuario,  opcion);
                if (cnstGenrl.Tables.Count > 0)
                {
                    DataTable tabla = new DataTable();
                    tabla.Columns.Add("RESULTADO", typeof(string));
                    string jsonData = "";
                    if (cnstGenrl.Tables[0].Rows.Count > 0)
                    {
                        jsonData = (string)Utilidades.DataTableToJSON(cnstGenrl.Tables[0]);
                        jsonData = jsonData.ToUpper();
                        jsonData = jsonData.Replace("\"D1\"", "\"ESTADO\"");
                        jsonData = jsonData.Replace("\"E1\"", "\"EST_ID\"");
                        jsonData = jsonData.Replace("\"C1\"", "\"CANT\"");
                    }else
                    {
                        _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                        _respuestaApi.IsSuccess = false;
                        _respuestaApi.Messages.Add("Error, No se existen datos, Verificar");
                        return BadRequest(_respuestaApi);
                    }
                    respuesta = JsonConvert.DeserializeObject<ReasignacionResp>("{\"respuesta\":" + jsonData + "}");
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





        //[HttpPost("/GuardartecnicoAsignadoSysHunter")]
        //[Authorize]
        //public ActionResult<TurnoResp> GuardartecnicoAsignadoSysHunter(string usuario, string ordenServicio, string orderTrabajo, string vehiculo, string cliente, string grupo,
        //                                                               string taller, string atencionturno, string tecnico, string tallerFuera, string tallerCiudad, string opcion)
        //{

        //    TurnoResp respuesta = null;
        //    Boolean bandera = true;

        //    //if (fechaini == null)
        //    //    fechaini = "";
        //    //if (fechafin == null)
        //    //    fechafin = "";
        //    //if (usuario == null)
        //    //    usuario = "";
        //    ////if (tecnico == null)
        //    ////    tecnico = "";
        //    //if (opcion == null)
        //    //    opcion = "";
        //    //if (ordenServicio == null)
        //    //    ordenServicio = "0";
        //    //if (taller == null)
        //    //    taller = "";

        //    //if (fechaini == "" || fechaini.ToUpper() == "NULL")
        //    //{
        //    //    bandera = false;
        //    //}
        //    //else if (fechafin == "" || fechafin.ToUpper() == "NULL")
        //    //{
        //    //    bandera = false;
        //    //}
        //    //else if (usuario == "" || usuario.ToUpper() == "NULL")
        //    //{
        //    //    bandera = false;
        //    //}
        //    ////else if (tecnico == "" || tecnico.ToUpper() == "NULL")
        //    ////{
        //    ////    bandera = false;
        //    ////}
        //    //else if (opcion == "" || opcion.ToUpper() == "NULL")
        //    //{
        //    //    bandera = false;
        //    //}
        //    //else if (taller == "" || taller.ToUpper() == "NULL")
        //    //{
        //    //    bandera = false;
        //    //}


        //    if (bandera)
        //    {
        //        //DataSet cnstGenrl = new DataSet();
        //        //cnstGenrl = ChequeoSQL.ConsultarDatos(fechaini, fechafin, "0", "0", ordenServicio, usuario, convenio, opcion);
        //        //if (cnstGenrl.Tables.Count > 0)
        //        //{
        //        //    //DataTable tabla = new DataTable();
        //        //    //tabla.Columns.Add("RESULTADO", typeof(string));
        //        //    //string jsonData = "";
        //        //    //if (cnstGenrl.Tables[0].Rows.Count > 0)
        //        //    //{
        //        //    //    jsonData = (string)Utilidades.DataTableToJSON(cnstGenrl.Tables[0]);
        //        //    //    //jsonData = new ConvertJson().SerializarJson(cnstGenrl.Tables[0]);
        //        //    //    jsonData = jsonData.ToUpper();
        //        //    //    jsonData = jsonData.Replace("\"SERVICIO_ID\"", "ORDEN_SERVICIO");
        //        //    //    jsonData = jsonData.Replace("\"TRABAJO_ID\"", "ORDEN_TRABAJO");
        //        //    //    jsonData = jsonData.Replace("\"CLIENTE\"", "CLIENTE_NOMBRE");
        //        //    //    jsonData = jsonData.Replace("\"VEHICULO\"", "VEHICULO_NOMBRE");
        //        //    //    jsonData = jsonData.Replace("\"TECNICO\"", "TECNICO_ID");



        //        //    //}
        //        //    //else
        //        //    //{
        //        //    //    _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
        //        //    //    _respuestaApi.IsSuccess = false;
        //        //    //    _respuestaApi.Messages.Add("Error, No se existen datos, Verificar");
        //        //    //    return BadRequest(_respuestaApi);
        //        //    //}

        //        //    //// respuesta =  JsonConvert.DeserializeObject<List<ConsultaDatos>>(jsonData);
        //        //    ////respuesta = ("{\"results\":" + jsonData + "}");
        //        //    //respuesta = JsonConvert.DeserializeObject<ConsultaDatosResp>("{\"respuesta\":" + jsonData + "}");
        //        //}
        //        //else
        //        //{
        //        //    _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
        //        //    _respuestaApi.IsSuccess = false;
        //        //    _respuestaApi.Messages.Add("Error, No se encuentra datos, Verificar");
        //        //    return BadRequest(_respuestaApi);
        //        //}
        //    }
        //    else
        //    {
        //        _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
        //        _respuestaApi.IsSuccess = false;
        //        _respuestaApi.Messages.Add("Error, Datos enviados en Blanco");
        //        return BadRequest(_respuestaApi);
        //    }

        //    return null;
        //}



        //[HttpPost("/GuardartecnicoConvenioSysHunter")]
        //[Authorize]
        //public ActionResult<TurnoResp> GuardartecnicoConvenioSysHunter(string usuario, string ordenServicio, string orderTrabajo, string vehiculo, string cliente, string grupo,
        //                                                               string taller, string atencionturno, string tecnico, string tallerFuera, string tallerCiudad, string opcion)
        //{

        //    TurnoResp respuesta = null;
        //    Boolean bandera = true;

        //    //if (fechaini == null)
        //    //    fechaini = "";
        //    //if (fechafin == null)
        //    //    fechafin = "";
        //    //if (usuario == null)
        //    //    usuario = "";
        //    ////if (tecnico == null)
        //    ////    tecnico = "";
        //    //if (opcion == null)
        //    //    opcion = "";
        //    //if (ordenServicio == null)
        //    //    ordenServicio = "0";
        //    //if (taller == null)
        //    //    taller = "";

        //    //if (fechaini == "" || fechaini.ToUpper() == "NULL")
        //    //{
        //    //    bandera = false;
        //    //}
        //    //else if (fechafin == "" || fechafin.ToUpper() == "NULL")
        //    //{
        //    //    bandera = false;
        //    //}
        //    //else if (usuario == "" || usuario.ToUpper() == "NULL")
        //    //{
        //    //    bandera = false;
        //    //}
        //    ////else if (tecnico == "" || tecnico.ToUpper() == "NULL")
        //    ////{
        //    ////    bandera = false;
        //    ////}
        //    //else if (opcion == "" || opcion.ToUpper() == "NULL")
        //    //{
        //    //    bandera = false;
        //    //}
        //    //else if (taller == "" || taller.ToUpper() == "NULL")
        //    //{
        //    //    bandera = false;
        //    //}


        //    if (bandera)
        //    {
        //        //DataSet cnstGenrl = new DataSet();
        //        //cnstGenrl = ChequeoSQL.ConsultarDatos(fechaini, fechafin, "0", "0", ordenServicio, usuario, convenio, opcion);
        //        //if (cnstGenrl.Tables.Count > 0)
        //        //{
        //        //    //DataTable tabla = new DataTable();
        //        //    //tabla.Columns.Add("RESULTADO", typeof(string));
        //        //    //string jsonData = "";
        //        //    //if (cnstGenrl.Tables[0].Rows.Count > 0)
        //        //    //{
        //        //    //    jsonData = (string)Utilidades.DataTableToJSON(cnstGenrl.Tables[0]);
        //        //    //    //jsonData = new ConvertJson().SerializarJson(cnstGenrl.Tables[0]);
        //        //    //    jsonData = jsonData.ToUpper();
        //        //    //    jsonData = jsonData.Replace("\"SERVICIO_ID\"", "ORDEN_SERVICIO");
        //        //    //    jsonData = jsonData.Replace("\"TRABAJO_ID\"", "ORDEN_TRABAJO");
        //        //    //    jsonData = jsonData.Replace("\"CLIENTE\"", "CLIENTE_NOMBRE");
        //        //    //    jsonData = jsonData.Replace("\"VEHICULO\"", "VEHICULO_NOMBRE");
        //        //    //    jsonData = jsonData.Replace("\"TECNICO\"", "TECNICO_ID");



        //        //    //}
        //        //    //else
        //        //    //{
        //        //    //    _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
        //        //    //    _respuestaApi.IsSuccess = false;
        //        //    //    _respuestaApi.Messages.Add("Error, No se existen datos, Verificar");
        //        //    //    return BadRequest(_respuestaApi);
        //        //    //}

        //        //    //// respuesta =  JsonConvert.DeserializeObject<List<ConsultaDatos>>(jsonData);
        //        //    ////respuesta = ("{\"results\":" + jsonData + "}");
        //        //    //respuesta = JsonConvert.DeserializeObject<ConsultaDatosResp>("{\"respuesta\":" + jsonData + "}");
        //        //}
        //        //else
        //        //{
        //        //    _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
        //        //    _respuestaApi.IsSuccess = false;
        //        //    _respuestaApi.Messages.Add("Error, No se encuentra datos, Verificar");
        //        //    return BadRequest(_respuestaApi);
        //        //}
        //    }
        //    else
        //    {
        //        _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
        //        _respuestaApi.IsSuccess = false;
        //        _respuestaApi.Messages.Add("Error, Datos enviados en Blanco");
        //        return BadRequest(_respuestaApi);
        //    }

        //    return null;
        //}


    }
}
