﻿
using ApiTaller.DTO;
using ApiTaller.Modelo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System;
using ApiTaller.Repositorio.IRepository;

namespace ApiTaller.Controllers
{
    [ApiController]
    [Route("Login")]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<LoginController> log;
        private readonly IUsuariosSQLServer repositorio;
        public LoginController(IConfiguration configuration, ILogger<LoginController> l, IUsuariosSQLServer r)
        {
            this.configuration = configuration;
            this.log = l;
            this.repositorio = r;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UsuarioDTO>> Login(LoginAPI usuarioLogin)
        {
            UsuarioAPI Usuario = null;
            Usuario = await AutenticarUsuarioAsync(usuarioLogin);
            if (Usuario == null)
                throw new Exception("Credenciales no válidas");
            else
                Usuario = GenerarTokenJWT(Usuario);

            return Usuario.ConvertirDTO();
        }

      
        private async Task<UsuarioAPI> AutenticarUsuarioAsync(LoginAPI usuarioLogin)
        {
            UsuarioAPI usuarioAPI = await repositorio.DameUsuario(usuarioLogin);
            return usuarioAPI;

        }

        private UsuarioAPI GenerarTokenJWT(UsuarioAPI usuarioInfo)
        {
            // Cabecera
            var _symmetricSecurityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["JWT:ClaveSecreta"])
                );
            var _signingCredentials = new SigningCredentials(
                    _symmetricSecurityKey, SecurityAlgorithms.HmacSha256
                );
            var _Header = new JwtHeader(_signingCredentials);

            // Claims
            var _Claims = new[] {
                new Claim("usuario", usuarioInfo.Usuario),
                new Claim("TipoCuenta", usuarioInfo.TipoCuenta),
                new Claim(JwtRegisteredClaimNames.FamilyName, usuarioInfo.Usuario),
            };

            //Payload
            var _Payload = new JwtPayload(
                    issuer: configuration["JWT:Issuer"],
                    audience: configuration["JWT:Audience"],
                    claims: _Claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddHours(1)
                );

            // Token
            var _Token = new JwtSecurityToken(
                    _Header,
                    _Payload
                );
            usuarioInfo.Token = new JwtSecurityTokenHandler().WriteToken(_Token);

            return usuarioInfo;
        }
    }
}
