using ApiTaller.Clases;
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
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading.Tasks;

namespace ApiTaller.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("Login")]
    public class LoginTallerController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<LoginTallerController> log;
        private readonly IConsultarEnMemoria repositorio;
        protected RespuestAPI _respuestaApi;
        public LoginTallerController(IConfiguration configuration, ILogger<LoginTallerController> l, IConsultarEnMemoria r)
        {
            this.configuration = configuration;
            this.log = l;
            this.repositorio = r;
            this._respuestaApi = new();
            //_roleManager = roleManager;
        }

        [HttpGet("/LoginTaller")]
        [Authorize]
        public ActionResult<LoginTallerResp> LoginTaller(string usuario, string password)
        {
            LoginTallerResp respuesta = null;
            Boolean bandera = true;
            if (usuario == null || string.IsNullOrEmpty(usuario.ToString()))
            {
                bandera = false;
            }else if (password == null || string.IsNullOrEmpty(password.ToString()))
            {
                bandera = false;
            }
            if (bandera)
            {
                DataSet cnstGenrl = new DataSet();
                cnstGenrl = ConsultarSQLServer.LoginUsuarios(usuario, password, 1);
                if (cnstGenrl.Tables.Count > 0)
                {
                    DataTable tabla = new DataTable();
                    tabla.Columns.Add("RESULTADO", typeof(string));
                    string jsonData = "";
                    if (cnstGenrl.Tables[0].Rows.Count > 0)
                    {
                        tabla = cnstGenrl.Tables[0];
                        DataTable firstTable = tabla;
                        ConvertJson serializar = new ConvertJson();
                        jsonData = JsonConvert.SerializeObject(firstTable);
                    }else
                    {
                        _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                        _respuestaApi.IsSuccess = false;
                        _respuestaApi.Messages.Add("USUARIO INCORRECTO, Verificar");
                        return BadRequest(_respuestaApi);
                    }
                    respuesta = JsonConvert.DeserializeObject<LoginTallerResp>("{\"respuesta\":" + jsonData + "}");
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
                _respuestaApi.Messages.Add("Error, Datos enviados en Blanco");
                return BadRequest(_respuestaApi);
            }
            return respuesta;
        }


    }
}
