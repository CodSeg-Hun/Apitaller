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
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Net;

namespace ApiTaller.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("Latam")]
    public class LoginLatamController : Controller
    {
    
        private readonly IConfiguration configuration;
        private readonly ILogger<LoginLatamController> log;
        private readonly IConsultarEnMemoria repositorio;
        protected RespuestAPI _respuestaApi;
        public LoginLatamController(IConfiguration configuration, ILogger<LoginLatamController> l, IConsultarEnMemoria r)
        {
            this.configuration = configuration;
            this.log = l;
            this.repositorio = r;
            this._respuestaApi = new();
            //_roleManager = roleManager;
        }

        [HttpGet("/LoginLatam")]
        [Authorize]
        //[AllowAnonymous]
        public ActionResult<LoginLatamResp> LoginLatam(string usuario, string password)
        {
            LoginLatamResp respuesta = null;
            bool bandera = true;
            string valor = "";
            string accesstoken = "";
            ConexionLatam ruta = new ConexionLatam();
            string usuarioToken = ruta.GetRuta("1");
            string claveToken = ruta.GetRuta("2");
            string rutaToken = ruta.GetRuta("3");
            string rutaLogin = ruta.GetRuta("4");
            if (usuario == null || string.IsNullOrEmpty(usuario.ToString()))
            {
                bandera = false;
            }else if (password == null || string.IsNullOrEmpty(password.ToString()))
            {
                bandera = false;
            }
            if (bandera)
            {
                var client = new RestClient(rutaToken);
                var request = new RestRequest("", Method.Post);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddParameter("id", usuarioToken);
                request.AddParameter("clave", claveToken);
                RestResponse response = client.Execute(request);
                valor = response.StatusCode.ToString();
                if (valor.Equals("OK"))
                {
                    var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(response.Content);
                    accesstoken = funciones.descomponer("token", jsonString);
                }else
                {
                    //bandera = false;
                    _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                    _respuestaApi.IsSuccess = false;
                    _respuestaApi.Messages.Add("Error, No se genero el token, Verificar");
                    return BadRequest(_respuestaApi);
                }
            }
            if (bandera)
            {
                var client = new RestClient(rutaLogin);
                var request = new RestRequest("", Method.Post);
                request.AddHeader("content-type", "application/x-www-galvarado  form-urlencoded");
                request.AddHeader("token", accesstoken);
                request.AddParameter("usuario", usuario);
                request.AddParameter("password", password);
                RestResponse response = client.Execute(request);
                valor = response.StatusCode.ToString();
                if (valor.Equals("OK"))
                {
                     respuesta = JsonConvert.DeserializeObject<LoginLatamResp>(response.Content);
                }else
                {
                    //bandera = false;
                    _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                    _respuestaApi.IsSuccess = false;
                    _respuestaApi.Messages.Add("Error, No se genero el token, Verificar");
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
