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
using System.ServiceModel.Channels;

namespace ApiTaller.Controllers
{
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("Recepcion")]
    public class RecepcionController : Controller
    {

        private readonly IConfiguration configuration;
        private readonly ILogger<RecepcionController> log;
        private readonly IRecepcion repositorio;
        protected RespuestAPI _respuestaApi;
        public RecepcionController(IConfiguration configuration, ILogger<RecepcionController> l, IRecepcion r)
        {
            this.configuration = configuration;
            this.log = l;
            this.repositorio = r;
            this._respuestaApi = new();
            //_roleManager = roleManager;
        }


        [HttpGet("/ConsultarRecepcionGeneral")]
        //[Authorize]
        public ActionResult<RecepcionResp> ConsultarRecepcionGeneral(string fechaini, string fechafin, string usuario, string opcion)
        {

            RecepcionResp respuesta;
            bool bandera = true;
            string mensaje = "";
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
                DataSet cnstGenrl = RecepcionSQL.ConsultarDatosRecepcionGeneral(fechaini, fechafin,  usuario, opcion);
                if (cnstGenrl.Tables.Count > 0)
                {
                    DataTable tabla = new DataTable();
                    tabla.Columns.Add("RESULTADO", typeof(string));
                    string jsonData = "";
                    if (cnstGenrl.Tables[0].Rows.Count > 0)
                    {
                        jsonData = (string)Utilidades.DataTableToJSON(cnstGenrl.Tables[0]);
                        jsonData = jsonData.ToUpper();
                        jsonData = jsonData.Replace("\"ORDE\"", "\"ORD\"");
                        jsonData = jsonData.Replace("\"VEHI\"", "\"DAT\"");
                        jsonData = jsonData.Replace("\"TALL\"", "\"D5\"");
                        jsonData = jsonData.Replace("\"CATE\"", "\"CAT\"");
                        jsonData = jsonData.Replace("\"TRAB\"", "\"TRA\"");
                        jsonData = jsonData.Replace("\"CLI\"", "\"CODID\"");
                        jsonData = jsonData.Replace("\"TIP\"", "\"TIPO\"");
                        jsonData = jsonData.Replace("\"ORI\"", "\"COD\"");
                    }else
                    {
                        _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                        _respuestaApi.IsSuccess = false;
                        _respuestaApi.Messages.Add("Error, No se existen datos, Verificar");
                        return BadRequest(_respuestaApi);
                    }
                    respuesta = JsonConvert.DeserializeObject<RecepcionResp>("{\"respuesta\":" + jsonData + "}");
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

      
        [HttpGet("/ConsultaRecepcion")]
        //[Authorize]
        public ActionResult<RecepcionResp> ConsultaRecepcion(string usuario, string identificacion, string placa, string taller, string orden, string vehiculo, string opcion)
        {

            RecepcionResp respuesta;
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
            if (vehiculo == null || string.IsNullOrEmpty(vehiculo.ToString()))
            {
                vehiculo = "0";
            }
            if (opcion =="19")
            {
                if (identificacion == null || string.IsNullOrEmpty(identificacion.ToString()))
                    bandera = true;
            }else
            {
                if (identificacion == null || string.IsNullOrEmpty(identificacion.ToString()))
                {
                    mensaje = "Valor de Identificacion en blanco";
                    bandera = false;
                }
            }
            if (bandera)
            {
                DataSet cnstGenrl = RecepcionSQL.ConsultarRecepcion(usuario, identificacion, placa, taller, orden, vehiculo, opcion);
                if (cnstGenrl.Tables.Count > 0)
                {
                    DataTable tabla = new DataTable();
                    tabla.Columns.Add("RESULTADO", typeof(string));
                    string jsonData = "";
                    if (cnstGenrl.Tables[0].Rows.Count > 0)
                    {
                        jsonData = (string)Utilidades.DataTableToJSON(cnstGenrl.Tables[0]);
                        jsonData = jsonData.ToUpper();
                        jsonData = jsonData.Replace("\"D1\"", "\"CLIE\"");
                        jsonData = jsonData.Replace("\"CLI\"", "\"CLIE\"");
                        jsonData = jsonData.Replace("\"COD\"", "\"CODID\"");
                        jsonData = jsonData.Replace("\"P1\"", "\"PLACA\"");
                        jsonData = jsonData.Replace("\"C2\"", "\"CHA\"");
                        jsonData = jsonData.Replace("\"M3\"", "\"MOT\"");
                        jsonData = jsonData.Replace("\"G4\"", "\"DAT\"");
                        jsonData = jsonData.Replace("\"O5\"", "\"CAT\"");
                        jsonData = jsonData.Replace("\"R6\"", "\"AUDITORIA\"");
                        jsonData = jsonData.Replace("\"O7\"", "\"OBS\"");
                    }else
                    {
                        _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                        _respuestaApi.IsSuccess = false;
                        _respuestaApi.Messages.Add("Error, No se existen datos, Verificar");
                        return BadRequest(_respuestaApi);
                    }
                    respuesta = JsonConvert.DeserializeObject<RecepcionResp>("{\"respuesta\":" + jsonData + "}");
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


        [HttpGet("/ConsultaAccesorio")]
        //[Authorize]
        public ActionResult<AccesorioResp> ConsultaAccesorio(string usuario,  string opcion)
        {
            AccesorioResp respuesta;
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
                DataSet cnstGenrl = RecepcionSQL.ConsultarRecepcion(usuario, "", "", "0", "", "0", opcion);
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
                    respuesta = JsonConvert.DeserializeObject<AccesorioResp>("{\"respuesta\":" + jsonData + "}");
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


        [HttpGet("/RecepcionDocumentoTecnico")]
        //[Authorize]
        public ActionResult<RecepcionResp> RecepcionDocumentoTecnico(string usuario, string vehiculo, string orden, string taller, string cliente, string comentario, string opcion)
        {
            RecepcionResp respuesta;
            bool bandera = true;
            string mensaje = "";
            if (usuario == null || string.IsNullOrEmpty(usuario.ToString()))
            {
                mensaje = "Valor de Usuario en blanco";
                bandera = false;
            }else  if (opcion == null || string.IsNullOrEmpty(opcion.ToString()))
            {
                mensaje = "Valor de Opcion en blanco";
                bandera = false;
            }else if (vehiculo == null || string.IsNullOrEmpty(vehiculo.ToString()))
            {
                mensaje = "Valor de Vehiculo en blanco";
                bandera = false;
            }else if (orden == null || string.IsNullOrEmpty(orden.ToString()))
            {
                mensaje = "Valor de Orden en blanco";
                bandera = false;
            }else if (cliente == null || string.IsNullOrEmpty(cliente.ToString()))
            {
                mensaje = "Valor de Cliente en blanco";
                bandera = false;
            }
         
            if (bandera)
            {
                DataSet cnstGenrl = RecepcionSQL.ConsultarRecepcionTecnico(usuario, vehiculo, orden, taller, cliente, comentario, opcion);
                if (cnstGenrl.Tables.Count > 0)
                {
                    DataTable tabla = new DataTable();
                    tabla.Columns.Add("RESULTADO", typeof(string));
                    string jsonData = "";
                    if (cnstGenrl.Tables[0].Rows.Count > 0)
                    {
                        jsonData = (string)Utilidades.DataTableToJSON(cnstGenrl.Tables[0]);
                        jsonData = jsonData.ToUpper();
                        jsonData = jsonData.Replace("\"CORREO\"", "\"D3\"");
                        jsonData = jsonData.Replace("\"CLIENTE\"", "\"CLIE\"");
                        jsonData = jsonData.Replace("\"FECHA_ENTREGA\"", "\"ENTR\"");
                    }else
                    {
                        _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                        _respuestaApi.IsSuccess = false;
                        _respuestaApi.Messages.Add("Error, No se existen datos, Verificar");
                        return BadRequest(_respuestaApi);
                    }
                    respuesta = JsonConvert.DeserializeObject<RecepcionResp>("{\"respuesta\":" + jsonData + "}");
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


        [HttpGet("/RutaDocumentoFirma")]
        //[Authorize]
        public ActionResult<ComboResp> RutaDocumentoFirma(string vehiculo, string orden, string tipo)
        {
            ComboResp respuesta;
            bool bandera = true;
            string mensaje = "";
            if (vehiculo == null || string.IsNullOrEmpty(vehiculo.ToString()))
            {
                mensaje = "Valor de vehiculo en blanco";
                bandera = false;
            }else if (orden == null || string.IsNullOrEmpty(orden.ToString()))
            {
                mensaje = "Valor de orden en blanco";
                bandera = false;
            }else if (tipo == null || string.IsNullOrEmpty(tipo.ToString()))
            {
                mensaje = "Valor de tipo en blanco";
                bandera = false;
            }
            string ruta="" ;

            if (tipo == "RECEP")
                ruta = "https://www.hunteronline.com.ec/IMGCOTIZADORWEB/GUIARECEPCION/Acta_Recepción_Vehículo_" + vehiculo + "_OS_" + orden + ".pdf";
            if (tipo == "ENTRE")
                ruta = "https://www.hunteronline.com.ec/IMGCOTIZADORWEB/ActaEntrega/Acta_Entrega_" + vehiculo + "_OS_" + orden + ".pdf";
            if (tipo == "REVEH")
                ruta = "https://www.hunteronline.com.ec/IMGCOTIZADORWEB/GUIARECEPCION/Acta_Entrega_Vehiculo_" + vehiculo + "_OS_" + orden + ".pdf";
            if (ruta == null || string.IsNullOrEmpty(ruta.ToString()))
            {
                mensaje = "Valor de Ruta en blanco";
                bandera = false;
            }
            if (bandera)
            {
                string jsonData = Utilidades.RutaDocumentoFirma(ruta, tipo);
                respuesta = JsonConvert.DeserializeObject<ComboResp>("{\"respuesta\":" + jsonData + "}");     
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
